using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Time_Tracker.Models;

namespace Time_Tracker.ViewModels
{
    public class LoginRegisterViewModel
    {
        public string LoginEmail { get; set; }
        public string LoginPassword { get; set; }
        public bool LoginRememberMe { get; set; }
        public string RegisterEmail { get; set; }
        public List<Company> RegisterCompany { get; set; }
        public List<Post> RegisterPost { get; set; }
        public string RegisterPassword { get; set; }
        public string RegisterPasswordConfirm { get; set; }
        public bool? Message { get; set; }
    }
}
