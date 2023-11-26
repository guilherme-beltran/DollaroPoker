using Backoffice.Domain.Entities;
using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;

namespace Backoffice.Infra.Contexts.Backoffice
{
    public class BackofficeContext : DbContext, IBackofficeContext
    {
        public BackofficeContext(DbContextOptions<BackofficeContext> options)
            : base(options)
        { }


        public DbSet<User> Users { get; set; }
        public DbSet<Punter> Punters { get; set; }
        public DbSet<TypeUser> TypeUsers { get; set; }
        public DbSet<Jurisdiction> Jurisdictions { get; set; }
        public DbSet<Sequence> Sequences { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Notification>();
            modelBuilder.Ignore<Entity>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
