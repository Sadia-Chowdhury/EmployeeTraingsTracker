using System.ComponentModel.DataAnnotations;

namespace EmployeeTraingsTracker.Model
{
    public class TrainingCatagory
    {
        [Key]
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        // Navigation property
        public ICollection<Training> Trainings { get; set; }
    }
}
