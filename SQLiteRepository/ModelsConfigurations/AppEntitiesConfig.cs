using Core.Database.AppEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SQLiteRepository.ModelsConfigurations
{
    
    public class UserAccountConfig : IEntityTypeConfiguration<UserAccountEntity>
    {
        public void Configure(EntityTypeBuilder<UserAccountEntity> builder)
        {
            builder
                .ToTable("accounts")
                .HasData(DefaultEntitiesProvider.GetDefaultUserAccounts());
        }
    }
}
