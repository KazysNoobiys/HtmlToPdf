using HtmlToPdf.Microservice.Contracts.Dto;
using HtmlToPdf.Microservice.Domain.Model.WorkFiles;
using static HtmlToPdf.Microservice.Domain.Model.WorkFiles.WorkFile.FileStatus;
using PublicFileStatus = HtmlToPdf.Microservice.Contracts.Dto.FileStatus;
using InternalFileStatus = HtmlToPdf.Microservice.Domain.Model.WorkFiles.WorkFile.FileStatus;

namespace HtmlToPdf.Microservice.Domain.Model.Extensions;

public static class WorkFileExtensions
{
    public static PublicFileStatus ToPublicStatus(this WorkFile model)
    {
        return model.Status switch
        {
            None => PublicFileStatus.None,
            Uploaded => PublicFileStatus.Uploaded,
            Processed => PublicFileStatus.Processed,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public static WorkFileDto ToDto(this WorkFile model)
    {
        var res = new WorkFileDto(
            model.Id,
            model.Name,
            model.ToPublicStatus());

        return res;
    }
    public static WorkFileDetailsDto ToDetailsDto(this WorkFile model)
    {
        var res = new WorkFileDetailsDto(
            model.Id,
            model.Name,
            model.PathToHtml,
            model.PathToPdf,
            model.ToPublicStatus());

        return res;
    }
}