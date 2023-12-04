using HtmlToPdf.Microservice.Dal.EntityConfigs;
using Microsoft.EntityFrameworkCore;

namespace HtmlToPdf.Microservice.Dal;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new EntityConfig_WorkFile());

        base.OnModelCreating(builder);
    }
}