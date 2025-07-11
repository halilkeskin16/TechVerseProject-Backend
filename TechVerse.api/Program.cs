
using Microsoft.EntityFrameworkCore;
using TechVerse.Api.Data;

var builder = WebApplication.CreateBuilder(args);

// ---- SERVİSLERİ EKLEME BÖLÜMÜ ----

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ---- HTTP İSTEK PİPELINE'I YAPILANDIRMA BÖLÜMÜ ----
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();