using System.ComponentModel.DataAnnotations;

namespace college_lms.Data.Entities;

public class Group : EntityBase
{
    [Required]
    [MaxLength(255)]
    public required string Name { get; set; }

    public ICollection<User> Users { get; set; } = [];

    public ICollection<Lesson> Lessons { get; set; } = [];
}
