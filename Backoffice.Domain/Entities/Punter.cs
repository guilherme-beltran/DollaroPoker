using Backoffice.Domain.Extensions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backoffice.Domain.Entities;

[Table("punter")]
public sealed class Punter : Entity
{
    public Punter() { }

    private Punter(int id, string firstname, string middlename, string lastname, string username, string? password, Jurisdiction jurisdiction)
    {
        PunterId = id;
        FirstName = firstname;
        MiddleName = middlename;
        LastName = lastname;
        Username = username;
        Password = password;
        Jurisdiction = jurisdiction;
        RegistrationDate = DateTime.UtcNow;
        Access = "ALLOW";
    }

    [Key]
    [Column("PUN_ID")]
    public int PunterId { get; set; }

    [Column("PUN_TITLE")]
    public string? Title { get; set; }

    [Column("PUN_FIRST_NAME")]
    public string? FirstName { get; set; }

    [Column("PUN_MIDDLE_NAME")]
    public string? MiddleName { get; set; }

    [Column("PUN_LAST_NAME")]
    public string? LastName { get; set; }

    [Column("PUN_USERNAME")]
    public string? Username { get; set; }

    [Column("PUN_PASSWORD")]
    public string? Password { get; set; }

    [Column("PUN_DOB")]
    public DateTime? DateOfBirth { get; set; }

    [Column("PUN_MEMBER_TYPE")]
    public string? MemberType { get; set; } = "NONFINANCIAL";

    [Column("PUN_CITY_SUBURB")]
    public string? CitySuburb { get; set; }

    [Column("PUN_CITY_OF_BIRTH")]
    public string? CityOfBirth { get; set; }

    [Column("PUN_PROFESSION")]
    public string? Profession { get; set; }

    [Column("PUN_STATE_PROVINCE")]
    public string? StateProvince { get; set; }

    [Column("PUN_POSTCODE_ZIP")]
    public string? PostcodeZip { get; set; }

    [Column("PUN_PHONE_BUSINESS")]
    public string? PhoneBusiness { get; set; }

    [Column("PUN_PHONE_HOME")]
    public string? PhoneHome { get; set; }

    [Column("PUN_PHONE_MOBILE")]
    public string? PhoneMobile { get; set; }

    [Column("PUN_FAX")]
    public string? Fax { get; set; }

    [Column("PUN_REG_DATE")]
    public DateTime? RegistrationDate { get; set; }

    [Column("PUN_LAST_LOGGED_IN")]
    public DateTime? LastLoggedIn { get; set; }

    [Column("PUN_ACCESS")]
    public string? Access { get; set; } = "deny";

    [Column("PUN_NIN_CODE")]
    public string? NinCode { get; set; }

    [Column("PUN_SRE_CODE")]
    public string? SreCode { get; set; }

    [Column("PUN_EMAIL")]
    public string? Email { get; set; }

    [Column("PUN_NONFINANCIAL_REG_DATE")]
    public DateTime? NonFinancialRegDate { get; set; }

    [Column("PUN_FINANCIAL_REG_DATE")]
    public DateTime? FinancialRegDate { get; set; }

    [Column("PUN_GENDER")]
    public string? Gender { get; set; }

    [Column("PUN_SECRET_ANSWER")]
    public string? SecretAnswer { get; set; }

    [Column("PUN_DAILY_ALLOWANCE")]
    public int? DailyAllowance { get; set; } = 500;

    [Column("PUN_CUSTOMER_NUMBER")]
    public int? CustomerNumber { get; set; }

    [Column("PUN_IDENTIFIED")]
    public int? Identified { get; set; } = -1;

    [Column("PUN_INVESTIGATE")]
    public int? Investigate { get; set; } = 0;

    [Column("PUN_REGISTRATION_STATUS")]
    public string? RegistrationStatus { get; set; }

    [Column("PUN_LAST_IP_USED")]
    public string? LastIpUsed { get; set; }

    [Column("PUN_TEMP_PASSWORD")]
    public string? TempPassword { get; set; }

    [Column("PUN_NUM_LOGINS")]
    public int? NumLogins { get; set; } = 0;

    [Column("PUN_NUM_FAILED_LOGINS")]
    public int? NumFailedLogins { get; set; } = 0;

