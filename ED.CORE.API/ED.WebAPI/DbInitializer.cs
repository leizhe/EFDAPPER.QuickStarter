using System;
using System.Linq;
using ED.Models.Command;
using ED.Repositories.EntityFramework;

namespace ED.WebAPI
{
    public static class DbInitializer
    {
        private static EntityFrameworkContext _context;
        public static void Initialize(IServiceProvider serviceProvider)
        {
            _context = (EntityFrameworkContext)serviceProvider.GetService(typeof(EntityFrameworkContext));

            Initialize();
        }

        public static void Initialize()
        {
            //_context.Database.EnsureCreated();

            if (_context.Users.Any())
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
                _context.Users.Add(s);
            }
            _context.Users.AddRange(users);
            //context.SaveChanges();
        }
    }
}
