using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Time_Tracker.Models
{
    public class User : IdentityUser
    {
        public List<Company>? Company { get; set; } = new List<Company>();
        public List<Post>? Post { get; set; } = new List<Post>();
    }
}
