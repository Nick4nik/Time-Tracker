using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Time_Tracker.Models
{
    public class User : IdentityUser
    {
        public int CompanyId { get; set; }
        public int PostId { get; set; }
        public List<Time> Time { get; set; } = new List<Time>();
        public List<Tasks> Tasks { get; set; } = new List<Tasks>();

        [ForeignKey(nameof(CompanyId))]
        public Company Company { get; set; }

        [ForeignKey(nameof(PostId))]
        public Post Post { get; set; }
    }
}
