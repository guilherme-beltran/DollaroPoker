using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backoffice.Domain.Entities;

[Table("sequences")]
public class Sequence : Entity
{
    public Sequence() { }

    [Key]
    [Column("name", TypeName = "varchar(22)")]
    public string Name { get; set; }

    [Column("seq")]
    public int NextSequence { get; set; }

    public void UpdateSequence()
    {
        NextSequence++; 
    }
}
