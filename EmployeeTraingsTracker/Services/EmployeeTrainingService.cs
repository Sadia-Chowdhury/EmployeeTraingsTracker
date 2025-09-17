using EmployeeTraingsTracker.Data;
using EmployeeTraingsTracker.Model;
using Microsoft.EntityFrameworkCore;

namespace EmployeeTraingsTracker.Services
{
    public class EmployeeTrainingService : IEmployeeTrainingService
    {
        private readonly ApplicationDbContext _context;

        public EmployeeTrainingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<EmployeeTraining>> GetForEmployeeAsync(int employeeId)
        {
            return await _context.EmployeeTrainings
                .Include(et => et.Training) // eager load Training details
                .Where(et => et.EmployeeId == employeeId)
                .ToListAsync();
        }

        public async Task AddAsync(EmployeeTraining employeeTraining)
        {
            // Prevent duplicate training assignment
            var exists = await _context.EmployeeTrainings
                .AnyAsync(et => et.EmployeeId == employeeTraining.EmployeeId
                             && et.TrainingId == employeeTraining.TrainingId);

            if (!exists)
            {
                _context.EmployeeTrainings.Add(employeeTraining);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int employeeId, int trainingId)
        {
            var et = await _context.EmployeeTrainings
                .FirstOrDefaultAsync(x => x.EmployeeId == employeeId && x.TrainingId == trainingId);

            if (et != null)
            {
                _context.EmployeeTrainings.Remove(et);
                await _context.SaveChangesAsync();
            }
        }
    }

}