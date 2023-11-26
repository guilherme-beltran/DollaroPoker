using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backoffice.Domain.Entities;

[Table("jurisdiction")]
public sealed class Jurisdiction : Entity
{
    public Jurisdiction() { }
    public Jurisdiction(int id, int sknId)
    {
        JurisdictionId = id;
        SknId = sknId;
    }


    [Key]
    [Column("JUR_ID")]
    public int JurisdictionId { get; set; }

    [Column("JUR_NAME")]
    public string? Name { get; set; }

    [Column("JUR_CLASS")]
    public string? Class { get; set; }

    [Column("JUR_PARENT_ID")]
    public int? ParentId { get; set; }

    [Column("JUR_ADDRESS1")]
    public string? Address1 { get; set; }

    [Column("JUR_ADDRESS2")]
    public string? Address2 { get; set; }

    [Column("JUR_POSTCODE_ZIP")]
    public string? PostCodeZip { get; set; }

    [Column("JUR_COU_ID")]
    public int? CouId { get; set; } = 138;

    [Column("JUR_CITY")]
    public string? City { get; set; }

    [Column("JUR_NOTES")]
    public string? Notes { get; set; }

    [Column("JUR_PHONE")]
    public string? Phone { get; set; }

    [Column("JUR_CODE")]
    public string? Code { get; set; }

    [Column("JUR_AVAILABLE_CREDIT")]
    public decimal? AvailableCredit { get; set; } = 0.00m;

    [Column("JUR_OVERDRAFT")]
    public decimal? Overdraft { get; set; } = 0.00m;

    [Column("JUR_CASH_IN_HAND")]
    public decimal? CashInHand { get; set; } = 0.00m;

    [Column("JUR_LAST_DEPOSIT")]
    public DateTime? LastDeposit { get; set; }

    [Column("JUR_LAST_WITHDRAW")]
    public DateTime? LastWithdraw { get; set; }

    [Column("JUR_TOT_OVERDRAFT_RECEIVED")]
    public decimal? TotOverdraftReceived { get; set; } = 0.00m;

    [Column("JUR_OVERDRAFT_START_TIME")]
    public DateTime? OverdraftStartTime { get; set; }

    [Column("JUR_RESERVED_FUND")]
    public decimal? ReservedFund { get; set; } = 0.00m;

    [Column("JUR_HAS_LIVE_GAMES")]
    public string? HasLiveGames { get; set; } = "1";

    [Column("JUR_CURRENCY")]
    public decimal? Currency { get; set; } = 1;

    [Column("JUR_BONUS_CREDIT")]
    public decimal? BonusCredit { get; set; } = 0.00m;

    [Column("JUR_VAT_CODE")]
    public string? VatCode { get; set; }

    [Column("JUR_CHILDS_LIMIT")]
    public decimal? ChildsLimit { get; set; } = 10;

    [Column("JUR_USERS_LIMIT")]
    public decimal? UsersLimits { get; set; } = 30;

    [Column("JUR_STATUS")]
    public decimal? Status { get; set; } = 1;

    [Column("JUR_CREATION_DATE")]
    public DateTime CreationDate { get; set; }

    [Column("JUR_COMM_SERVICE")]
    public int? CommService { get; set; } = 1;

    [Column("JUR_SKN_ID")]
    public int? SknId { get; set; }

    [Column("JUR_TIME_UTC")]
    public int? TimeUtc { get; set; } = 1;

    [Column("JUR_AGENCY_HOUR")]
    public string? AgencyHour { get; set; }

    [Column("JUR_ACCESS_TYPE")]
    public string? AccessType { get; set; } = "BACKOFFICE";

    [Column("JUR_GOOGLE_MAP_INFO")]
    public string? GoogleMapInfo { get; set; }

    [Column("JUR_PROCESSOR_ENABLED")]
    public string? ProcessorEnabled { get; set; }

    [Column("JUR_PARAMS")]
    public string? Params { get; set; }
}
