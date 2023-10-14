using Microsoft.EntityFrameworkCore;
using Desafio_Balta_IBGE.Domain.Models;
using Desafio_Balta_IBGE.Shared.Entities;
using Desafio_Balta_IBGE.Domain.ValueObjects;
using Desafio_Balta_IBGE.Infra.Data.Mapping;

namespace Desafio_Balta_IBGE.Infra.Data.Context;

public class IbgeContext : DbContext
{
    public IbgeContext(DbContextOptions<IbgeContext> options) : base(options) { }

    public DbSet<Ibge> Ibge { get; set; }
    public DbSet<User> User { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<Entity>();
        modelBuilder.Ignore<VerifyEmail>();

        //modelBuilder.ApplyConfiguration(new UserMap());
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IbgeContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
