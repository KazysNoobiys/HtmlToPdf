using HtmlToPdf.Common.Services.Pdf;

namespace HtmlToPdf.Common.Services.File;

public class FileService : IFileService
{
    private string htmlFolderName = "htmlFiles";

    public async Task<string> UploadFile(Stream stream, string fileName)
    {
        var path = $"{Directory.GetCurrentDirectory()}\\{htmlFolderName}";
        var exists = Directory.Exists(path);
        if (!exists)
        {
            Directory.CreateDirectory(path);
        }
        var filePath = $"{path}\\{fileName}";
        var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
        await stream.CopyToAsync(fileStream);
        await fileStream.DisposeAsync();
        
        return filePath;
    }
}