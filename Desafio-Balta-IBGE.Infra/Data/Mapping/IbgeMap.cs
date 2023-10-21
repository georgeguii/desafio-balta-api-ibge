using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Desafio_Balta_IBGE.Domain.Models;

namespace Desafio_Balta_IBGE.Infra.Data.Mapping;

public sealed class IbgeMap : IEntityTypeConfiguration<Ibge>
{
    public void Configure(EntityTypeBuilder<Ibge> builder)
    {
        builder.ToTable("Ibge");

        builder.Ignore(x => x.Id);
        builder.Ignore(x => x.IsValid);
        builder.Ignore(x => x.Errors);

        builder.HasKey(x => x.IbgeId);

        builder.Property(x => x.IbgeId)
            .HasColumnName("IbgeId")
            .HasColumnType("char(7)")
            .IsRequired();

        builder.Property(x => x.State)  
            .HasColumnName("State")
            .HasColumnType("char(2)")
            .IsRequired()
            // Sempre que salvar um novo valor no banco, será salvo em ToUpper, a segunda expression serve para mapear o retorno,
            // mas como não ocorre nenhuma modificação, somente retorna
            .HasConversion(state => state.ToUpper(), state => state);

        builder.Property(x => x.City)
            .HasColumnName("City")
            .HasColumnType("varchar(80)")
            .IsRequired();

    }
}
