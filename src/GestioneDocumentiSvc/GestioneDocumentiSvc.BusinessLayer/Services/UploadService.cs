using GestioneDocumentiSvc.BusinessLayer.Mapper;
using GestioneDocumentiSvc.DataAccessLayer;
using GestioneDocumentiSvc.Shared.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Constants = GSWCloudApp.Common.Constants.BusinessLayer;

namespace GestioneDocumentiSvc.BusinessLayer.Services;

public class UploadService : IUploadService
{
    public async Task<Results<FileContentHttpResult, NotFound<string>>> DownloadFileAsync(string fileName, AppDbContext dbContext)
    {
        var findDocumento = await dbContext.Documenti.AsNoTracking()
            .Where(x => x.NomeDocumento == fileName.Trim())
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
        //return TypedResults.File(fileBytes, "application/octet-stream", fileName);
        return TypedResults.File(fileBytes, findDocumento.ContentType, fileName);
    }

    public async Task<Results<Ok, BadRequest<string>, Conflict<string>>> UploadFileAsync([FromForm] UploadDocumentoDto documentoDto, AppDbContext dbContext)
    {
        try
        {
            var documentPath = Path.Combine(Constants.DocumentUploadFolder, Constants.CurrentYear, Constants.CurrentMonth);
            var documento = documentoDto.Documento;

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

            var nuovoDocumento = new CreateDocumentoDto
            {
                FestaId = documentoDto.FestaId,
                Path = documentPath,
                ContentType = contentType,
                Extension = extension,
                Length = length,
                NomeDocumento = nameFile,
                Descrizione = documentoDto.Descrizione
            };

            //var newDocumento = ProfileMapper.CreateDocumentoDtoToEntity(nuovoDocumento);
            var newDocumento = nuovoDocumento.CreateDocumentoDtoToEntity();

            dbContext.Documenti.Add(newDocumento);
            await dbContext.SaveChangesAsync();

            return TypedResults.Ok();
        }
        catch (Exception ex)
        {
            return TypedResults.BadRequest(ex.Message);
        }
    }

    public async Task<Results<NoContent, NotFound<string>>> DeleteFileAsync(string fileName, AppDbContext dbContext)
    {
        var documento = await dbContext.Documenti.AsNoTracking()
            .Where(x => x.NomeDocumento == fileName.Trim())
            .FirstOrDefaultAsync();

        if (documento == null)
        {
            return TypedResults.NotFound("File not found.");
        }

        dbContext.Documenti.Remove(documento);
        await dbContext.SaveChangesAsync();

        return TypedResults.NoContent();
    }
}