using EmployeeTraingsTracker.Model;

namespace EmployeeTraingsTracker.Services
{
    public interface IEmployeeTrainingService
    {
        Task<List<EmployeeTraining>> GetForEmployeeAsync(int employeeId);
        Task AddAsync(EmployeeTraining employeeTraining);
        Task DeleteAsync(int employeeId, int trainingId);
    }
}
