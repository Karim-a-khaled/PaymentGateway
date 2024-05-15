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

 



    public async Task<int> GetUserId(string Token)
    {
        throw new NotImplementedException();
    }
}
