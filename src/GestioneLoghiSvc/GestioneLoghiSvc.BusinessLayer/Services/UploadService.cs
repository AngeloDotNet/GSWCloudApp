using GestioneLoghiSvc.BusinessLayer.Mapper;
using GestioneLoghiSvc.DataAccessLayer;
using GestioneLoghiSvc.Shared.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Constants = GSWCloudApp.Common.Constants.BusinessLayer;

namespace GestioneLoghiSvc.BusinessLayer.Services;

public class UploadService : IUploadService
{
    public async Task<Results<Ok, BadRequest<string>, Conflict<string>>> UploadFileAsync([FromForm] UploadImmagineDto documentoDto, AppDbContext dbContext)
    {
        try
        {
            var documentPath = Path.Combine(Constants.DocumentUploadFolder, Constants.CurrentYear, Constants.CurrentMonth);
            var documento = documentoDto.Immagine;

            var extension = Path.GetExtension(documento.FileName);
            var contentType = documento.ContentType;
            var length = documento.Length;
            var nameFile = documento.FileName;

            if (!Constants.AllowedDocumentExtensions.Contains(extension.ToLower()))
            {
                return TypedResults.BadRequest("This file type is not allowed.");
            }

            if (length == 0)
            {
                return TypedResults.BadRequest("No file uploaded.");
            }

            if (!Directory.Exists(documentPath))
            {
                Directory.CreateDirectory(documentPath);
            }

            var filePath = Path.Combine(documentPath, nameFile);

            if (File.Exists(filePath))
            {
                return TypedResults.Conflict("File already exists.");
            }

            using var stream = new FileStream(filePath, FileMode.Create);
            await documento.CopyToAsync(stream);

            var nuovoDocumento = new CreateImmagineDto
            {
                FestaId = documentoDto.FestaId,
                Path = documentPath,
                ContentType = contentType,
                Extension = extension,
                Length = length,
                NomeImmagine = nameFile,
                Descrizione = documentoDto.Descrizione
            };

            var newImmagine = ProfileMapper.CreateImmagineDtoToEntity(nuovoDocumento);

            dbContext.Immagini.Add(newImmagine);
            await dbContext.SaveChangesAsync();

            return TypedResults.Ok();
        }
        catch (Exception ex)
        {
            return TypedResults.BadRequest(ex.Message);
        }
    }

    public async Task<Results<FileContentHttpResult, NotFound<string>, BadRequest<string>>> DownloadFileAsync(string fileName, AppDbContext dbContext)
    {
        if (string.IsNullOrWhiteSpace(fileName))
        {
            return TypedResults.BadRequest("'FileName' must have a value.");
        }

        var findDocumento = await dbContext.Immagini.AsNoTracking()
            .Where(x => x.NomeImmagine == fileName.Trim())
            .FirstOrDefaultAsync();

        if (findDocumento == null)
        {
            return TypedResults.NotFound("File not found.");
        }

        var pathUpload = findDocumento.Path;
        var filePath = Path.Combine(pathUpload, fileName);

        if (!File.Exists(filePath))
        {
            return TypedResults.NotFound("File not found.");
        }

        var fileBytes = await File.ReadAllBytesAsync(filePath);
        return TypedResults.File(fileBytes, "application/octet-stream", fileName);
    }

    public async Task<Results<NoContent, NotFound<string>, BadRequest<string>>> DeleteFileAsync(string fileName, AppDbContext dbContext)
    {
        if (string.IsNullOrWhiteSpace(fileName))
        {
            return TypedResults.BadRequest("'FileName' must have a value.");
        }

        var documento = await dbContext.Immagini.AsNoTracking()
            .Where(x => x.NomeImmagine == fileName.Trim())
            .FirstOrDefaultAsync();

        if (documento == null)
        {
            return TypedResults.NotFound("File not found.");
        }

        dbContext.Immagini.Remove(documento);
        await dbContext.SaveChangesAsync();

        return TypedResults.NoContent();
    }
}