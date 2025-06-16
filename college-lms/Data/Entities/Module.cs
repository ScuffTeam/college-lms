namespace college_lms.Data.Entities;

public class Module : EntityBase
{
    public required string Name { get; set; }
    public required string Description { get; set; }

    public ICollection<ModulePart> ModuleParts { get; set; } = [];
}
