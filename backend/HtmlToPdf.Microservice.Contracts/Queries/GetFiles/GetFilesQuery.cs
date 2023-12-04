using HtmlToPdf.Microservice.Contracts.Dto;
using MassTransit;
using MassTransit.Mediator;

namespace HtmlToPdf.Microservice.Contracts.Queries.GetFiles;

public record GetFilesQuery() : Request<GetFilesQuery.Result>
{
    public record Result(List<WorkFileDto> List);
}