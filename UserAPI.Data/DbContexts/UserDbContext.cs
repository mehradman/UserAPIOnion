using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserAPI.Data.Entities;

namespace UserAPI.Data.DbContexts
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasData(
                new User()
                {
                    UserId = 1,
                    UserName = "mehradman",
                    Email = "mehrad@radman.com",
                    Password = "123"
                },
                 new User()
                 {
                     UserId = 2,
                     UserName = "rezaman",
                     Email = "reza@radman.com",
                     Password = "123"
                 },
                  new User()
                  {
                      UserId = 3,
                      UserName = "radarman",
                      Email = "arman@radman.com",
                      Password = "123"
                  }
                );
            modelBuilder.Entity<UserInfo>()
                .HasData(
                new UserInfo()
                {
                    UserInfoId = 1,
                    UserId = 1,
                    FullName = "Mehrad Radman",
                    Phone = "09381231234",
                    PlaceOfBirth = "Ahvaz",
                    JobTitle = "Software"
                },
                 new UserInfo()
                 {
                     UserInfoId = 2,
                     UserId = 2,
                     FullName = "Reza Radman",
                     Phone = "09161231234",
                     PlaceOfBirth = "Dezful",
                     JobTitle = "Agriculture"
                 },
                   new UserInfo()
                   {
                       UserInfoId = 3,
                       UserId = 3,
                       FullName = "Arman Radman",
                       Phone = "09351231234",
                       PlaceOfBirth = "Abadan",
                       JobTitle = "Manager"
                   }
                );


            base.OnModelCreating(modelBuilder);
        }
    }
}
