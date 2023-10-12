using Desafio_Balta_IBGE.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Desafio_Balta_IBGE.Infra.Data.Mapping;

public sealed class IbgeMap : IEntityTypeConfiguration<Ibge>
{
    public void Configure(EntityTypeBuilder<Ibge> builder)
    {
        builder.ToTable("Ibge");

        builder.Ignore(x => x.Id);

        builder.HasKey(x => x.IbgeId);

        builder.Property(x => x.State)
            .HasColumnName("State")
            .HasColumnType("char(2)")
            .IsRequired();

        builder.Property(x => x.City)
            .HasColumnName("City")
            .HasColumnType("varchar(50)")
            .IsRequired();

    }
}
