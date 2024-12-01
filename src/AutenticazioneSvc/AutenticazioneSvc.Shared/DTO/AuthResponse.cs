namespace AutenticazioneSvc.Shared.DTO;

public class AuthResponse
{
    public string AccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
    public DateTime ExpiredToken { get; set; }
}