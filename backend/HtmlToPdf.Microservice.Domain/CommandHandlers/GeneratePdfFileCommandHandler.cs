using HtmlToPdf.Common.Services.Pdf;
using HtmlToPdf.Microservice.Contracts.Commands.GeneratePdfFile;
using HtmlToPdf.Microservice.Dal;
using HtmlToPdf.Microservice.Domain.Model.WorkFiles;
using MassTransit;

namespace HtmlToPdf.Microservice.Domain.CommandHandlers;

public class GeneratePdfFileCommandHandler : IConsumer<GeneratePdfFileCommand>
{
    private readonly AppDbContext _dbContext;
    private readonly IPdfService _pdfService;

    public GeneratePdfFileCommandHandler(AppDbContext dbContext, IPdfService pdfService)
    {
        _dbContext = dbContext;
        _pdfService = pdfService;
    }
    public async Task Consume(ConsumeContext<GeneratePdfFileCommand> context)
    {
        var id = context.Message.FileId;
        var workFile = _dbContext.Set<WorkFile>().FirstOrDefault(x => x.Id == id);
        if (workFile == null)
        {
            throw new FileNotFoundException($"workFile with id={id} not found");
        }

        if (!string.IsNullOrEmpty(workFile.PathToPdf))
        {
            await context.RespondAsync(new GeneratePdfFileCommand.Result(workFile.PathToPdf));
            return;
        }

        var pathToPdf = await _pdfService.GeneratePdf(workFile.PathToHtml, workFile.Name);
        workFile.SetPathToPdf(pathToPdf);
        await _dbContext.SaveChangesAsync();
        await context.RespondAsync(new GeneratePdfFileCommand.Result(pathToPdf));
    }
}