using EmployeeTraingsTracker.Components.Pages;
using EmployeeTraingsTracker.Components.Pages.Employee;
using EmployeeTraingsTracker.Data;
using EmployeeTraingsTracker.Model;
using Microsoft.EntityFrameworkCore;

namespace EmployeeTraingsTracker.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Save(TrainingCatagory category)
        {
            _context.TrainingCatagory.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task<List<TrainingCatagory>> GetAllAsync()
        {
            try
            {
                var categories = await _context.TrainingCatagory.ToListAsync();
                //Console.WriteLine($"CategoryService.GetAllAsync: Retrieved {categories.Count} categories from database");
                return categories;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CategoryService.GetAllAsync Error: {ex.Message}");
                Console.WriteLine($"Full exception: {ex}");
                throw;
            }
        }

        public async Task<TrainingCatagory> GetByIdAsync(int id) =>
            await _context.TrainingCatagory.FindAsync(id);

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

        public async Task UpdateAsync(TrainingCatagory category)
        {
            if (category == null) return;

            var existing = await _context.TrainingCatagory.FindAsync(category.CategoryId);
            if (existing == null) return;

            // Update editable fields
            existing.CategoryName = category.CategoryName;

            // Fixed: Remove duplicate SaveChangesAsync call
            await _context.SaveChangesAsync();
        }
    }
}