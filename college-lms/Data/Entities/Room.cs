using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace college_lms.Data.Entities;


public class Room
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Name { get; set; }

    [Required]
    [MaxLength(255)]
    public string Building { get; set; }

    public DateTime Created_At { get; set; }
    public DateTime UpdatedAt { get; set; }

    public List<Lesson> Lessons { get; set; } = new();
}