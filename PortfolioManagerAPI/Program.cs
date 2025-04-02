using PortfolioManagerAPI.Data;
using Microsoft.EntityFrameworkCore;
using PortfolioManagerAPI.Mappers;
using PortfolioManagerAPI.Repository.IRepository;
using PortfolioManagerAPI.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("DbConnection"),
    new MySqlServerVersion(new Version(8, 0, 25)));
});

// Add services to the container.
builder.Services.AddScoped<IAssetRepository, AssetRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddAutoMapper(typeof(PortfolioManagerMapper));
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseCors(builder =>
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
