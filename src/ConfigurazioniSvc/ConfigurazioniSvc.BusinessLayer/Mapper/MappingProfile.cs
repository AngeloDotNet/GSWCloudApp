using AutoMapper;
using ConfigurazioniSvc.DataAccessLayer.Entities;
using ConfigurazioniSvc.Shared.DTO;

namespace ConfigurazioniSvc.BusinessLayer.Mapper;

/// <summary>
/// Defines the mapping profile for AutoMapper.
/// </summary>
public class MappingProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MappingProfile"/> class.
    /// </summary>
    public MappingProfile()
    {

        CreateMap<Configurazione, ConfigurazioneDto>().ReverseMap();
        CreateMap<CreateConfigurazioneDto, Configurazione>().ReverseMap();

        CreateMap<Configurazione, EditConfigurazioneDto>();
        CreateMap<EditConfigurazioneDto, Configurazione>()
            .ForMember(dst => dst.FestaId, opt => opt.PreCondition(src => src.FestaId != null))
            .ForMember(dst => dst.Chiave, opt => opt.PreCondition(src => src.Chiave != null))
            .ForMember(dst => dst.Valore, opt => opt.PreCondition(src => src.Valore != null))
            .ForMember(dst => dst.Tipo, opt => opt.PreCondition(src => src.Tipo != null))
            .ForMember(dst => dst.Posizione, opt => opt.PreCondition(src => src.Posizione != null))
            .ForMember(dst => dst.Obbligatorio, opt => opt.PreCondition(src => src.Obbligatorio != null))
            .ForMember(dst => dst.Scope, opt => opt.PreCondition(src => src.Scope != null));
    }
}
