using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace college_lms.Data.Entities;


public enum Status
{
    Active,
    Passed,
    Verified,
    Expired
}

public class Homework
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int Lesson_id { get; set; }
    public Lesson Lesson { get; set; }

    public string? Comment { get; set; }

    [Required]
    public string Exercise { get; set; }

    public string? Solution { get; set; }

    [Required]
    public Status Status { get; set; }

    [Required]
    public DateOnly Term { get; set; }

==