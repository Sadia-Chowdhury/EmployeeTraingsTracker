using System.ComponentModel.DataAnnotations;

namespace EmployeeTraingsTracker.Model
{
    public class TrainingInfo
    {
        [Required]
        [Key]
        public int TrainingId { get; set; }

        public string Title { get; set; } = string.Empty;

        public int CategoryID { get; set; }

        public bool IsActive { get; set; }

        public ICollection<Training> Trainings { get; set; }


    }
}
