using System.ComponentModel.DataAnnotations.Schema;

namespace college_lms.Data.Entities;

public class ModulePart : EntityBase
{
    [ForeignKey(nameof(Module))]
    public int ModuleId { get; set; }
    public required Module Module { get; set; }
    public required string Title { get; set; }
}
