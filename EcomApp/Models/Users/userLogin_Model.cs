using System.ComponentModel.DataAnnotations;

namespace EcomApp.Models.Users
{
    public class userLogin_Model
    {
        [Required]
        public string UserEmail { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
