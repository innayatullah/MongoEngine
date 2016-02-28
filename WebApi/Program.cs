using System;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Hosting;
using MongoDB.AspNet.Identity;
using WebApi.Models;

namespace WebApi
{
    class Program
    {
        // ReSharper disable once UnusedParameter.Local
        static void Main(string[] args)
        {
            var baseUri = Common.Configurations.ApplicationDomain;

            Console.WriteLine("Starting web Server...");
            WebApp.Start<Startup>(baseUri);
            Seed();
            Console.WriteLine("Server running at {0} - press Enter to quit. ", baseUri);
            Console.ReadLine();
        }
        
        protected static void Seed()
        {
            //  This method will be called after migrating to the latest version.

            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new IdentityDbContext()));

            var user = new ApplicationUser()
            {
                UserName = "Administrator",
                Email = "admin@email.com",
                EmailConfirmed = true,
                FirstName = "Admin",
                LastName = "Test",
                Level = 1,
                JoinDate = DateTime.Now.AddYears(-3)
            };

            manager.Create(user, "P@ssw0rd");
        }
    }
}
