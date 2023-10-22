using Desafio_Balta_IBGE.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Desafio_Balta_IBGE.Infra.Data.Mapping
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.Ignore(x => x.Id);
            builder.Ignore(x => x.IsValid);
            builder.Ignore(x => x.Errors);

            builder.HasKey(x => x.UserId);

            #region Name

            builder.Property(x => x.Name)
                .HasColumnName("Name")
                .HasColumnType("varchar")
                .HasMaxLength(60)
                .IsRequired(true);

            #endregion

            #region Password

            builder.OwnsOne(x => x.Password)
                .Ignore(x => x.IsValid);

            builder.OwnsOne(x => x.Password)
                .Ignore(x => x.Errors);

            builder.OwnsOne(x => x.Password)
                .Property(x => x.Hash)
                .HasColumnName("Password")
                .HasColumnType("varchar")
                .HasMaxLength(64)
                .IsRequired(true);

            builder.OwnsOne(x => x.Password)
               .Property(x => x.Code)
               .HasColumnName("PasswordChangeCode")
               .HasColumnType("varchar")
               .HasMaxLength(8)
               .IsRequired(false);

            builder.OwnsOne(x => x.Password)
                .Property(x => x.ExpireDate)
                .HasColumnName("ExpireDate")
                .HasColumnType("datetime")
                .IsRequired(false);

            builder.OwnsOne(x => x.Password)
               .Property(x => x.ActivateDate)
               .HasColumnName("ActivateDate")
               .HasColumnType("datetime")
               .IsRequired(false);

            builder.OwnsOne(x => x.Password)
              .Ignore(x => x.Active);

            #endregion

            #region Email

            builder.OwnsOne(x => x.Email)
                .Ignore(x => x.IsValid);

            builder.OwnsOne(x => x.Email)
                .Ignore(x => x.Errors);

            builder.OwnsOne(x => x.Email)
                .OwnsOne(x => x.VerifyEmail)
                .Ignore(x => x.IsValid);

            builder.OwnsOne(x => x.Email)
                .OwnsOne(x => x.VerifyEmail)
                .Ignore(x => x.Errors);

            builder.OwnsOne(x => x.Email)
              .Property(x => x.Address)
              .HasColumnName("Address")
              .HasColumnType("varchar")
              .HasMaxLength(60)
              .IsRequired(true);

            builder.OwnsOne(x => x.Email)
                .OwnsOne(x => x.VerifyEmail)
                .Property(x => x.Code)
                .HasColumnName("ActivationCode")
                .HasColumnType("varchar")
                .HasMaxLength(6)
                .IsRequired(false);

            builder.OwnsOne(x => x.Email)
                .OwnsOne(x => x.VerifyEmail)
                .Property(x => x.ExpireDate)
                .HasColumnName("ExpireDateVerifyEmail")
                .HasColumnType("datetime")
                .IsRequired(false);

            builder.OwnsOne(x => x.Email)
                .OwnsOne(x => x.VerifyEmail)
                .Property(x => x.ActivateDate)
                .HasColumnName("ActivateDateAccount")
                .HasColumnType("datetime")
                .IsRequired(false);

            builder.OwnsOne(x => x.Email)
                .OwnsOne(x => x.VerifyEmail)
                .Ignore(x => x.Active);

            #endregion

            builder.Property(x => x.Role)
                .HasColumnName("Role")
                .HasColumnType("varchar(30)")
                .IsRequired(true);
        }
    }
}
