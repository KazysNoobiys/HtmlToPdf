using HtmlToPdf.Common.Services.File;
using HtmlToPdf.Common.Services.Pdf;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddMassTransit(configurator =>
{
    configurator.UsingRabbitMq((context, factoryConfigurator) =>
    {
        factoryConfigurator.Host("localhost", "/", h => {
            h.Username("guest");
            h.Password("guest");
        });

        factoryConfigurator.ConfigureEndpoints(context);

    });
});
builder.Services.AddTransient<IFileService, FileService>();
builder.Services.AddTransient<IPdfService, PdfService>();
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();
app.UseCors(policyBuilder => policyBuilder.AllowAnyOrigin());
app.MapControllers();

app.Run();
