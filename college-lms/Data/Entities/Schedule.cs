using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace college_lms.Data.Entities;

public enum Day {
    Monday,
    Tuesday,
    Wednesday,
    Thursday,
    Friday,
    Saturday,
    Sunday
}
public class Schedule
{
    [Key]
    public int Id { get; set; }

    [Required]
    public DateOnly Date { get; set; }

    [Required]
    public Day Day_of_week { get; set; }

    public List<Lesson> Lessons { get; set; } = new()
}