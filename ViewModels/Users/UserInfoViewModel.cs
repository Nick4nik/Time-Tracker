using Time_Tracker.Models;

namespace Time_Tracker.ViewModels.Users
{
    public class UserInfoViewModel
    {
        public User user { get; set; }
        public bool IsTasks { get; set; }
        public bool IsTime { get; set; }
        public int ActiveTasks { get; set; }
        public string ScheduleHours { get; set; }
        public string ScheduleSeconds { get; set; }
        public string Start { get; set; }
        public string ExpEnd { get; set; }
    }
}
