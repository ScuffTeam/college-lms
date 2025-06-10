using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace college_lms.Data.Entities;


public class Lesson
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int TeacherId { get; set; }
    public required User User { get; set; }

    [Required]
    public int GroupId { get; set; }

    [Required]
    public int RoomId { get; set; }
    public required Room Room { get; set; }

    [MaxLength(255)]
    public string? Topic { get; set; }

    public string? Description { get; set; }

    [Required]
    public TimeOnly TimeStart { get; set; }

    [Required]
    public TimeOnly TimeEnd { get; set; }

    [Required]
    public int ScheduleId { get; set; }
    public required Schedule Schedule { get; set; }

    [Required]
    public int PairNumber { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public List<Homework> Homeworks { get; set; } = new();

    public List<AttendanceMark> AttendanceMarks { get; set; } = new();

    public List<Group> Groups { get; set; } = new();
}