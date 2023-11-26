using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backoffice.Domain.Entities;


[Table("skins")]
public class Skin : Entity
{
    public Skin() { }
    public Skin(string name)
    {
        Name = name;
    }

    [Key]
    [Column("SKN_ID")]
    public int SkinId { get; set; }

    [Required]
    [Column("SKN_NAME")]
    public string? Name { get; set; }

    [Column("SKN_URL")]
    public string? Url { get; set; }

    [Column("SKN_MEDIA_URL")]
    public string? MediaUrl { get; set; }

    [Required]
    [Column("SKN_GAME_LAUNCHER")]
    public string? GameLauncher { get; set; }

    [Column("SKN_BETTING_URL")]
    public string? BettingUrl { get; set; }

    [Column("SKN_BACKOFFICE_URL")]
    public string? BackofficeUrl { get; set; }

    [Column("SKN_REG_ACTIVATION_URL")]
    public string? RegistrationActivationUrl { get; set; }

    [Required]
    [Column("SKN_FOLDERNAME")]
    public string? FolderName { get; set; }

    [Required]
    [Column("SKN_STATUS")]
    public string? Status { get; set; }

    [Column("SKN_ALLOWIP")]
    public string? AllowIp { get; set; }

    [Required]
    [Column("SKN_JUR_ID")]
    public int? JurisdictionId { get; set; }

    [Column("SKN_KEY")]
    public string? Key { get; set; }

    [Column("SKN_INTERNET_CLUB_ID")]
    public int? InternetClubId { get; set; }

    [Column("SKN_EMAIL")]
    public string? Email { get; set; }

    [Column("SKN_SMTP")]
    public string? Smtp { get; set; }

    [Column("SKN_EMAIL_REG_TEMPLATE", TypeName = "text")]
    public string? EmailRegistrationTemplate { get; set; }

    [Column("SKN_PES_CODE_ENABLED")]
    public string? PesCodeEnabled { get; set; }

    [Column("SKN_PARAMS")]
    public string? Params { get; set; }

    [Column("SKN_GATEWAY_RESPONSE_URL")]
    public string? GatewayResponseUrl { get; set; }

    [Column("SKN_KIOSK_URL")]
    public string? KioskUrl { get; set; }

    [Column("SKN_PARENT_SKINS")]
    public string? ParentSkins { get; set; }

    [Column("SKN_DOMAIN")]
    public string? Domain { get; set; }

    [Column("SKN_BANNER_URL")]
    public string? BannerUrl { get; set; }

    [Column("SKN_LANGUAGE")]
    public string? Language { get; set; }

    // Relacionamento com a tabela Jurisdiction
    [ForeignKey("JurisdictionId")]
    public Jurisdiction? Jurisdiction { get; set; }
}
