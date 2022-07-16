namespace Time_Tracker.Models
{
    public class Tasks
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public User User { get; set; }
    }
}
