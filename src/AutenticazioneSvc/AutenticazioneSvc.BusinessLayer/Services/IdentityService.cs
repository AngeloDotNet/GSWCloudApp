using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutenticazioneSvc.BusinessLayer.Extensions;
using AutenticazioneSvc.BusinessLayer.Settings;
using AutenticazioneSvc.DataAccessLayer.Entities;
using AutenticazioneSvc.Shared.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AutenticazioneSvc.BusinessLayer.Services;

public class IdentityService(IOptions<JwtOptions> jwtOptions, UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager) : IIdentityService
{
    private readonly JwtOptions jwtSettings = jwtOptions.Value;

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        var signInResult = await signInManager.PasswordSignInAsync(request.UserName, request.Password, false, false);

        if (!signInResult.Succeeded)
        {
            return null!;
        }

        var user = await userManager.FindByNameAsync(request.UserName);

        if (user == null)
        {
            return null!;
        }

        await userManager.UpdateSecurityStampAsync(user);

        var userRoles = await userManager.GetRolesAsync(user);

        var claims = new List<Claim>()
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, request.UserName),
            new(ClaimTypes.Email, user.Email ?? string.Empty),
            new(ClaimTypes.SerialNumber, user.SecurityStamp!.ToString()),

            new("FullName", string.Join(" ", user.LastName, user.FirstName))

            //TODO: Manca il claim per la licenza

            //TODO: Manca la union della lista dei permessi

            //TODO: Manca la union della lista dei moduli a cui l'utente può accedere
        }
        .Union(userRoles.Select(role => new Claim(ClaimTypes.Role, role))).ToList();

        var loginResponse = CreateToken(claims);

        user.RefreshToken = loginResponse.RefreshToken;
        user.RefreshTokenExpirationDate = DateTime.UtcNow.AddMinutes(jwtSettings.RefreshTokenExpirationMinutes);

        await userManager.UpdateAsync(user);

        return loginResponse;
    }

    public async Task<AuthResponse> RefreshTokenAsync(RefreshTokenRequest request)
    {
        var user = ValidateAccessToken(request.AccessToken);

        if (user != null)
        {
            var userId = user.GetId();
            var dbUser = await userManager.FindByIdAsync(userId.ToString());

            if (dbUser?.RefreshToken == null || dbUser?.RefreshTokenExpirationDate < DateTime.UtcNow || dbUser?.RefreshToken != request.RefreshToken)
            {
                return null!;
            }

            var loginResponse = CreateToken(user.Claims.ToList());

            dbUser.RefreshToken = loginResponse.RefreshToken;
            dbUser.RefreshTokenExpirationDate = DateTime.UtcNow.AddMinutes(jwtSettings.RefreshTokenExpirationMinutes);

            await userManager.UpdateAsync(dbUser);

            return loginResponse;
        }

        return null!;
    }

    private AuthResponse CreateToken(IList<Claim> claims)
    {
        var audienceClaim = claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Aud);
        claims.Remove(audienceClaim!);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecurityKey));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(jwtSettings.Issuer, jwtSettings.Audience, claims,
            DateTime.UtcNow, DateTime.UtcNow.AddMinutes(jwtSettings.AccessTokenExpirationMinutes), signingCredentials);

        var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        var italyTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central Europe Standard Time");
        var expiredLocalNow = TimeZoneInfo.ConvertTimeFromUtc(jwtSecurityToken.ValidTo, italyTimeZone);

        var response = new AuthResponse
        {
            AccessToken = accessToken,
            RefreshToken = GenerateRefreshToken(),
            ExpiredToken = expiredLocalNow
        };

        return response;

        static string GenerateRefreshToken()
        {
            var randomNumber = new byte[256];
            using var generator = RandomNumberGenerator.Create();
            generator.GetBytes(randomNumber);

            return Convert.ToBase64String(randomNumber);
        }
    }

    private ClaimsPrincipal ValidateAccessToken(string accessToken)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidateAudience = true,
            ValidAudience = jwtSettings.Audience,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecurityKey)),
            RequireExpirationTime = true,
            ClockSkew = TimeSpan.Zero
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            var user = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out var securityToken);

            if (securityToken is JwtSecurityToken jwtSecurityToken && jwtSecurityToken.Header.Alg == SecurityAlgorithms.HmacSha256)
            {
                return user;
            }
        }
        catch
        { }

        return null!;
    }

    public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
    {
        var user = new ApplicationUser
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            UserName = request.Email,

            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(user, request.Password);

        if (result.Succeeded)
        {
            result = await userManager.AddToRoleAsync(user, RoleNames.User);
        }

        var response = new RegisterResponse
        {
            Succeeded = result.Succeeded,
            Errors = result.Errors.Select(e => e.Description)
        };

        return response;
    }
}