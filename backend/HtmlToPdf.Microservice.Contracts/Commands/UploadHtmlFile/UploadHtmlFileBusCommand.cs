using HtmlToPdf.Microservice.Contracts.Dto;
using Microsoft.AspNetCore.Http;

namespace HtmlToPdf.Microservice.Contracts.Commands.UploadHtmlFile;

public record UploadHtmlFileBusCommand(string FileName, string FilePath)
{
    public record Result(WorkFileDto Dto);
}
