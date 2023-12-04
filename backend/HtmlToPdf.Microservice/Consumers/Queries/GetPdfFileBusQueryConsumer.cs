using HtmlToPdf.Microservice.Contracts.Queries;
using HtmlToPdf.Microservice.Contracts.Queries.GetPdfFile;
using MassTransit;
using MassTransit.Mediator;

namespace HtmlToPdf.Microservice.Consumers.Queries;

public class GetPdfFileBusQueryConsumer : IConsumer<GetPdfFileBusQuery>
{
    private readonly IMediator _mediator;

    public GetPdfFileBusQueryConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task Consume(ConsumeContext<GetPdfFileBusQuery> context)
    {
        var res = await _mediator.SendRequest(new GetPdfFileQuery(context.Message.FileId));
        await context.RespondAsync(new GetPdfFileBusQuery.Result(res.PathToPdf, res.FileName));
    }
}