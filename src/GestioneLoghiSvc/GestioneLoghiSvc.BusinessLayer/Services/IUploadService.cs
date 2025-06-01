using GestioneLoghiSvc.DataAccessLayer;
using GestioneLoghiSvc.Shared.DTO;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GestioneLoghiSvc.BusinessLayer.Services;

public interface IUploadService
{
    Task<Results<Ok, BadRequest<string>, Conflict<string>>> UploadFileAsync([FromForm] UploadImmagineDto immagineDto, AppDbContext dbContext);
    Task<Results<FileContentHttpResult, NotFound<string>, BadRequest<string>>> DownloadFileAsync(string fileName, AppDbContext dbContext);
    Task<Results<NoContent, NotFound<string>, BadRequest<string>>> DeleteFileAsync(string fileName, AppDbContext dbContext);
}