namespace Time_Tracker.ViewModels.Times
{
    public class TrackerModel
    {
        public Models.User user { get; set; }
        public bool IsStarted { get; set; }
        public bool IsPaused { get; set; }
        public bool IsFinished { get; set; }
        public string Error { get; set; }
    }
}
