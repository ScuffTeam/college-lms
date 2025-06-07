using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using college_lms.Data.DTOs.Auth;
using college_lms.Data.Entities;
using college_lms.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

// namespace college_lms.Services;
//
// public class UserService : IUserService
// {
//     private readonly UserManager<User> _userManager;
//     private readonly IRefreshTokenStore _refreshTokenStore;
//
//     public AuthService(
//         UserManager<User> userManager,
//         IRefreshTokenStore refreshTokenStore,
//         IOptions<AppOptions> options
//     )
//     {
//         _userManager = userManager;
//         _refreshTokenStore = refreshTokenStore;
//     }
// }
