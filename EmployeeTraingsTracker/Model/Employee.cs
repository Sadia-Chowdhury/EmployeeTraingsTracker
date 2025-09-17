using System.ComponentModel.DataAnnotations;

namespace EmployeeTraingsTracker.Model
{
    public class Employee
    {
        public int Id { get; set; }

        // Personal Information
        [Required]
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string? Address { get; set; }

        // Professional Information
        public string? EmployeeId { get; set; } // Company employee ID
        public string? Designation { get; set; }
        public string? Department { get; set; }
        public string? AssignedTeam { get; set; } // Team name
        public string? ReportsTo { get; set; } // Manager name

        // Emergency Contact
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactPhone { get; set; }
        public string? EmergencyContactRelation { get; set; }



        // Navigation property (link to trainings through join table)
        public ICollection<EmployeeTraining> EmployeeTrainings { get; set; }
    }
}
