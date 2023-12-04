using HtmlToPdf.Microservice.Contracts.Queries.GetFiles;
using HtmlToPdf.Microservice.Contracts.Queries.GetPdfFile;
using HtmlToPdf.Microservice.Dal;
using HtmlToPdf.Microservice.Domain.Model.Extensions;
using HtmlToPdf.Microservice.Domain.Model.WorkFiles;
using MassTransit;

namespace HtmlToPdf.Microservice.Domain.QueryHandlers;

public class GetFilesQueryHandler : IConsumer<GetFilesQuery>
{
    private readonly AppDbContext _dbContext;

    public GetFilesQueryHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Consume(ConsumeContext<GetFilesQuery> context)
    {
        var list = _dbContext.Set<WorkFile>().Select(x => x.ToDto()).ToList();
        await context.RespondAsync(new GetFilesQuery.Result(list));
    }
}