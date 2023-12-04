namespace HtmlToPdf.Common.Services.Pdf;

public interface IPdfService
{
    Task<string> GeneratePdf(string htmlFilePath, string fileName);
    Task<FileDto> GetPdfFileDto(string filePath, string fileName);
    Task Init();
}