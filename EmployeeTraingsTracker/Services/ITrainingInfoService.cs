using EmployeeTraingsTracker.Model;

namespace EmployeeTraingsTracker.Services
{
    public interface ITrainingInfoService
    {
        Task Save(TrainingInfo training);

        Task UpdateAsync(TrainingInfo training);
        Task DeleteAsync(int id);

        Task<List<TrainingInfo>> GetAllAsync();
        Task<TrainingInfo> GetByIdAsync(int id);

    }
}
