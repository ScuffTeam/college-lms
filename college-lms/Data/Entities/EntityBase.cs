using System.ComponentModel.DataAnnotations;
using college_lms.Data.Entities.Interfaces;

namespace college_lms.Data.Entities;

public abstract class EntityBase : IWithTimestamps, IIdentifiable
{
    [Key]
    public int Id { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
