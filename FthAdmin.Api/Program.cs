// code: fatih.unal date: 2025-04-22
using FthAdmin.Api.ErrorHandling.Handlers;
using FthAdmin.Api.ErrorHandling.Middlewares;
using FthAdmin.Application.DependencyInjection;
using FthAdmin.Infrastructure;
using MediatR;
using FthAdmin.Application.Features.Auth.Commands;
using FthAdmin.Infrastructure.Features.Auth.Commands;
using FthAdmin.Infrastructure.Identity;
using FthAdmin.Infrastructure.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using AutoMapper;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Infrastructure ve Application servis kayıtları
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();

// ServerRepository DI kaydı
builder.Services.AddScoped<FthAdmin.Domain.Repositories.IGenericRepository<FthAdmin.Domain.Entities.Server, System.Guid>, FthAdmin.Persistence.Repositories.ServerRepository>();

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
               .AddEntityFrameworkStores<AppIdentityDbContext>()
               .AddDefaultTokenProviders();

// JWT Authentication pipeline
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SuperSecretKey_12345")) // Bunu config'den oku
        };
    });

// Presentation katmanına özgü servisler
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// AutoMapper servisi DI container'a eklendi
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Hata yönetimi DI kaydı
builder.Services.AddScoped<HttpExceptionHandler>();

var app = builder.Build();

// HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

// Hata yönetimi middleware'i
app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();
