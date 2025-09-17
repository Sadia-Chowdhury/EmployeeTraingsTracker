namespace EmployeeTraingsTracker.Model
{
    public class Training
    {
        public int Id { get; set; } // Primary Key
        public string Title { get; set; }
        public string Description { get; set; }

        public string? Category { get; set; } // e.g., "Technical", "Safety", "Soft Skills"
        public string? Institute { get; set; } // Training provider/institute
        public int DurationHours { get; set; } // Duration in hours
        // Navigation property (link to employees through join table)
        public ICollection<EmployeeTraining> EmployeeTrainings { get; set; }
    }
}
