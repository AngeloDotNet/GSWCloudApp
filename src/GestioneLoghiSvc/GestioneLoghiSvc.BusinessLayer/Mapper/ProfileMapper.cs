using GestioneLoghiSvc.DataAccessLayer.Entities;
using GestioneLoghiSvc.Shared.DTO;

namespace GestioneLoghiSvc.BusinessLayer.Mapper;

public static class ProfileMapper
{
    public static Immagine CreateImmagineDtoToEntity(this CreateImmagineDto dto)
    {
        return new Immagine
        {
            FestaId = dto.FestaId,
            Path = dto.Path,
            ContentType = dto.ContentType,
            Extension = dto.Extension,
            Length = dto.Length,
            NomeImmagine = dto.NomeImmagine,
            Descrizione = dto.Descrizione
        };
    }
}