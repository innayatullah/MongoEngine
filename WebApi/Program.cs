using System;
using System.Linq;
using MongoEngine.Repositories;
using WebApi.DbModels;
using Microsoft.Owin.Hosting;

namespace WebApi
{
    class Program
    {
        // ReSharper disable once UnusedParameter.Local
        static void Main(string[] args)
        {
            var baseUri = "http://localhost:8080";

            Console.WriteLine("Starting web Server...");
            WebApp.Start<Startup>(baseUri);
            ExecuteGenericDepartment();
            Console.WriteLine("Server running at {0} - press Enter to quit. ", baseUri);
            Console.ReadLine();
        }

        // ReSharper disable once UnusedMember.Local
        private static void ExecuteGenericDepartment()
        {
            var repo = new GenericMongoRepository<Department>();
            repo.RemoveAll();
            repo.Add(new Department
            {
                Name = "Admin",
                Description = "Administration Department"
            });
            var allRoles = repo.GetAll();
            var role = allRoles.FirstOrDefault(x => x.Name == "Admin");
            if (role != null)
            {
                role.Description = "Administration Department Updated";
                repo.Update(role);
                repo.Get(role.Id);
            }
        }
    }
}
