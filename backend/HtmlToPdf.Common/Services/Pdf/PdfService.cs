using PuppeteerSharp;

namespace HtmlToPdf.Common.Services.Pdf;

public class PdfService : IPdfService
{
    private string pdfFolderName = "pdfFiles";

    public async Task Init()
    {
        var browserFetcher = new BrowserFetcher();
        await browserFetcher.DownloadAsync();
    }
    public async Task<string> GeneratePdf(string htmlFilePath, string fileName)
    {
       
        await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true });
        await using var page = await browser.NewPageAsync();

        var path = $"{Directory.GetCurrentDirectory()}\\{pdfFolderName}";
        var exists = Directory.Exists(path);
        if (!exists)
        {
            Directory.CreateDirectory(path);
        }
        var pdfFilePath = $"{path}\\{Guid.NewGuid()}-{fileName}.pdf";
        await page.GoToAsync(htmlFilePath);
        await page.PdfAsync(pdfFilePath);

        return pdfFilePath;
    }

    public Task<FileDto> GetPdfFileDto(string filePath, string fileName)
    {
        if (!System.IO.File.Exists(filePath))
        {
            throw new FileNotFoundException("Нет файла!");
        }

        byte[] data = System.IO.File.ReadAllBytes(filePath);
        var fileType = "application/pdf";
        var pdfFileName = $"{fileName}.pdf";

        return Task.FromResult(new FileDto(data, fileType, pdfFileName));
    }

   
}