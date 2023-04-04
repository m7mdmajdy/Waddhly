using System.ComponentModel.DataAnnotations;
using Waddhly.Models.UserServices;

namespace Waddhly.Models.Authentication
{
    public class RegisterModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Summary { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public double HourRate { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Password { get; set; }
        
        public virtual int categoryID { get; set; }
    }
}
