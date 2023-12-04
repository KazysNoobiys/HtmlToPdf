namespace HtmlToPdf.Microservice.Contracts.Dto;

public class WorkFileDto
{
    public WorkFileDto(Guid id, string name,  FileStatus status)
    {
        this.Id = id;
        this.Name = name;
        this.Status = status;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public FileStatus Status { get; set; }
}