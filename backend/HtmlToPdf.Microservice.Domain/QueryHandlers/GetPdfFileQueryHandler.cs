using HtmlToPdf.Microservice.Contracts.Commands;
using HtmlToPdf.Microservice.Contracts.Commands.GeneratePdfFile;
using HtmlToPdf.Microservice.Contracts.Queries.GetPdfFile;
using HtmlToPdf.Microservice.Dal;
using HtmlToPdf.Microservice.Domain.Model.WorkFiles;
using MassTransit;
using MassTransit.Mediator;

namespace HtmlToPdf.Microservice.Domain.QueryHandlers;

public class GetPdfFileQueryHandler : IConsumer<GetPdfFileQuery>
{
    private readonly AppDbContext _dbContext;
    private readonly IMediator _mediator;

    public GetPdfFileQueryHandler(AppDbContext dbContext, IMediator mediator)
    {
        _dbContext = dbContext;
        _mediator = mediator;
    }
    public async Task Consume(ConsumeContext<GetPdfFileQuery> context)
    {
        var id = context.Message.FileId;
        var workFile = _dbContext.Set<WorkFile>().FirstOrDefault(x => x.Id == id);
        if (workFile == null)
        {
            throw new FileNotFoundException($"workFile with id={id} not found");
        }

        if (!string.IsNullOrEmpty(workFile.PathToPdf))
        {
            await context.RespondAsync(new GetPdfFileQuery.Result(workFile.PathToPdf, workFile.Name));
            return;
        }

        var result = await _mediator.SendRequest(new GeneratePdfFileCommand(id));
        await context.RespondAsync(new GetPdfFileQuery.Result(result.PathToPdf, workFile.Name));
    }
}