using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MaestroTech.Infrastructure.Data;
using MaestroTech.Infrastructure.Repositories;
using MaestroTech.Domain.Repositories;
using MaestroTech.Application.Services;
using MaestroTech.Infrastructure.Services;
using MaestroTech.Domain.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure EF Core
builder.Services.AddDbContext<MaestroTechDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), 
        b => b.MigrationsAssembly("MaestroTech.API")));

// Configure Identity
builder.Services.AddIdentity<Usuario, IdentityRole<int>>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
})
.AddEntityFrameworkStores<MaestroTechDbContext>()
.AddDefaultTokenProviders();

// Configure DI for services and repositories
builder.Services.AddScoped<IIgrejaRepository, IgrejaRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IMusicaRepository, MusicaRepository>();
builder.Services.AddScoped<ICultoRepository, CultoRepository>();

// Obter configurações do Twilio e verificar nulidade
string accountSid = builder.Configuration["Twilio:AccountSid"] 
                    ?? throw new ArgumentNullException("Twilio:AccountSid");
string authToken = builder.Configuration["Twilio:AuthToken"] 
                   ?? throw new ArgumentNullException("Twilio:AuthToken");
string fromNumber = builder.Configuration["Twilio:FromNumber"] 
                    ?? throw new ArgumentNullException("Twilio:FromNumber");

// Registrar o serviço de envio para WhatsApp
builder.Services.AddScoped<IWhatsAppService, TwilioWhatsAppService>(provider => 
    new TwilioWhatsAppService(
        accountSid,
        authToken,
        fromNumber
    ));

// Obter a chave JWT e verificar nulidade
string jwtKey = builder.Configuration["Jwt:Key"] 
                ?? throw new ArgumentNullException("Jwt:Key");
var key = Encoding.ASCII.GetBytes(jwtKey);

// Configurar autenticação JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

// Adicionar políticas de autorização
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Administrador"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MaestroTech API V1");
    });
}

app.UseHttpsRedirection();

// Adicionar middlewares de autenticação e autorização
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
