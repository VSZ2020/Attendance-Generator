using Core.Database.AppEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SQLiteRepository.ModelsConfigurations
{
    public class UserRolesConfig : IEntityTypeConfiguration<UserRoleEntity>
    {
        public void Configure(EntityTypeBuilder<UserRoleEntity> builder)
        {
            builder
                .HasMany(r => r.UserAccounts)
                .WithOne(u => u.Role)
                .HasForeignKey(u => u.UserRoleId);
            builder.HasMany(r => r.Permissions)
                .WithMany(p => p.Roles)
                .UsingEntity(r => r.ToTable("role-permision"));

            builder
                .ToTable("roles")
                .HasData(UserRoleEntity.GetDefault());
        }
    }

    public class UserAccountConfig : IEntityTypeConfiguration<UserAccountEntity>
    {
        public void Configure(EntityTypeBuilder<UserAccountEntity> builder)
        {
            builder
                .ToTable("accounts")
                .HasData(UserAccountEntity.GetDefault());
        }
    }

    public class PermissionsConfig : IEntityTypeConfiguration<PermissionEntity>
    {
        public void Configure(EntityTypeBuilder<PermissionEntity> builder)
        {
            builder
                .ToTable("permissions")
                .HasData(PermissionEntity.GetDefault());
        }
    }
}
