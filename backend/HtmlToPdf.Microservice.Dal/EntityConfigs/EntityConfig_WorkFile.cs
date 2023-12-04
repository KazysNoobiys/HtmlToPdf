using HtmlToPdf.Microservice.Domain.Model.WorkFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HtmlToPdf.Microservice.Dal.EntityConfigs;

public class EntityConfig_WorkFile : IEntityTypeConfiguration<WorkFile>
{
    public void Configure(EntityTypeBuilder<WorkFile> builder)
    {
        builder.ToTable(nameof(WorkFile));
        builder.HasKey(c => c.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedNever();
    }
}