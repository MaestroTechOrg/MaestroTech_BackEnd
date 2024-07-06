using MaestroTech.Infrastructure.Data;
using MaestroTech.Infrastructure.Repositories;
using MaestroTech.Domain.Repositories;
using MaestroTech.Application.Services;
using MaestroTech.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure EF Core
builder.Services.AddDbContext<MaestroTechDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MaestroTech API V1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
