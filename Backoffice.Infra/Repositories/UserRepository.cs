using Backoffice.Domain.DTO;
using Backoffice.Domain.Entities;
using Backoffice.Domain.Interfaces.Repositories;
using Backoffice.Infra.Contexts.Backoffice;
using Microsoft.EntityFrameworkCore;

namespace Backoffice.Infra.Repositories;

internal sealed class UserRepository : IUserRepository
{
    private readonly BackofficeContext _context;

    public UserRepository(BackofficeContext context) => _context = context;

    public async Task<IEnumerable<UserDTO>> GetAll()
    {
        var users = await _context
                          .Users
                          .AsNoTracking()
                          .Include(j => j.Jurisdiction)
                          .Select(u => new UserDTO
                          {
                              Id = u.UserId,
                              Name = u.Name,
                              LastName = u.LastName,
                              Username = u.Username,
                              Password = u.Password,
                              TypeId = u.TypeId,
                              Access = u.Access,
                              CreationTime = u.CreationTime,
                              LastLoggedIn = u.LastLoggedIn,
                              HasLoggedId = u.HasLoggedId,
                              Email = u.Email,
                              DailyDepositsLimit = u.DailyDepositsLimit,
                              NumLogins = u.NumLogins,
                              Address1 = u.Address1,
                              Address2 = u.Address2,
                              CitySuburb = u.CitySuburb,
                              StateProvince = u.StateProvince,
                              PostcodeZip = u.PostcodeZip,
                              PhoneHome = u.PhoneHome,
                              PhoneWork = u.PhoneWork,
                              PhoneMobile = u.PhoneMobile,
                              CountryId = u.CountryId,
                              Notes = u.Notes,
                              SalesCode = u.SalesCode,
                              VatCode = u.VatCode,
                              BankAccount = u.BankAccount,
                              JurisdictionId = u.JurisdictionId,
                              LockStart = u.LockStart,
                              FailedLogins = u.FailedLogins,
                              PasswordSetTime = u.PasswordSetTime,
                              SslKey = u.SslKey,
                              HasKey = u.HasKey,
                              SecureChangePass = u.SecureChangePass,
                              AddressIp = u.AddressIp,
                              SessionId = u.SessionId,
                              JurisdictionDTO = new JurisdictionDTO
                              {
                                  JurisdictionId = u.Jurisdiction.JurisdictionId,
                                  Name = u.Jurisdiction.Name,
                                  Class = u.Jurisdiction.Class,
                                  ParentId = u.Jurisdiction.ParentId,
                                  Address1 = u.Jurisdiction.Address1,
                                  Address2 = u.Jurisdiction.Address2,
                                  PostCodeZip = u.Jurisdiction.PostCodeZip,
                                  CouId = u.Jurisdiction.CouId,
                                  City = u.Jurisdiction.City,
                                  Notes = u.Jurisdiction.Notes,
                                  Phone = u.Jurisdiction.Phone,
                                  Code = u.Jurisdiction.Code,
                                  AvailableCredit = u.Jurisdiction.AvailableCredit,
                                  Overdraft = u.Jurisdiction.Overdraft,
                                  CashInHand = u.Jurisdiction.CashInHand,
                                  LastDeposit = u.Jurisdiction.LastDeposit,
                                  LastWithdraw = u.Jurisdiction.LastWithdraw,
                                  TotOverdraftReceived = u.Jurisdiction.TotOverdraftReceived,
                                  OverdraftStartTime = u.Jurisdiction.OverdraftStartTime,
                                  ReservedFund = u.Jurisdiction.ReservedFund,
                                  HasLiveGames = u.Jurisdiction.HasLiveGames,
                                  Currency = u.Jurisdiction.Currency,
                                  BonusCredit = u.Jurisdiction.BonusCredit,
                                  VatCode = u.Jurisdiction.VatCode,
                                  ChildsLimit = u.Jurisdiction.ChildsLimit,
                                  UsersLimits = u.Jurisdiction.UsersLimits,
                                  Status = u.Jurisdiction.Status,
                                  CreationDate = u.Jurisdiction.CreationDate,
                                  CommService = u.Jurisdiction.CommService,
                                  SknId = u.Jurisdiction.SknId,
                                  TimeUtc = u.Jurisdiction.TimeUtc,
                                  AgencyHour = u.Jurisdiction.AgencyHour,
                                  AccessType = u.Jurisdiction.AccessType,
                                  GoogleMapInfo = u.Jurisdiction.GoogleMapInfo,
                                  ProcessorEnabled = u.Jurisdiction.ProcessorEnabled,
                                  Params = u.Jurisdiction.Params
                              }
                          })
                          .ToListAsync();

        return users;
    }

    public async Task<User?> GetByIdAsync(int id)
        => await _context
                    .Users
                    .Where(x => x.UserId == id)
                    .FirstOrDefaultAsync();

    public async Task<User> GetByUsernameAsync(string username)
        => await _context
                    .Users
                    .Where(x => x.Username == username)
                    .FirstOrDefaultAsync();

    public async Task<bool> IsRegisteredAsync(string username)
        => await _context
             .Users
             .AnyAsync(x => x.Username == username);

    public async Task Insert(User user, CancellationToken cancellationToken) => await _context.Users.AddAsync(user, cancellationToken);

}
