using System.Collections.Generic;

namespace Time_Tracker.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Salary { get; set; }
        public double Schedule { get; set; }
        public List<Company> Companies { get; set; } = new List<Company>();
        public List<User> Users { get; set; } = new List<User>();
    }
}
