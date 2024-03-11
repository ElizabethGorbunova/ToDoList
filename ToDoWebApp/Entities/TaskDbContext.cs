using Microsoft.EntityFrameworkCore;

namespace ToDoWebApp.Entities
{
    public class TaskDbContext: DbContext
    {
        private string _connectionString = "Server=localhost;Database=TaskDbContext;Trusted_Connection=True";
       public DbSet<MyTask> Tasks { get; set; }
       public DbSet<User> Users { get; set; }
       public DbSet<Role> Roles { get; set; }
       public DbSet<Group> Groups { get; set; }
       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MyTask>()
                .Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(20);

            /*modelBuilder.Entity<User>().HasOne    WithMany(t => t.MyTasks).HasForeignKey(u => u.MyTaskId).OnDelete(DeleteBehavior.Restrict);
*/
            modelBuilder.Entity<User>()
                .Property(t=>t.Name)
                .IsRequired()
                .HasMaxLength(30);

            modelBuilder.Entity<User>()
                .Property(u=>u.LastName)
                .IsRequired()
                .HasMaxLength(30);

            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired();

            modelBuilder.Entity<Role>()
                .Property(r => r.Name)
                .IsRequired();

            modelBuilder.Entity<Group>()
                .Property(g => g.GroupName)
                .IsRequired();

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }




    }
}
