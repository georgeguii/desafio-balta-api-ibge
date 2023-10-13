using Microsoft.EntityFrameworkCore;

using Desafio_Balta_IBGE.Domain.Models;

namespace Desafio_Balta_IBGE.Infra.Data.Context;

public class IbgeContext : DbContext
{
    public IbgeContext(DbContextOptions<IbgeContext> options) : base(options) { }

    public DbSet<Ibge> Ibge { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IbgeContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
