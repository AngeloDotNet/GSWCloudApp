using GSWCloudApp.Common.Identity.Options;
using GSWCloudApp.Common.Options;

namespace ConfigurazioniSvc.Shared;

public class ConfigurationApp
{
    /// <summary>
    /// Gets or sets the connection strings.
    /// </summary>
    public ConnectionStrings ConnectionStrings { get; set; } = null!;

    /// <summary>
    /// Gets or sets the application options.
    /// </summary>
    public ApplicationOptions ApplicationOptions { get; set; } = null!;

    /// <summary>
    /// Gets or sets the JWT options.
    /// </summary>
    public JwtOptions JwtOptions { get; set; } = null!;

    /// <summary>
    /// Gets or sets the worker settings.
    /// </summary>
    public WorkerSettings WorkerSettings { get; set; } = null!;

    /// <summary>
    /// Gets or sets the Polly policy options.
    /// </summary>
    public PollyPolicyOptions PollyPolicyOptions { get; set; } = null!;

    ///// <summary>
    ///// Gets or sets the default admin password.
    ///// </summary>
    public string DefaultAdminPassword { get; set; } = null!;
}