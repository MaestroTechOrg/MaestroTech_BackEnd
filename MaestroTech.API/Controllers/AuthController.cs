using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using MaestroTech.Domain.Entities;
using MaestroTech.Infrastructure.Data;
using System.Security.Claims;

namespace MaestroTech.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly MaestroTechDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(MaestroTechDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var admin = new Admin
            {
                Nome = model.Nome,
                Email = model.Email,
                Senha = BCrypt.Net.BCrypt.HashPassword(model.Senha) // Usar hashing para senhas
            };

            _context.Admins.Add(admin);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Admin created successfully" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var admin = await _context.Admins.SingleOrDefaultAsync(a => a.Email == model.Email);
            if (admin == null || !BCrypt.Net.BCrypt.Verify(model.Senha, admin.Senha))
            {
                return Unauthorized(new { Message = "Invalid login attempt." });
            }

            var token = GenerateJwtToken(admin);
            return Ok(new { Token = token });
        }

        private string GenerateJwtToken(Admin admin)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, admin.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, admin.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? throw new ArgumentNullException("Jwt:Key")));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    public class RegisterModel
    {
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
    }

    public class LoginModel
    {
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
    }
}
