using System.Collections.Generic;

namespace EcomApp.Models.Users
{
    public class UserRegistrationModel
    {
        public string username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } 
        public IList<User_details> userdetails { get; set; }

    }
}
