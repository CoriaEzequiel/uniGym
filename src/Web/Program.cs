using Application.Interfaces;
using Application.Models.Helpers;
using Application.Services;
using Domain.Interfaces;
using Infrastructure.Context;
using Infrastructure.Data;
using Infrastructure.ThirdServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

#region Configuraci�n de controladores con opciones JSON
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.WriteIndented = true;
    });
#endregion

#region Configuraci�n de DbContext
builder.Services.AddDbContext<uniContext>(x => x.UseSqlite(builder.Configuration.GetConnectionString("uniContextDbConnection")));
#endregion

#region Configuraci�n de autenticaci�n
builder.Services.Configure<AuthenticationServiceOptions>(builder.Configuration.GetSection("AuthenticationServiceOptions"));
#endregion

#region Configuraci�n de Swagger
builder.Services.AddSwaggerGen(setupAction =>
{
    setupAction.AddSecurityDefinition("uniApiBearerAuth", new OpenApiSecurityScheme()
    {
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        Description = "Please, paste the token to login for use all endpoints."
    });

    setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "uniApiBearerAuth"
                }
            },
            new List<string>()
        }
    });
});
#endregion

#region Configuraci�n de autenticaci�n con JWT
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["AuthenticationServiceOptions:Issuer"],
            ValidAudience = builder.Configuration["AuthenticationServiceOptions:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["AuthenticationServiceOptions:SecretForKey"]!))
        };
    });
#endregion

#region Configuraci�n de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});
#endregion

#region Configuraci�n de pol�ticas de autorizaci�n
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("VipClientOnly", policy => policy.RequireClaim("TypeUser", "VipClient"));
    options.AddPolicy("ProfessorOnly", policy => policy.RequireClaim("TypeUser", "Professor"));
    options.AddPolicy("SuperAdmOnly", policy => policy.RequireClaim("TypeUser", "SuperAdm"));

    options.AddPolicy("VipClientOrSuperAdm", policy =>
        policy.RequireAssertion(context =>
            context.User.HasClaim("TypeUser", "VipClient") ||
            context.User.HasClaim("TypeUser", "SuperAdm")));

    options.AddPolicy("ProfessorOrSuperAdm", policy =>
        policy.RequireAssertion(context =>
            context.User.HasClaim("TypeUser", "Professor") ||
            context.User.HasClaim("TypeUser", "SuperAdm")));

    options.AddPolicy("VipClientOrProfessorOrSuperAdm", policy =>
        policy.RequireAssertion(context =>
            context.User.HasClaim("TypeUser", "VipClient") ||
            context.User.HasClaim("TypeUser", "Professor") ||
            context.User.HasClaim("TypeUser", "SuperAdm")));
});
#endregion

#region Configuraci�n de servicios de aplicaci�n e infraestructura

builder.Services.AddScoped<IProfessorRepository, ProfessorRepository>();
builder.Services.AddScoped<IProfessorService, ProfessorService>();
builder.Services.AddScoped<IMeetingService, MeetingService>();
builder.Services.AddScoped<IMeetingRepository, MeetingRepository>();
builder.Services.AddScoped<IVipClientService, VipClientService>();
builder.Services.AddScoped<IVipClientRepository, VipClientRepository>();
builder.Services.AddScoped<ISuperAdm, SuperAdm>();
builder.Services.AddScoped<ISuperAdmRepository, SuperAdmRepository>();
#endregion

var app = builder.Build();

#region Configuraci�n de middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

// Habilitaci�n de CORS
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();
#endregion

app.Run();
