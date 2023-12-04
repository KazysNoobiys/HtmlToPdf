using HtmlToPdf.Common.Services.Pdf;

namespace HtmlToPdf.Common.Services.File;

public interface IFileService
{
    Task<string> UploadFile(Stream stream, string fileName);
}