using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MaestroTech.Infrastructure.Data;
using MaestroTech.Infrastructure.Repositories;
using MaestroTech.Domain.Repositories;
using MaestroTech.Application.Services;
using MaestroTech.Infrastructure.Services;
using MaestroTech.Domain.Entities;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "MaestroTech API", 
        Version = "v1",
        Description = "API para gerenciamento e sorteio de músicas para igrejas.",
        Contact = new OpenApiContact
        {
            Name = "MaestroTech",
            Email = "contato@maestrotech.org"
        }
    });
});

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MaestroTech API V1");
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllers();

app.Run();
