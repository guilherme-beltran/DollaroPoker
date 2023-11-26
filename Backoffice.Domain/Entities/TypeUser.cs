using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backoffice.Domain.Entities;

[Table("admin_user_type")]
public sealed class TypeUser : Entity
{
    public TypeUser() {}
    
    [Key]
    [Column("ATY_ID")]
    public int TypeUserId { get; set; }

    [Column("ATY_NAME")]
    public string? Name { get; set; }

    [Column("ATY_CODE")]
    public string? Code { get; set; }

    [Column("ATY_LEVEL")]
    public int? Level { get; set; }

    [Column("ATY_JURISDICTION_CLASS")]
    public string? JurisdictionClass { get; set; }
}
