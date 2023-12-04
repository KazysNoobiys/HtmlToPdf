using HtmlToPdf.Microservice.Contracts.Commands.UploadHtmlFile;
using HtmlToPdf.Microservice.Dal;
using HtmlToPdf.Microservice.Domain.Model.Extensions;
using HtmlToPdf.Microservice.Domain.Model.WorkFiles;
using MassTransit;

namespace HtmlToPdf.Microservice.Domain.CommandHandlers;

public class UploadHtmlFileCommandHandler : IConsumer<UploadHtmlFileCommand>
{
    private readonly AppDbContext _dbContext;

    public UploadHtmlFileCommandHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Consume(ConsumeContext<UploadHtmlFileCommand> context)
    {
        var workFile = WorkFile.Create(context.Message.FileName, context.Message.FilePath);
        _dbContext.Set<WorkFile>().Add(workFile);
        await _dbContext.SaveChangesAsync();

        await context.RespondAsync(
            new UploadHtmlFileCommand.Result(
                workFile.Id,
                workFile.Name,
                workFile.ToPublicStatus()));
    }
}