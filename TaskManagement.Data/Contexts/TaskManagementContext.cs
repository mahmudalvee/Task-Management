using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TaskManagement.Class;

namespace TaskManagement.Data.Contexts
{
    public class TaskManagementContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TTask> TTasks { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        public TaskManagementContext(DbContextOptions<TaskManagementContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TTask>()        //1 to Many
                .HasOne(t => t.AssignedToUser)
                .WithMany()
                .HasForeignKey(t => t.AssignedToUserId)
                .OnDelete(DeleteBehavior.Cascade); ;

            modelBuilder.Entity<TTask>()
                .HasOne(t => t.Team)
                .WithMany()
                .HasForeignKey(t => t.TeamId)
                .OnDelete(DeleteBehavior.Restrict); //Cascad delete

        }
    }


}
