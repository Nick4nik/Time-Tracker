using System;

namespace Time_Tracker.Models
{
    public class Time
    {
        public int Id { get; set; }
        public string Start { get; set; }
        public string ExpEnd { get; set; }
        public string? End { get; set; }
        public string? Duration { get; set; }
        public string? PauseDuration { get; set; }
        public int Session { get; set; }
        public string Date { get; set; }
        public string? StartPause { get; set; }
        public bool IsFullDay { get; set; }
        public bool IsStarted { get; set; }
        public bool IsPaused { get; set; }
        public bool IsFinished { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}