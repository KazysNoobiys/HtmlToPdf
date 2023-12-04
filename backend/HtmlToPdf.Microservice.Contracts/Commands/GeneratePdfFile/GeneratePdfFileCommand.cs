using MassTransit.Mediator;

namespace HtmlToPdf.Microservice.Contracts.Commands.GeneratePdfFile;

public record GeneratePdfFileCommand(Guid FileId) : Request<GeneratePdfFileCommand.Result>
{
    public record Result(string PathToPdf);
}