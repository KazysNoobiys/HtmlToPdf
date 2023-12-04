using HtmlToPdf.Microservice.Contracts.Dto;

namespace HtmlToPdf.Microservice.Contracts.Queries.GetFiles;

public record GetFilesBusQuery()
{
    public record Result(List<WorkFileDto> List);
}