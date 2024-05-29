using Microsoft.EntityFrameworkCore;
using Mysqlx.Crud;
using Programming_Web_API.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Threading.Tasks;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace Programming_Web_API.Data
{
    public class DataContext : DbContext
    {

        //Table Names (Exact Database match)
        public DbSet<Booking> Booking { get; set; }
        public DbSet<CampingSpot> CampingSpot { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<User> User { get; set; }



        //Data Context Options
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }


        //Foreign Keys Linken
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //CampingSpot Foreign Keys
             modelBuilder.Entity<CampingSpot>()
            .HasOne(cs => cs.Owner)
            .WithMany(u => u.CampingSpot)
            .HasForeignKey(cs => cs.FK_Owner);

            //Booking Foreign Key 1
            modelBuilder.Entity<Booking>()
            .HasOne(cs => cs.Booker)
            .WithMany(u => u.Bookings)
            .HasForeignKey(cs => cs.FK_BookedBy);

            //Booking Foreign Key 2
            modelBuilder.Entity<Booking>()
            .HasOne(cs => cs.CampingSpot)
            .WithMany(u => u.Bookings)
            .HasForeignKey(cs => cs.FK_CampingSpot);

            //Review Foreign Key 1
            modelBuilder.Entity<Review>()
            .HasOne(cs => cs.Reviewer)
            .WithMany(u => u.Reviews)
            .HasForeignKey(cs => cs.FK_Reviewer);

            //Review Foreign Key 2
            modelBuilder.Entity<Review>()
            .HasOne(cs => cs.CampingSpot)
            .WithMany(u => u.Reviews)
            .HasForeignKey(cs => cs.FK_CampingSpot);




            base.OnModelCreating(modelBuilder);
        }






    }
}
