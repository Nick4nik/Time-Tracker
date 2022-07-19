using System.Collections.Generic;

namespace Time_Tracker.ViewModels.Time
{
    public class TimeTodayViewModel
    {
        public List<Models.Time> Time { get; set; }
        public bool IsExist { get; set; }
    }
}
