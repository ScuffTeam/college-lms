using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace college_lms.Data.Entities;

public class Group
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Name { get; set; }

    public string? Description { get; set; }

    public DateTime Created_At { get; set; }
    public DateTime UpdatedAt { get; set; }

    public List<User> Users { get; set; } = new();

    public List<Lesson> Lessons { get; set; } = new();
}