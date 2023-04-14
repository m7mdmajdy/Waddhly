using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Waddhly.Data;
using Waddhly.Helpers;
using Waddhly.Models;
using Waddhly.Models.Authentication;
using Waddhly.Models.UserServices;

namespace Waddhly.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly JWT _jwt;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        public AuthService(UserManager<User> userManager, IOptions<JWT> jwt, ApplicationDbContext context, IConfiguration configuration)
        {
            _userManager = userManager;
            _jwt = jwt.Value;
            _context = context;
            _configuration = configuration;

        }

        public async Task<AuthModel> loginAsync(LoginModel model)
        {
            var authModel=new AuthModel();
            var user = await _userManager.FindByEmailAsync(model.Email);
            if(user == null || !await _userManager.CheckPasswordAsync(user,model.Password))
            {
                authModel.Message = "Email or Password is incorrect";
                return authModel;
            }

            var JwtSecurityToken = await CreateJwtToken(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            authModel.IsAuthenticated = true;
            authModel.ExpiresOn = JwtSecurityToken.ValidTo;
            authModel.Token= new JwtSecurityTokenHandler().WriteToken(JwtSecurityToken);
            authModel.Email = user.Email;
            authModel.Username = user.UserName;
            authModel.Roles = userRoles.ToList();
            return authModel;
        }

        public async Task<AuthModel> RegisterAsync(RegisterModel model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) is not null)
            {
                return new AuthModel { Message = "Email is already registered" };
            }
            if (await _userManager.FindByNameAsync(model.Username) is not null)
            {
                return new AuthModel { Message = "Username is already registered" };
            }

            /*Category retrievedCategory = _context.Categories.Find(model.categoryID);
            if (retrievedCategory == null)
            {
                return new AuthModel { Message = "No category selected" };
            }*/
            User user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Username,
                Country = model.Country,
                PhoneNumber= model.PhoneNumber,
                /*category = retrievedCategory,*/
            };
            var result = await _userManager.CreateAsync(user,model.Password);
            if(!result.Succeeded) 
            {
                var errors=string.Empty;
                foreach(var error in result.Errors)
                {
                    errors += $"{error.Description}, ";
                }
                return new AuthModel { Message = errors };
            }
            await _userManager.AddToRoleAsync(user, "User");
            var jwtSecurityToken = await CreateJwtToken(user);
            return new AuthModel
            {
                Email = user.Email,
                ExpiresOn = jwtSecurityToken.ValidTo,
                IsAuthenticated = true,
                Roles = new List<string> { "User" },
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Username = user.UserName,
            };
        }
        private async Task<JwtSecurityToken> CreateJwtToken(User user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);
            string JWTKey = _configuration["Jwt:Key"];
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTKey));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
    }
}
