using FriendOrganizer.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendOrganizer.Infra.DataAccess
{
    public class FriendOrganizerDbContext : DbContext
    {

        public DbSet<Friend> Friends { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           // //If they can't share the same App.config, you can use the syntax to use a separate config file for the connection strings, and then include that file as a link.
           // var conStringConfig = ConfigurationManager.ConnectionStrings["FriendOrganizerDb"];
           // if(conStringConfig != null)
           // {
           //     Console.WriteLine(conStringConfig);
           // }
           // else
           // {
           //     Console.WriteLine("CANNOT GET CONNECTION STRING FROM CONFIGURATION");
           // }

            //TODO: It gets the con string in the UI, but it doesnt get it in DataAccess.
             string conString = ConfigurationManager.ConnectionStrings["FriendOrganizerDb"].ConnectionString;

            //string conString = @"Server =(localdb)\mssqllocaldb;Database=FriendOrganizer;Trusted_Connection =True;";
            optionsBuilder.UseSqlServer(conString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Friend>().Property(f => f.FirstName).IsRequired().HasMaxLength(50);

            modelBuilder.Entity<Friend>().HasData(
                    new Friend { Id=1, FirstName = "Thomas", LastName = "Huber", Email=" "},
                    new Friend { Id=2, FirstName = "Urs", LastName = "Meier", Email = " " },
                    new Friend { Id=3, FirstName = "Erkan", LastName = "Egin", Email = " " },
                    new Friend { Id=4, FirstName = "Sara", LastName = "Huber", Email = " " }

                );
        }

    }
}
