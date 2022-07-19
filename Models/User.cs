using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Time_Tracker.Models
{
    public class User : IdentityUser
    {
        public List<Time> Time { get; set; } = new List<Time>();
        public List<Tasks> Tasks { get; set; } = new List<Tasks>();
        public Company? Company { get; set; }
        public Post? Post { get; set; }
    }
}
