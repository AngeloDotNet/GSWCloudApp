using AutoMapper;
using InvioEmailSvc.BusinessLayer.Mediator.Command;
using InvioEmailSvc.DataAccessLayer.Entities;
using InvioEmailSvc.Shared.DTO;

namespace InvioEmailSvc.BusinessLayer.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateEmailMessageDto, EmailMessage>().ReverseMap();
        CreateMap<EmailMessage, CreateEmailMessageCommand>().ReverseMap();
    }
}