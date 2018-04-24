using System;
using System.Linq;
using ED.Models.Command;
using ED.Repositories.EntityFramework;

namespace ED.Repositories.Migrations
{
    public static class DbInitializer
    {
        public static void Initialize(EntityFrameworkContext context)
        {
            context.Database.EnsureCreated();
            
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            var users = new User[]
            {
            new User{Name="Carson",RealName="Alexander Carson",CreationTime=DateTime.Parse("2005-09-01")},
            new User{Name="Meredith",RealName="Alonso Meredith",CreationTime=DateTime.Parse("2002-09-01")},
            new User{Name="Arturo",RealName="Anand Arturo",CreationTime=DateTime.Parse("2003-09-01")}
            };
            foreach (User s in users)
            {
                context.Users.Add(s);
            }
            context.Users.AddRange(users);
            //context.SaveChanges();
        }
    }
}
