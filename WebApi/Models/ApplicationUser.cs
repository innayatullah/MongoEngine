using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using MongoDB.AspNet.Identity;

namespace WebApi.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public byte Level { get; set; }
        
        public DateTime JoinDate { get; set; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
