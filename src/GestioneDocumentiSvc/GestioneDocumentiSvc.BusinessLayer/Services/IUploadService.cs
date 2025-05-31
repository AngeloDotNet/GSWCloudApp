using GestioneDocumentiSvc.DataAccessLayer;
using GestioneDocumentiSvc.Shared.DTO;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GestioneDocumentiSvc.BusinessLayer.Services;

public interface IUploadService
{
    Task<Results<Ok, BadRequest<string>, Conflict<string>>> UploadFileAsync([FromForm] UploadDocumentoDto documentoDto, AppDbContext dbContext);
    Task<Results<FileContentHttpResult, NotFound<string>>> DownloadFileAsync(string fileName, AppDbContext dbContext);
    Task<Results<NoContent, NotFound<string>>> DeleteFileAsync(string fileName, AppDbContext dbContext);
}