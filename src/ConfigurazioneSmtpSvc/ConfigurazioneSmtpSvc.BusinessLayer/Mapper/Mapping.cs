using ConfigurazioneSmtpSvc.BusinessLayer.Mediator.Command.Create;
using ConfigurazioneSmtpSvc.BusinessLayer.Mediator.Command.Edit;
using ConfigurazioneSmtpSvc.DataAccessLayer.Entities;
using ConfigurazioneSmtpSvc.Shared.DTO.SettingSender;
using ConfigurazioneSmtpSvc.Shared.DTO.SettingSmtp;

namespace ConfigurazioneSmtpSvc.BusinessLayer.Mapper;

public static class Mapping
{
    /// <summary>
    /// Maps a <see cref="CreateSettingSmtpCommand"/> to a <see cref="SettingSmtp"/>.
    /// </summary>
    /// <param name="command">The command containing SMTP settings.</param>
    /// <returns>A <see cref="SettingSmtp"/> object.</returns>
    public static SettingSmtp ToSettingSmtp(this CreateSettingSmtpCommand command)
    {
        return new SettingSmtp
        {
            Host = command.Host,
            Port = command.Port,
            Security = command.Security,
            Username = command.Username,
            Password = command.Password
        };
    }

    /// <summary>
    /// Maps a <see cref="CreateSettingSenderCommand"/> to a <see cref="SettingSender"/>.
    /// </summary>
    /// <param name="command">The command containing sender settings.</param>
    /// <returns>A <see cref="SettingSender"/> object.</returns>
    public static SettingSender ToSettingSender(this CreateSettingSenderCommand command)
    {
        return new SettingSender
        {
            Name = command.Name,
            Email = command.Email
        };
    }

    /// <summary>
    /// Maps an <see cref="EditSettingSmtpCommand"/> to a <see cref="SettingSmtp"/>.
    /// </summary>
    /// <param name="command">The command containing SMTP settings.</param>
    /// <returns>A <see cref="SettingSmtp"/> object.</returns>
    public static SettingSmtp ToSettingSmtp(this EditSettingSmtpCommand command)
    {
        return new SettingSmtp
        {
            Id = command.Id,
            Host = command.Host,
            Port = command.Port,
            Security = command.Security,
            Username = command.Username,
            Password = command.Password
        };
    }

    /// <summary>
    /// Maps an <see cref="EditSettingSenderCommand"/> to a <see cref="SettingSender"/>.
    /// </summary>
    /// <param name="command">The command containing sender settings.</param>
    /// <returns>A <see cref="SettingSender"/> object.</returns>
    public static SettingSender ToSettingSender(this EditSettingSenderCommand command)
    {
        return new SettingSender
        {
            Id = command.Id,
            Name = command.Name,
            Email = command.Email
        };
    }

    /// <summary>
    /// Maps a <see cref="SettingSmtp"/> to a <see cref="SettingSmtpDto"/>.
    /// </summary>
    /// <param name="settingSmtp">The SMTP setting entity.</param>
    /// <returns>A <see cref="SettingSmtpDto"/> object.</returns>
    public static SettingSmtpDto ToSettingSmtpDto(this SettingSmtp settingSmtp)
    {
        return new SettingSmtpDto(settingSmtp.Id, settingSmtp.Host, settingSmtp.Port, settingSmtp.Security, settingSmtp.Username, settingSmtp.Password);
    }

    /// <summary>
    /// Maps a <see cref="SettingSender"/> to a <see cref="SettingSenderDto"/>.
    /// </summary>
    /// <param name="settingSender">The sender setting entity.</param>
    /// <returns>A <see cref="SettingSenderDto"/> object.</returns>
    public static SettingSenderDto ToSettingSenderDto(this SettingSender settingSender)
    {
        return new SettingSenderDto(settingSender.Id, settingSender.Email, settingSender.Name);
    }
}