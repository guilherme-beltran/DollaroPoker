namespace Backoffice.Domain.DTO;

public record UserDTO
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public int? TypeId { get; set; }
    public string? Access { get; set; } = "DENY";
    public DateTime? CreationTime { get; set; }
    public DateTime? LastLoggedIn { get; set; }
    public int? HasLoggedId { get; set; }
    public string? Email { get; set; }
    public decimal? DailyDepositsLimit { get; set; } = 500.00m;
    public int? NumLogins { get; set; }
    public string? Address1 { get; set; }
    public string? Address2 { get; set; }
    public string? CitySuburb { get; set; }
    public string? StateProvince { get; set; }
    public string? PostcodeZip { get; set; }
    public string? PhoneHome { get; set; }
    public string? PhoneWork { get; set; }
    public string? PhoneMobile { get; set; }
    public int? CountryId { get; set; }
    public string? Notes { get; set; }
    public string? SalesCode { get; set; }
    public string? VatCode { get; set; }
    public string? BankAccount { get; set; }
    public int? JurisdictionId { get; set; }
    public DateTime? LockStart { get; set; }
    public int? FailedLogins { get; set; } = 0;
    public DateTime? PasswordSetTime { get; set; }
    public string? SslKey { get; set; }
    public int? HasKey { get; set; } = 0;
    public int? SecureChangePass { get; set; } = 1;
    public string? AddressIp { get; set; }
    public string? SessionId { get; set; }
    public JurisdictionDTO? JurisdictionDTO { get; set; }
    public TypeUserDTO? TypeUserDTO { get; set; }
}
