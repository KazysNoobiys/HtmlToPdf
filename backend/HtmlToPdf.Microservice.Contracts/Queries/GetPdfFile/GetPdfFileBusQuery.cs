namespace HtmlToPdf.Microservice.Contracts.Queries.GetPdfFile;

public record GetPdfFileBusQuery(Guid FileId)
{
    public record Result(string PathToPdf, string FileName);
}