using System;
using MongoDB.AspNet.Identity;

namespace WebApi.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public byte Level { get; set; }
        
        public DateTime JoinDate { get; set; }
    }
}
