using System;

namespace Time_Tracker.Models
{
    public class Time
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Duration { get; set; }
        public TimeSpan PauseDuration { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
