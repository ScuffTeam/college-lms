using System.ComponentModel.DataAnnotations;

namespace college_lms.Data.Entities;

public class Room : EntityBase
{
    [Required]
    [MaxLength(255)]
    public string Name { get; set; }

    [Required]
    [MaxLength(255)]
    public string Building { get; set; }

    public ICollection<Lesson> Lessons { get; set; } = [];
}
