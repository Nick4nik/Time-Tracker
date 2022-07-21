using System.Collections.Generic;
using Time_Tracker.Models;

namespace Time_Tracker.ViewModels.Logins
{
    public class LoginRegisterViewModel
    {
        public string LoginEmail { get; set; }
        public string LoginPassword { get; set; }
        public bool LoginRememberMe { get; set; }
        public string RegisterEmail { get; set; }
        public int CompanyId { get; set; }
        public int PostId { get; set; }
        public string RegisterPassword { get; set; }
        public string RegisterPasswordConfirm { get; set; }
        public bool Error { get; set; }
        public List<Company> RegisterCompany { get; set; }
        public List<Post> RegisterPost { get; set; }
    }
}
