using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Class;
using BCrypt.Net;

namespace TaskManagement.Data.Contexts
{
    public static class SeedDB
    {
        public static void Seed(TaskManagementDBContext context)
        {
            //if (!context.Users.Any( u => u.Role == EnumEmpRole.Admin))       //check admin
            if (!context.Users.Any())
            {
                var users = new List<User>
                {
                    new User
                    {
                        Id = 1,
                        Email = "admin@demo.com",
                        Role = EnumEmpRole.Admin,
                        Password = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                        Name = "Admin",
                        CreatedBy = 1,
                        ModifiedBy = 1
                    },
                    new User
                    {
                        Id = 2,
                        Email = "employee@demo.com",
                        Role = EnumEmpRole.Employee,
                        Password = BCrypt.Net.BCrypt.HashPassword("Employee123!"),
                        Name = "Employee",
                        CreatedBy = 1,
                        ModifiedBy = 1
                    },
                    new User
                    {
                        Id = 3,
                        Email = "manager@demo.com",
                        Role = EnumEmpRole.Manager,
                        Password = BCrypt.Net.BCrypt.HashPassword("Manager123!"),
                        Name = "Manager",
                        CreatedBy = 1,
                        ModifiedBy = 1
                    }
                };

                context.Users.AddRange(users);
                context.SaveChanges();
            }
        }
    }
}
