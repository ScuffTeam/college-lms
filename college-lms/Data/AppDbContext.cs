using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using college_lms.Data.Entities;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : IdentityDbContext<User>(options)
{
    public DbSet<AttendanceMark> AttendanceMarks { get; set; } = null!;

    public DbSet<Group> Groups { get; set; } = null!;

    public DbSet<Homework> Homeworks { get; set; } = null!;

    public DbSet<Lesson> Lessons { get; set; } = null!;

    public DbSet<Room> Rooms { get; set; } = null!;

    public DbSet<Schedule> Schedules { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Room>()
                .HasIndex(r => r.Name)
                .IsUnique();
        modelBuilder.Entity<Group>()
                .HasIndex(g => g.Name)
                .IsUnique();
        modelBuilder.Entity<Group>()
                .HasMany(g => g.Users)
                .WithMany(u => u.Groups)
                .UsingEntity(ug => ug.ToTable("User_Group"));
        modelBuilder.Entity<Group>()
                .HasMany(g => g.Lessons)
                .WithMany(l => l.Groups)
                .UsingEntity(gl => gl.ToTable("Group_Lesson"));
        modelBuilder.Entity<AttendanceMark>()
                .HasOne(a => a.User)
                .WithMany(u => u.AttendanceMarks)
                .HasForeignKey(a => a.Student_id)
                .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<AttendanceMark>()
                .HasOne(a => a.Lesson)
                .WithMany(l => l.AttendanceMarks)
                .OnDelete(DeleteBehavior.SetNull);
        modelBuilder.Entity<Lesson>()
                .HasOne(l => l.User)
                .WithMany(u => u.Lessons)
                .HasForeignKey(l => l.Teacher_id)
                .OnDelete(DeleteBehavior.SetNull);
        modelBuilder.Entity<Lesson>()
                .HasOne(l => l.Schedule)
                .WithMany(s => s.Lessons)
                .HasForeignKey(l => l.Schedule_id)
                .OnDelete(DeleteBehavior.SetNull);
        modelBuilder.Entity<Lesson>()
                .HasOne(l => l.Room)
                .WithMany(r => r.Lessons)
                .HasForeignKey(l => l.Room_id)
                .OnDelete(DeleteBehavior.SetNull);
        modelBuilder.Entity<Homework>()
                .HasOne(h => h.Lesson)
                .WithMany(l => l.Homeworks)
                .HasForeignKey(h => h.Lesson_id)
                .OnDelete(DeleteBehavior.SetNull);
    }
}
