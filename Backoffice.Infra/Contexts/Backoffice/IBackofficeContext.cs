using Backoffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backoffice.Infra.Contexts.Backoffice;

public interface IBackofficeContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<TypeUser> TypeUsers { get; set; }
    public DbSet<Punter> Punters { get; set; }
    public DbSet<Jurisdiction> Jurisdictions { get; set; }
    public DbSet<Sequence> Sequences { get; set; }
}
