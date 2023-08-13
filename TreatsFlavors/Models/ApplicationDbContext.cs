using Microsoft.EntityFrameworkCore;

namespace TreatsFlavors.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Treat> Treats { get; set; }
        public DbSet<Flavor> Flavors { get; set; }
        public DbSet<TreatFlavor> TreatFlavors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TreatFlavor>()
                .HasKey(tf => new { tf.TreatId, tf.FlavorId });

            modelBuilder.Entity<TreatFlavor>()
                .HasOne(tf => tf.Treat)
                .WithMany(t => t.TreatFlavors)
                .HasForeignKey(tf => tf.TreatId);

            modelBuilder.Entity<TreatFlavor>()
                .HasOne(tf => tf.Flavor)
                .WithMany(f => f.TreatFlavors)
                .HasForeignKey(tf => tf.FlavorId);
        }
    }

}
