using HtmlToPdf.Common.Services.File;
using HtmlToPdf.Common.Services.Pdf;
using HtmlToPdf.Microservice.Contracts.Commands.UploadHtmlFile;
using HtmlToPdf.Microservice.Contracts.Queries.GetFiles;
using HtmlToPdf.Microservice.Contracts.Queries.GetPdfFile;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.FileIO;


namespace HtmlToPdf.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IRequestClient<UploadHtmlFileBusCommand> _uploadHtmlFileRequestClient;
        private readonly IRequestClient<GetPdfFileBusQuery> _getPdfFileBusQueryCommandRequestClient;
        private readonly IRequestClient<GetFilesBusQuery> _getFilesBusQueryCommandRequestClient;
        private readonly IFileService _fileService;
        private readonly IPdfService _pdfService;

        public FileController(
            IRequestClient<UploadHtmlFileBusCommand> uploadHtmlFileRequestClient, 
            IRequestClient<GetPdfFileBusQuery> getPdfFileBusQueryCommandRequestClient, 
            IRequestClient<GetFilesBusQuery> getFilesBusQueryCommandRequestClient, 
            IFileService fileService,
            IPdfService pdfService)
        {
            _uploadHtmlFileRequestClient = uploadHtmlFileRequestClient;
            _getPdfFileBusQueryCommandRequestClient = getPdfFileBusQueryCommandRequestClient;
            _getFilesBusQueryCommandRequestClient = getFilesBusQueryCommandRequestClient;
            _fileService = fileService;
            _pdfService = pdfService;
        }

        [HttpPost(nameof(UploadFile))]
        public async Task<IActionResult> UploadFile(IFormFile file, CancellationToken cancellationToken)
        {
            if (file.ContentType != "text/html")
            {
                return BadRequest("Must html");
            }

            await using var fileStream = file.OpenReadStream();
            var fileName = $"{Guid.NewGuid()}-{file.FileName}";
            var filePath = await _fileService.UploadFile(fileStream, fileName);
            var response = await _uploadHtmlFileRequestClient.GetResponse<UploadHtmlFileBusCommand.Result>(
                new UploadHtmlFileBusCommand(file.Name, filePath),
                cancellationToken);

            return Ok(response.Message.Dto);
        }

        [HttpGet(nameof(GetPdfFile))]
        public async Task<IActionResult> GetPdfFile([FromQuery] Guid id, CancellationToken cancellationToken)
        {
            var response = await _getPdfFileBusQueryCommandRequestClient.GetResponse<GetPdfFileBusQuery.Result>(
                new GetPdfFileBusQuery(id),
                cancellationToken);
            var fileDto = await _pdfService.GetPdfFileDto(response.Message.PathToPdf, response.Message.FileName);
            return File(fileDto.Data, fileDto.FileType, fileDto.FileName);
        }

        [HttpGet(nameof(GetFiles))]
        public async Task<IActionResult> GetFiles(CancellationToken cancellationToken)
        {
            var response = await _getFilesBusQueryCommandRequestClient.GetResponse<GetFilesBusQuery.Result>(
                new GetFilesBusQuery(),
                cancellationToken);
            return Ok(response.Message.List);
        }
    }
}
