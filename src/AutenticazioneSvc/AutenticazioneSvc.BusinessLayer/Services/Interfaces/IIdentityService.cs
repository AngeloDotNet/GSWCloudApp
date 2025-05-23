﻿using AutenticazioneSvc.Shared.DTO;

namespace AutenticazioneSvc.BusinessLayer.Services.Interfaces;

public interface IIdentityService
{
    Task<RegisterResponse> RegisterAsync(RegisterRequest request);
    Task<AuthResponse> LoginAsync(LoginRequest request);
    Task<AuthResponse> RefreshTokenAsync(RefreshTokenRequest request);
}