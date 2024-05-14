using Microsoft.IdentityModel.Tokens;
using PaymentGateway.Data;
using PaymentGateway.Entities.DTOs;
using PaymentGateway.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace PaymentGateway.Services.Utilities;

public class AccountUtilities
{
    private readonly AppDbContext _context;
    private readonly JwtOptions _jwtOptions;
    
    public AccountUtilities(AppDbContext context, JwtOptions jwtOptions)
    {
        _context = context;
        _jwtOptions = jwtOptions;
    }

    public async Task<User> CheckUser(LoginDto loginDto)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Username == loginDto.Username && u.Password == loginDto.Password);
    }

    public async Task<string> GenerateToken(User? user)
    {
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = _jwtOptions.Issuer,
            Audience = _jwtOptions.Audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key)), SecurityAlgorithms.HmacSha256),
            Subject = new ClaimsIdentity(new Claim[]
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Name, user.Username),
            })
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        var token = tokenHandler.WriteToken(securityToken);
        return token;
    }

    public async Task<string> LoginWithHttpClient(LoginDto loginDto)
    {
        using (var httpClient = new HttpClient())
        {
            var jsonPayload = JsonConvert.SerializeObject(loginDto);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("/api/account/login", content);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Login failed: {response.StatusCode}");

            var responseContent = await response.Content.ReadAsStringAsync();
            var loginResponse = JsonConvert.DeserializeObject<User>(responseContent);

            return loginResponse.Token;
        }
    }

    public async Task<int> GetUserId(string Token)
    {
        throw new NotImplementedException();
    }
}
