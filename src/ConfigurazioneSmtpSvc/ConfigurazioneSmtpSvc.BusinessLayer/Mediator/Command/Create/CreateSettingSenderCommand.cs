using ConfigurazioneSmtpSvc.Shared.DTO.SettingSender;
using GSWCloudApp.Common.Mediator.Interfaces.Command;

namespace ConfigurazioneSmtpSvc.BusinessLayer.Mediator.Command.Create;

public class CreateSettingSenderCommand(CreateSettingSenderDto dto) : ICommand<SettingSenderDto>
{
    public string Name { get; set; } = dto.Name;
    public string Email { get; set; } = dto.Email;
}