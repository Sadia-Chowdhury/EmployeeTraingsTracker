namespace EmployeeTraingsTracker.Model
{
    public class EmployeeTraining
    {
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public int TrainingId { get; set; }
        public Training Training { get; set; }

        // Completion tracking
        public bool IsCompleted { get; set; } = false;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime EnrolledOn { get; set; } = DateTime.Now;
    }
}
