using System.Collections.Generic;

namespace Time_Tracker.ViewModels.Times
{
    public class TimeHistoryViewModel
    {
        public List<Models.Time> Time { get; set; }
        public bool IsExist { get; set; }
    }
}
