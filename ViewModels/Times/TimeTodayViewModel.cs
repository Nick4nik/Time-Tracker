using System.Collections.Generic;

namespace Time_Tracker.ViewModels.Times
{
    public class TimeTodayViewModel
    {
        public List<Models.Time> Time { get; set; }
        public bool IsExist { get; set; }
        public bool Error { get; set; }
        public string Message { get; set; }
    }
}
