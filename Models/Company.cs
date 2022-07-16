using System.Collections.Generic;

namespace Time_Tracker.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Tasks> Tasks { get; set; } = new List<Tasks>();
        public List<User> Users { get; set; } = new List<User>();
        public List<Post> Posts { get; set; } = new List<Post>();
    }
}