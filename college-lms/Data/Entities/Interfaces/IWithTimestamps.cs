namespace college_lms.Data.Entities.Interfaces;

public interface IWithTimestamps
{
    DateTime CreatedAt { get; set; }
    DateTime UpdatedAt { get; set; }
}
