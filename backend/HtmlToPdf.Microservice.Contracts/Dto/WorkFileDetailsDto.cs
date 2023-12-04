namespace HtmlToPdf.Microservice.Contracts.Dto;

public class WorkFileDetailsDto : WorkFileDto
{
    public WorkFileDetailsDto(Guid id, string name, string pathToHtml, string? pathToPdf, FileStatus status) : base(id, name, status)
    {
        this.PathToHtml = pathToHtml;
        this.PathToPdf = pathToPdf;
    }

    public string PathToHtml { get; set; }
    public string? PathToPdf { get; set; }
}