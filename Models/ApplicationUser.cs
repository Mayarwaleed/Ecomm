using Ecomm.Models;
using Microsoft.AspNetCore.Identity;

namespace Ecomm.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Order>? Orders { get; set; }
    }
}