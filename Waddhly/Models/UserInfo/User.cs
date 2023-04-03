using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Waddhly.Models.UserServices;

namespace Waddhly.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public double MoneyAccount { get; set; }
        public string Country { get; set; }
        public double HourRate { get; set; }
        public virtual Category category{ get; set; }

    }
}
