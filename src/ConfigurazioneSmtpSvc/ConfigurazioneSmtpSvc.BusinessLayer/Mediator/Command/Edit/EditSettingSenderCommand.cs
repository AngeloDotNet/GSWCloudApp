using ConfigurazioneSmtpSvc.Shared.DTO.SettingSender;
using GSWCloudApp.Common.Mediator.Interfaces.Command;

namespace ConfigurazioneSmtpSvc.BusinessLayer.Mediator.Command.Edit;

public class EditSettingSenderCommand(EditSettingSenderDto dto) : ICommand<SettingSenderDto>
{
    public Guid Id { get; set; } = dto.Id;
    public string Name { get; set; } = dto.Name;
    public string Email { get; set; } = dto.Email;
}