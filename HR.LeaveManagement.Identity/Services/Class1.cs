using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Models.Identity;
using HR.LeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Identity.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly JwtSettings _jwtSettings;

    public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager
        , JwtSettings jwtSettings)
    {
        this._userManager = userManager;
        this._signInManager = signInManager;
        this._jwtSettings = jwtSettings;
    }
    public async Task<AuthResponse> Login(AuthRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if ((user == null))
        {
            throw new NotFoundException($"User with email: {request.Email} does not exist", request.Email);
        }
        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        if (!result.Succeeded)
        {
            throw new UnauthorizedAccessException($"Invalid credentials for user: {request.Email}");
        }
        // Generate JWT Token
        JwtSecurityToken token = await GenerateToken(user);
    }

    private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
    {
        var userClaims = await _userManager.GetClaimsAsync(user); //claims here specifies the user's roles
        var roles = await _userManager.GetRolesAsync(user);
        var roleClaims = roles.Select(tmp => new Claim(ClaimTypes.Role, tmp)).ToList();

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("uid", user.Id),
        }.Union(userClaims)
        .Union(roleClaims);
    }

    public Task<RegistrationResponse> Register(RegistrationRequest request)
    {
        throw new NotImplementedException();
    }
}
