using EmployeeTraingsTracker.Data;
using EmployeeTraingsTracker.Model;
using Microsoft.EntityFrameworkCore;

namespace EmployeeTraingsTracker.Services
{
    public class TrainingService : ITrainingService
    {
        private readonly ApplicationDbContext _context;

        public TrainingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Training>> GetAllAsync() =>
            await _context.Trainings.ToListAsync();

        public async Task<Training> GetByIdAsync(int id) =>
            await _context.Trainings.FindAsync(id);

        public async Task AddAsync(Training training)
        {
            _context.Trainings.Add(training);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Training training)
        {
            _context.Trainings.Update(training);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var training = await _context.Trainings.FindAsync(id);
            if (training != null)
            {
                _context.Trainings.Remove(training);
                await _context.SaveChangesAsync();
            }
        }
    }
}
