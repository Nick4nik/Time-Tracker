﻿using System;

namespace Time_Tracker.Models
{
    public class Time
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime ExpEnd { get; set; }
        public DateTime End { get; set; }
        public TimeSpan Duration { get; set; }
        public TimeSpan PauseDuration { get; set; }
        public int Session { get; set; }
        public string Date { get; set; }
        public bool IsWorking { get; set; }
        public bool IsPaused { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
    }
}
