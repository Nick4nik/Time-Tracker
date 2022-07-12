using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Time_Tracker.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<User>? Users { get; set; } = new List<User>();
        public List<Post>? Posts { get; set; } = new List<Post>();
    }
}
