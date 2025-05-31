using GestioneDocumentiSvc.DataAccessLayer.Entities;
using GestioneDocumentiSvc.Shared.DTO;

namespace GestioneDocumentiSvc.BusinessLayer.Mapper;

public static class ProfileMapper
{
    public static Documento CreateDocumentoDtoToEntity(this CreateDocumentoDto dto)
    {
        return new Documento
        {
            NomeDocumento = dto.NomeDocumento,
            Descrizione = dto.Descrizione,
            Path = dto.Path,
            ContentType = dto.ContentType,
            Extension = dto.Extension,
            Length = dto.Length
        };
    }
}