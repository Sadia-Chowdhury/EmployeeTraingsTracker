using EmployeeTraingsTracker.Model;

namespace EmployeeTraingsTracker.Services
{
    public interface ICategoryService
    {

        Task Save(TrainingCatagory category);

        Task UpdateAsync(TrainingCatagory category);
        Task DeleteAsync(int id);

        Task<List<TrainingCatagory>> GetAllAsync();
        Task<TrainingCatagory> GetByIdAsync(int id);

    }
}
