// code: fatih.unal date: 2025-04-21T16:39:30
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

var builder = WebApplication.CreateBuilder(args);

// Infrastructure ve Application servis kayıtları
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
               .AddEntityFrameworkStores<AppIdentityDbContext>()
               .AddDefaultTokenProviders();

// Presentation katmanına özgü servisler
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
