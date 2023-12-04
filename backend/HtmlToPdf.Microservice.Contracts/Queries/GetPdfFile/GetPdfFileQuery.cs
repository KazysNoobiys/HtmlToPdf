using HtmlToPdf.Microservice.Contracts.Commands;
using MassTransit.Mediator;

namespace HtmlToPdf.Microservice.Contracts.Queries.GetPdfFile;

public record GetPdfFileQuery(Guid FileId) : Request<GetPdfFileQuery.Result>
{
    public record Result(string PathToPdf, string FileName);
}