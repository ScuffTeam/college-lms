using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace college_lms.Data.Entities;


public class Lesson
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int Teacher_id { get; set; }
    public User User { get; set; }

    [Required]
    public int Group_id { get; set; }

    [Required]
    public int Room_id { get; set; }
    public Room Room { get; set; }

    [MaxLength(255)]
    public string? Topic { get; set; }

    public string? Description { get; set; }

    [Required]
    public TimeOnly Time_Start { get; set; }

    [Required]
    public TimeOnly Time_end { get; set; }

    [Required]
    public int Schedule_id { get; set; }
    public Schedule Schedule { get; set; }

    [Required]
    public int Pair_number { get; set; }

    public DateTime Created_At { get; set; }
    public DateTime UpdatedAt { get; set; }

    public List<Homework> Homeworks { get; set; } = new();

    public List<AttendanceMark> AttendanceMarks { get; set; } = new();

    public List<Group> Groups { get; set; } = new();
}