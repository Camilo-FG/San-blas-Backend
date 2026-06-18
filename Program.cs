using Microsoft.EntityFrameworkCore;
using SanblasBackend.Data;
using SanblasBackend.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<GlobalContex>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"), npgsqlOptions =>
    {
        npgsqlOptions.EnableRetryOnFailure(
            maxRetryCount: 1,
            maxRetryDelay: TimeSpan.FromSeconds(5),
            errorCodesToAdd: null);
    }));



builder.Services.AddScoped<IFormSacraService, FormSacraService>();
builder.Services.AddScoped<IInscripcionCatequesisService, InscripcionCatequesisService>();
builder.Services.AddScoped<IDonacionService, DonacionService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddScoped<IBautismoService, BautismoService>();
builder.Services.AddScoped<IComunionService, ComunionService>();
builder.Services.AddScoped<IConfirmacionService, ConfirmacionService>();
builder.Services.AddScoped<IMatrimonioService, MatrimonioService>();

builder.Services.AddControllers();
// Configure CORS to allow frontend requests during development
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    // In development skip HTTPS redirection to avoid port/SSL issues
}
else
{
    app.UseHttpsRedirection();
}

// Use CORS before other middleware that handles requests
app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();
