using EmployeeTraingsTracker.Components.Pages.Employee;
using EmployeeTraingsTracker.Data;
using EmployeeTraingsTracker.Model;
using Microsoft.EntityFrameworkCore;

namespace EmployeeTraingsTracker.Services
{
    public class TrainingInfoService : ITrainingInfoService
    {
        private readonly ApplicationDbContext _context;

        public TrainingInfoService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Save(TrainingInfo traininginfo)
        {
            _context.TrainingInfo.Add(traininginfo);
            await _context.SaveChangesAsync();
        }
        public async Task<TrainingInfo> GetByIdAsync(int id) =>
         await _context.TrainingInfo.FindAsync(id);

        public async Task DeleteAsync(int id)
        {
            // Fixed: Delete from TrainingCatagory table, not Trainings table
            var category = await _context.TrainingCatagory.FindAsync(id);
            if (category != null)
            {
                _context.TrainingCatagory.Remove(category);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(TrainingInfo traininginfo)
        {
            if (traininginfo == null) return;

            var existing = await _context.TrainingCatagory.FindAsync(traininginfo.TrainingId);
            if (existing == null) return;

            // Update editable fields
            existing.CategoryName = traininginfo.Title;

            // Fixed: Remove duplicate SaveChangesAsync call
            await _context.SaveChangesAsync();
        }

        public async Task<List<TrainingInfo>> GetAllAsync()
        {
            try
            {
                var traininginfo = await _context.TrainingInfo.ToListAsync();
                //Console.WriteLine($"CategoryService.GetAllAsync: Retrieved {categories.Count} categories from database");
                return traininginfo;
            }
            catch (Exception ex)
            {
               // Console.WriteLine($"TrainingInfoService.GetAllAsync Error: {ex.Message}");
               // Console.WriteLine($"Full exception: {ex}");
                throw;
            }
        }
    }
}
