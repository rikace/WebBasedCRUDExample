using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using TechStudioTest.Models;

namespace TechStudioTest.DataAccess
{
    public class AuthInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<AuthContext>
    {
        protected override void Seed(AuthContext context)
        {
            var users = new List<User>
            {
                new User { UserName="admin", FirstName="admin", LastName="admin", Email="admin@admin.com", IsActive=true, Password="admin" }
            };
            users.ForEach(u => context.Users.Add(u));
            context.SaveChanges();
        }
    }
}