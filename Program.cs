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

builder.Services.AddScoped<IBautismoService, BautismoService>();
builder.Services.AddScoped<IComunionService, ComunionService>();
builder.Services.AddScoped<IConfirmacionService, ConfirmacionService>();
builder.Services.AddScoped<IMatrimonioService, MatrimonioService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        policy =>
        {
            policy.WithOrigins(
                    "http://localhost:5173"
                )
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors("AllowSpecificOrigins");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
