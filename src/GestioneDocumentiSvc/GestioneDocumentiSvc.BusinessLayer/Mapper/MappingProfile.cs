using AutoMapper;
using GestioneDocumentiSvc.DataAccessLayer.Entities;
using GestioneDocumentiSvc.Shared.DTO;

namespace GestioneDocumentiSvc.BusinessLayer.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Documento, DocumentoDto>().ReverseMap();
        CreateMap<CreateDocumentoDto, Documento>().ReverseMap();
    }
}
