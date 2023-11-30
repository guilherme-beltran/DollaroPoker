using Backoffice.Domain.Extensions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backoffice.Domain.Entities;

[Table("admin_user")]
public sealed class User : Entity
{
    public User() {}

    public User(int id, string username, string? password, Jurisdiction jurisdiction)
    {
        UserId = id;
        Username = username;
        Password = password;
        Jurisdiction = jurisdiction;
        Name = username;
        CreationTime = DateTime.UtcNow;
        Access = "ALLOW";
        Credit = 5000;
    }

    [Key]
    [Column("AUS_ID")]
    public int UserId { get; set; }

    [Required]
    [Column("AUS_NAME", TypeName = "varchar(50)")]
    public string? Name { get; set; }

    [Column("AUS_LASTNAME", TypeName = "varchar(40)")]
    public string? LastName { get; set; }

    [Column("AUS_USERNAME", TypeName = "varchar(50)")]
    public string? Username { get; set; }

    [Column("AUS_PASSWORD", TypeName = "varchar(50)")]
    public string? Password { get; set; }

    [Column("AUS_ATY_ID")]
    public int? TypeId { get; set; }

    [Column("AUS_ACCESS", TypeName = "varchar(5)")]
    public string? Access { get; set; } = "DENY";

    [Column("AUS_CREATION_TIME")]
    public DateTime? CreationTime { get; set; }

    [Column("AUS_LAST_LOGGED_IN")]
    public DateTime? LastLoggedIn { get; set; }

    [Column("AUS_HAS_LOGGED_ID")]
    public int? HasLoggedId { get; set; }

    [Column("AUS_EMAIL", TypeName = "varchar(255)")]
    public string? Email { get; set; }

    [Column("AUS_DAILY_DEPOSITS_LIMIT", TypeName = "decimal(10, 2)")]
    public decimal? DailyDepositsLimit { get; set; } = 500.00m;

    [Column("AUS_NUM_LOGINS")]
    public int? NumLogins { get; set; }

    [Column("AUS_ADDRESS1", TypeName = "varchar(255)")]
    public string? Address1 { get; set; }

    [Column("AUS_ADDRESS2", TypeName = "varchar(255)")]
    public string? Address2 { get; set; }

    [Column("AUS_CITY_SUBURB", TypeName = "varchar(100)")]
    public string? CitySuburb { get; set; }

    [Column("AUS_STATE_PROVINCE", TypeName = "varchar(100)")]
    public string? StateProvince { get; set; }

    [Column("AUS_POSTCODE_ZIP", TypeName = "varchar(50)")]
    public string? PostcodeZip { get; set; }

    [Column("AUS_PHONE_HOME", TypeName = "varchar(50)")]
    public string? PhoneHome { get; set; }

    [Column("AUS_PHONE_WORK", TypeName = "varchar(50)")]
    public string? PhoneWork { get; set; }

    [Column("AUS_PHONE_MOBILE", TypeName = "varchar(50)")]
    public string? PhoneMobile { get; set; }

    [Column("AUS_COU_ID")]
    public int? CountryId { get; set; }

    [Column("AUS_NOTES", TypeName = "varchar(1024)")]
    public string? Notes { get; set; }

    [Column("AUS_SALES_CODE", TypeName = "varchar(25)")]
    public string? SalesCode { get; set; }

    [Column("AUS_VAT_CODE", TypeName = "varchar(40)")]
    public string? VatCode { get; set; }

    [Column("AUS_BANK_ACCOUNT", TypeName = "varchar(100)")]
    public string? BankAccount { get; set; }

    [Column("AUS_JURISDICTION_ID")]
    public int? JurisdictionId { get; set; }

    [Column("AUS_LOCK_START")]
    public DateTime? LockStart { get; set; }

    [Column("AUS_FAILED_LOGINS")]
    public int? FailedLogins { get; set; } = 0;

    [Column("AUS_PASSWORD_SET_TIME")]
    public DateTime? PasswordSetTime { get; set; }

    [Column("AUS_SSL_KEY", TypeName = "varchar(2048)")]
    public string? SslKey { get; set; }

    [Column("AUS_HAS_KEY")]
    public int? HasKey { get; set; } = 0;

    [Column("AUS_SECUR_CHANGE_PASS")]
    public int? SecureChangePass { get; set; } = 1;

    [Column("AUS_ADDRESS_IP", TypeName = "varchar(20)")]
    public string? AddressIp { get; set; }

    [Column("AUS_SESS_ID", TypeName = "varchar(50)")]
    public string? SessionId { get; set; }

    [NotMapped]
    public int Credit { get; private set; } = 0;

    [ForeignKey("JurisdictionId")]
    public Jurisdiction Jurisdiction { get; set; }
    
    [ForeignKey("TypeId")]
    public TypeUser TypeUser { get; set; }

    public void Encrypt()
    {
        if (Password is null)
        {
            AddNotification("User.Password","Invalid password.");
            return;
        }

        Password = Password!.Trim().EncryptUsingSHA256();
    }

    public bool VerifyPassword(string password)
    {
        return Password == password;
    }

    public void DepositCredit(Punter punter,
                              int amount,
                              string? notes = null)
    {
        if (punter is null)
        {
            AddNotification("User.DepositCredit", "Invalid Punter");
            return;
        }

        if (amount <= 0)
        {
            AddNotification("User.DepositCredit", "It's not allowed to deposit credit less than 0");
            return;
        }

        if (amount > Credit)
        {
            AddNotification("User.DepositCredit", "Account without balance");
            return;
        }

        DecreaseCredit(amount);
        punter!.ReceiveCredit(amount, notes);

    }

    public void WithdrawCredit(Punter punter,
                               int credit,
                               string? notes = null)
    {
        if (punter is null)
        {
            AddNotification("User.DepositCredit", "Invalid Punter");
        }

        punter!.TransferCredit(credit, notes);
        AddCredit(credit);
    }

    public void AddCredit(int credit) => Credit += credit;

    public void DecreaseCredit(int credit)
    {
        Credit -= credit;
    }
}
