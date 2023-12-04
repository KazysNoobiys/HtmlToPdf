using HtmlToPdf.Microservice.Contracts.Dto;
using MassTransit.Mediator;

namespace HtmlToPdf.Microservice.Contracts.Commands.UploadHtmlFile;

public record UploadHtmlFileCommand(string FileName, string FilePath) : Request<UploadHtmlFileCommand.Result>
{
    public record Result(Guid Id, string FileName, FileStatus Status);
}
