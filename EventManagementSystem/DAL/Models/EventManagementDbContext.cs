using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class EventManagementDbContext : DbContext
    {
        //all the Dbset properties for all model classes
        public DbSet<UserInfo> UserInfos { get; set; }

        public DbSet<EventDetails> EventDetails { get; set; }

        public DbSet<SpeakersDetails> SpeakersDetails { get; set; }

        public DbSet<SessionInfo> SessionInfos { get; set; }

        public DbSet<ParticipantEventDetails> ParticipantEventDetails { get; set; }

        //Overriding Onconfiguring Model

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(DatabaseHelper.GetConnectionString());
            }
        }

        //Overriding OnModelCreating method
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define relationships here using Fluent API
            modelBuilder.Entity<SessionInfo>().HasOne<SpeakersDetails>().WithMany().HasForeignKey(s => s.SpeakerId);

            modelBuilder.Entity<SessionInfo>().HasOne<EventDetails>().WithMany().HasForeignKey(e => e.EventId);

            modelBuilder.Entity<ParticipantEventDetails>().HasOne<UserInfo>().WithMany().HasForeignKey(e => e.ParticipantEmailId);

            modelBuilder.Entity<ParticipantEventDetails>().HasOne<EventDetails>().WithMany().HasForeignKey(e => e.EventId);


            //Adding Seed values for default admin info

            modelBuilder.Entity<UserInfo>().HasData
                (
                  new UserInfo { EmailId = "admin@gmail.com", Password = "admin", Role = "Admin", UserName = "Admin", IsDeleted = false }
                ) ;


        }

        //Override SaveChanges for performing soft delete
        public override int SaveChanges()
        {
            //  set IsDeleted flag to true

            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;
                    entry.CurrentValues["IsDeleted"] = true;
                }
            }
            return base.SaveChanges();

        }
    }
}
