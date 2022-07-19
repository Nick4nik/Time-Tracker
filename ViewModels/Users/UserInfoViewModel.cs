using System;
using Time_Tracker.Models;

namespace Time_Tracker.ViewModels.User
{
    public class UserInfoViewModel
    {
        public User user { get; set; }
        public bool IsTasks { get; set; }
        public bool IsTime { get; set; }
        public int ActiveTasks { get; set; }
        public string ScheduleHours { get; set; }
        public string ScheduleSeconds { get; set; }
        public DateTime Start { get; set; }
        public DateTime ExpEnd { get; set; }
    }
}
