using BikeRental.Entities.DBEtities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BikeRental.Repository
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Bike> Bikes { get; set; }
        public DbSet<BikeRating> BikeRatings { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        private void ConfigureBike(EntityTypeBuilder<Bike> builder)
        {
            builder.Property(b => b.PerDayRate).HasColumnType("decimal(18,2)");
            builder.Property(b => b.IsAvailable).HasDefaultValue(true);
        }
        private void ConfigureReservation(EntityTypeBuilder<Reservation> builder)
        {
            builder.Property(b => b.PerDayRate).HasColumnType("decimal(18,2)");
            builder.Property(b => b.Total).HasColumnType("decimal(18,2)");
            builder.Property(b => b.IsCancled).HasDefaultValue(false);
        }

        #region On Model Creating
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Bike>(ConfigureBike);
            builder.Entity<Reservation>(ConfigureReservation);
            //SeedData.Seed(builder);
        }
        #endregion
    }
}
