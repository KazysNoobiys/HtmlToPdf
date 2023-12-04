using HtmlToPdf.Common.Services.File;
using HtmlToPdf.Common.Services.Pdf;
using HtmlToPdf.Microservice.Dal;
using HtmlToPdf.Microservice.Domain.CommandHandlers;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMassTransit(configurator =>
{
    configurator.AddConsumers(typeof(Program).Assembly);
    configurator.UsingRabbitMq((context, factoryConfigurator) =>
    {
        factoryConfigurator.Host("localhost", "/", h => {
            h.Username("guest");
            h.Password("guest");
        });

        factoryConfigurator.ConfigureEndpoints(context);
    });
});
builder.Services.AddMediator(cfg =>
{
    cfg.AddConsumers(typeof(UploadHtmlFileCommandHandler).Assembly);

});
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppDbContext"));
});

builder.Services.AddTransient<IFileService, FileService>();
builder.Services.AddTransient<IPdfService, PdfService>();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.Migrate();

    var pdfService = scope.ServiceProvider.GetRequiredService<IPdfService>();
    await pdfService.Init();
}


app.Run();

