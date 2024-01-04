using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Contracts.Auth;
using ShipperStation.Application.Interfaces.Services;
using ShipperStation.Domain.Entities.Identities;
using ShipperStation.Infrastructure.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ShipperStation.Infrastructure.Services;

public class JwtService : IJwtService
{

    private readonly JwtSettings _jwtSettings;
    private readonly SignInManager<User> _signInManager;
    private readonly IDataProtectionProvider _dataProtectionProvider;
    private readonly IDataProtector protector;
    private readonly TicketDataFormat ticketDataFormat;

    public JwtService(
        SignInManager<User> signInManager,
        IDataProtectionProvider dataProtectionProvider,
        IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
        _signInManager = signInManager;
        _dataProtectionProvider = dataProtectionProvider;

        protector = _dataProtectionProvider.CreateProtector(_jwtSettings.SerectRefreshKey);
        ticketDataFormat = new TicketDataFormat(protector);
    }

    public async Task<AccessTokenResponse> GenerateTokenAsync(User user)
    {
        var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(user);
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SerectKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        var token = new JwtSecurityToken(
            claims: claimsPrincipal.Claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.TokenExpire),
            signingCredentials: creds);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        var response = new AccessTokenResponse
        {
            AccessToken = jwt,
            ExpiresIn = (long)TimeSpan.FromMinutes(_jwtSettings.TokenExpire).TotalSeconds,
            RefreshToken = ticketDataFormat.Protect(CreateRefreshTicket(claimsPrincipal, DateTimeOffset.UtcNow)),
        };
        return response;
    }

    public async Task<User> ValidateRefreshTokenAsync(string refreshToken)
    {
        var ticket = ticketDataFormat.Unprotect(refreshToken);

        if (ticket?.Properties?.ExpiresUtc is not { } expiresUtc ||
            DateTimeOffset.UtcNow >= expiresUtc ||
            await _signInManager.ValidateSecurityStampAsync(ticket.Principal) is not User user)
        {
            throw new UnauthorizedAccessException(Resource.InvalidRefreshToken);
        }
        return user;
    }

    private AuthenticationTicket CreateRefreshTicket(ClaimsPrincipal user, DateTimeOffset utcNow)
    {
        var refreshProperties = new AuthenticationProperties
        {
            ExpiresUtc = utcNow.AddMinutes(_jwtSettings.RefreshTokenExpire)
        };
        return new AuthenticationTicket(user, refreshProperties, JwtBearerDefaults.AuthenticationScheme);
    }
}