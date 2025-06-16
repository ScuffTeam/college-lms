using college_lms.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace college_lms.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : IdentityDbContext<User, IdentityRole<int>, int>(options)
{
    public DbSet<AttendanceMark> AttendanceMarks { get; set; } = null!;

    public DbSet<Group> Groups { get; set; } = null!;

    public DbSet<Homework> Homeworks { get; set; } = null!;

    public DbSet<Lesson> Lessons { get; set; } = null!;

    public DbSet<Room> Rooms { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Room>()
            .HasIndex(r => new { r.Building, r.Name })
            .HasDatabaseName("IX_Room_Name_Building")
            .IsUnique();
        modelBuilder.Entity<Group>().HasIndex(g => g.Name).IsUnique();
        modelBuilder
            .Entity<Group>()
            .HasMany(g => g.Users)
            .WithMany(u => u.Groups)
            .UsingEntity(ug => ug.ToTable("User_Group"));
        modelBuilder
            .Entity<Group>()
            .HasMany(g => g.Lessons)
            .WithMany(l => l.Groups)
            .UsingEntity(gl => gl.ToTable("Group_Lesson"));
    }
}
