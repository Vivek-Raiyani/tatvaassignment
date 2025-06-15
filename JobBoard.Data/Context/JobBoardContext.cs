using JobBoard.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Data.Context;

public class JobBoardContext(DbContextOptions options) : DbContext(options)
{

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<JobApplicants> JobApplicants { get; set; }

    public virtual DbSet<JobSeeker> JobSeakers { get; set; }

    public virtual DbSet<Roles> Roles { get; set; }

    public virtual DbSet<State> States { get; set; }

    public virtual DbSet<Users> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Job>(entity =>
        {
            entity.Property(e => e.Status)
               .HasDefaultValue(false);
            entity.Property(e => e.IsDeleted)
               .HasDefaultValue(false);
        });

        modelBuilder.Entity<Users>(entity =>
        {
            entity.Property(e => e.IsDeleted)
               .HasDefaultValue(false);
            entity.Property(e => e.IsActive)
            .HasDefaultValue(true);
             entity.Property(e => e.TwoFactorAuth)
            .HasDefaultValue(false);
        });

    }

}
