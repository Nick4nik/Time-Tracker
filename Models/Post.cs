using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Time_Tracker.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Salary { get; set; }
        public double Schedule { get; set; }
        public List<Company>? Company { get; set; } = new List<Company>();
        public List<User>? User { get; set; } = new List<User>();
    }
}
