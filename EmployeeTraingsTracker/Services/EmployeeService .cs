using EmployeeTraingsTracker.Data;
using EmployeeTraingsTracker.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace EmployeeTraingsTracker.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public EmployeeService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<List<Employee>> GetAllAsync() =>
            await _context.Employees.ToListAsync();

        public async Task<Employee> GetByIdAsync(int id) =>
            await _context.Employees.FindAsync(id);

        public async Task AddAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            if (employee == null) return;

            var existing = await _context.Employees.FindAsync(employee.Id);
            if (existing == null) return;

            // Only editable fields
            existing.Address = employee.Address;
            existing.PhoneNumber = employee.PhoneNumber;
            existing.Designation = employee.Designation;
            existing.Department = employee.Department;

            await _context.SaveChangesAsync();
            await _context.SaveChangesAsync();
        }

        public async Task<Employee> GetByEmailAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
                return null;

            return await _context.Employees
                                 .AsNoTracking() // read-only for safety
                                 .FirstOrDefaultAsync(e => e.Email == email);
        }
        public async Task DeleteAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<bool> IsEmailRegisteredAsEmployeeAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return false;

            var roles = await _userManager.GetRolesAsync(user);
            return roles.Contains("Employee");
        }
    }

}
