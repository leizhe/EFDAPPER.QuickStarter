using ED.Common;
using ED.Models.Command;
using Microsoft.EntityFrameworkCore;

namespace ED.Repositories.EntityFramework
{
    public class EntityFrameworkContext : DbContext
    {

        //public EntityFrameworkContext(DbContextOptions<EntityFrameworkContext> options)
        //    : base(options)
        //{
        //}


        private readonly string _connstr= Global.CommandDB;
        //private readonly string _connstr = "dsdsdsd";
        //public EntityFrameworkContext()
        //{
        //}

        public EntityFrameworkContext(string connstr)
        {
            _connstr = connstr;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connstr);
            //optionsBuilder.UseInMemoryDatabase("TodoList");
            //base(optionsBuilder);
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }
        
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Role>().ToTable("Role");
            modelBuilder.Entity<UserRole>().ToTable("UserRole");
        }


    }
}