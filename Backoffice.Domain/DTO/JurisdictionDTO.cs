namespace Backoffice.Domain.DTO;

public class JurisdictionDTO
{
    public int? JurisdictionId { get; set; }
    public string? Name { get; set; }
    public string? Class { get; set; }
    public int? ParentId { get; set; }
    public string? Address1 { get; set; }
    public string? Address2 { get; set; }
    public string? PostCodeZip { get; set; }
    public int? CouId { get; set; } = 138;
    public string? City { get; set; }
    public string? Notes { get; set; }
    public string? Phone { get; set; }
    public string? Code { get; set; }
    public decimal? AvailableCredit { get; set; } = 0.00m;
    public decimal? Overdraft { get; set; } = 0.00m;
    public decimal? CashInHand { get; set; } = 0.00m;
    public DateTime? LastDeposit { get; set; }
    public DateTime? LastWithdraw { get; set; }
    public decimal? TotOverdraftReceived { get; set; } = 0.00m;
    public DateTime? OverdraftStartTime { get; set; }
    public decimal? ReservedFund { get; set; } = 0.00m;
    public string? HasLiveGames { get; set; } = "1";
    public decimal? Currency { get; set; } = 1;
    public decimal? BonusCredit { get; set; } = 0.00m;
    public string? VatCode { get; set; }
    public decimal? ChildsLimit { get; set; } = 10;
    public decimal? UsersLimits { get; set; } = 30;
    public decimal? Status { get; set; } = 1;
    public DateTime CreationDate { get; set; }
    public int? CommService { get; set; } = 1;
    public int? SknId { get; set; }
    public int? TimeUtc { get; set; } = 1;
    public string? AgencyHour { get; set; }
    public string? AccessType { get; set; } = "BACKOFFICE";
    public string? GoogleMapInfo { get; set; }
    public string? ProcessorEnabled { get; set; }
    public string? Params { get; set; }
}
