namespace HtmlToPdf.Microservice.Domain.Model.WorkFiles;

public class WorkFile
{
    public enum FileStatus
    {
        None = 0,
        Uploaded,
        Processed,
    }

    public static WorkFile Create(string name, string pathToHtml)
    {
        var id = Guid.NewGuid();
        var res = new WorkFile(
            id,
            name,
            pathToHtml,
            FileStatus.Uploaded);
        return res;
    }


    private WorkFile(
        Guid id,
        string name,
        string pathToHtml,
        FileStatus status,
        string? pathToPdf = null)
    {
        this.Id = id;
        this.Name = name;
        this.PathToHtml = pathToHtml;
        this.PathToPdf = pathToPdf;
        this.Status = status;
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string PathToHtml { get; private set; }
    public string? PathToPdf { get; private set; }
    public FileStatus Status { get; private set; }

    public void SetPathToPdf(string pathToPdf)
    {
        this.PathToPdf = pathToPdf;
        this.Status = FileStatus.Processed;
    }
}