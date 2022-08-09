using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuditApp.Data.Entities.Configurations
{
    internal class AuditConfiguration : IEntityTypeConfiguration<Audit>
    {
        public void Configure(EntityTypeBuilder<Audit> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.EntityId).IsRequired();

            builder.Property(x => x.EntityName).IsRequired();

            builder.Property(x => x.ActionType).IsRequired();

            builder.Property(x => x.TimeStamp).IsRequired();

            builder.Property(x => x.OldValues).IsRequired(false);
            builder.Property(x => x.NewValues).IsRequired(false);

            builder.HasData(
                new Audit
                {
                    Id = 1,
                    EntityId = 1,
                    EntityName = "Customer",
                    ActionType = "INSERT",
                    TimeStamp = DateTime.UtcNow.AddDays(-1),

                    OldValues = null,
                    NewValues = "{\"id\":1, \"name\":\"asd\", \"cnp\":\"1234567812345\"}",
                },
                new Audit
                {
                    Id = 2,
                    EntityId = 1,
                    EntityName = "Customer",
                    ActionType = "DELETE",
                    TimeStamp = DateTime.UtcNow.AddDays(-1),

                    OldValues = "{\"id\":1, \"name\":\"asd\", \"cnp\":\"1234567812345\"}",
                    NewValues = null,
                });
        }
    }
}
