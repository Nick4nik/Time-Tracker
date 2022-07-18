using Time_Tracker.Models;

namespace Time_Tracker.ViewModels
{
    public class UserInfoViewModel
    {
        public User user { get; set; }
        public bool Tasks { get; set; }
        public bool Time { get; set; }
    }
}