    [Column("PUN_LOCK_REASON")]
    public string? LockReason { get; set; }

    [Column("PUN_CONNECTION_TYPE")]
    public string? ConnectionType { get; set; } = "unknown";

    [Column("PUN_ACTIVATION_METHOD")]
    public string? ActivationMethod { get; set; }

    [Column("PUN_SMS_REG_CODE")]
    public string? SmsRegCode { get; set; }

    [Column("PUN_EMAIL_REG_CODE")]
    public string? EmailRegCode { get; set; }

    [Column("PUN_CONFIRMATION_LIST")]
    public string? ConfirmationList { get; set; }

    [Column("PUN_LAST_REQUEST_TIME")]
    public DateTime? LastRequestTime { get; set; }

    [Column("PUN_PRE_REGISTERED")]
    public string? PreRegistered { get; set; }

    [Column("PUN_INTERNAL_CUSTOMER")]
    public int? InternalCustomer { get; set; } = 0;

    [Column("PUN_PREG_ACT_ATTEMPTS")]
    public int? PregActAttempts { get; set; }

    [Column("PUN_PREG_CODE")]
    public string? PregCode { get; set; }

    [Column("PUN_ID_DOCUMENTS")]
    public string? IdDocuments { get; set; }

    [Column("PUN_LOGIN_LOCK_START")]
    public DateTime? LoginLockStart { get; set; }

    [Column("PUN_ADDRESS_LINE1")]
    public string? AddressLine1 { get; set; }

    [Column("PUN_ADDRESS_LINE2")]
    public string? AddressLine2 { get; set; }

    [Column("PUN_LAST_LOGGED_OUT")]
    public DateTime? LastLoggedOut { get; set; }

    [Column("PUN_PASSWORD_SET_TIME")]
    public DateTime? PasswordSetTime { get; set; }

    [Column("PUN_COU_CODE")]
    public string? CouCode { get; set; }

    [Column("PUN_COU_ID")]
    public int? CouId { get; set; }

    [Column("PUN_BETTING_CLUB")]
    public int? FkJurisdiction { get; set; }

    [Column("PUN_LANG")]
    public string? Lang { get; set; }

    [Column("PUN_SESS_ID")]
    public string? SessId { get; set; }

    [Column("PUN_PREFERENCES")]
    public string? Preferences { get; set; }

    [Column("PUN_SITE_ID")]
    public int? SiteId { get; set; } = 0;

    [Column("PUN_SOURCE_ID")]
    public int? SourceId { get; set; }

    [Column("PUN_DAILY_DEPOSITS_LIMIT")]
    public decimal? DailyDepositsLimit { get; set; } = 500.00m;

    [Column("PUN_SECRET_QUESTION")]
    public string? SecretQuestion { get; set; }

    [Column("PUN_NOTES")]
    public string? Notes { get; set; }

    [Column("PUN_ACCOUNT_DATA")]
    public string? AccountData { get; set; }

    [Column("PUN_POB")]
    public string? Pob { get; set; }

    [Column("PUN_BETTING_TYPE")]
    public int? BettingType { get; set; } = 1;

    [Column("PUN_AFF_ID")]
    public int? AffId { get; set; }

    [Column("PUN_AUS_ID")]
    public int? FkUser { get; set; }

    [Column("PUN_BETTING_PREFERENCES")]
    public string? BettingPreferences { get; set; }

    [NotMapped]
    public int Credit { get; private set; } = 0;

    [ForeignKey("FkJurisdiction")]
    public Jurisdiction? Jurisdiction { get; set; }

    [ForeignKey("FkUser")]
    public User? User { get; set; }

    public static Punter Create(int id, string firstname, string middlename, string lastname, string username, string? password, Jurisdiction jurisdiction) 
        => new(id: id,
               firstname: firstname,
               middlename: middlename,
               lastname: lastname,
               username: username,
               password: password,
               jurisdiction: jurisdiction);

    public void Encrypt()
    {
        if (Password is null)
        {
            AddNotification("Punter.Password", "Invalid password.");
            return;
        }

        Password = Password!.Trim().EncryptUsingSHA256();
    }

    public bool VerifyPassword(string password)
    {
        return Password == password.EncryptUsingSHA256();
    }

}
