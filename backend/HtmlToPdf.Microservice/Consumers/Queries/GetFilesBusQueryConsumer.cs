using HtmlToPdf.Microservice.Contracts.Queries.GetFiles;
using MassTransit;
using MassTransit.Mediator;

namespace HtmlToPdf.Microservice.Consumers.Queries;

public class GetFilesBusQueryConsumer : IConsumer<GetFilesBusQuery>
{
    private readonly IMediator _mediator;

    public GetFilesBusQueryConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task Consume(ConsumeContext<GetFilesBusQuery> context)
    {
        var result = await _mediator.SendRequest(new GetFilesQuery());
        await context.RespondAsync(new GetFilesBusQuery.Result(result.List));
    }
}