using HtmlToPdf.Microservice.Contracts;
using HtmlToPdf.Microservice.Contracts.Commands;
using HtmlToPdf.Microservice.Contracts.Commands.UploadHtmlFile;
using HtmlToPdf.Microservice.Contracts.Dto;
using MassTransit;
using MassTransit.Mediator;

namespace HtmlToPdf.Microservice.Consumers.Commands;

public class UploadHtmlFileCommandConsumer : IConsumer<UploadHtmlFileBusCommand>
{
    private readonly IMediator _mediator;

    public UploadHtmlFileCommandConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task Consume(ConsumeContext<UploadHtmlFileBusCommand> context)
    {
        var res = await _mediator.SendRequest(new UploadHtmlFileCommand(context.Message.FileName, context.Message.FilePath));
        var dto = new WorkFileDto(res.Id, res.FileName, res.Status);
        await context.RespondAsync(new UploadHtmlFileBusCommand.Result(dto));
    }
}