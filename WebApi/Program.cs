using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebApi.Config;
using WebApi.Entities;
using WebApi.Repository;
using WebApi.Token;

var builder = WebApplication.CreateBuilder( args );

// Add services to the container.

builder.Services.AddControllers( );
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer( );
builder.Services.AddSwaggerGen( );


builder.Services.AddDbContext<ContextBase>( options =>
               options.UseSqlServer(
                   builder.Configuration.GetConnectionString( "DefaultConnection" ) ) );

builder.Services.AddDefaultIdentity<ApplicationUser>( options =>
{
    options.SignIn.RequireConfirmedAccount = false;
} ).AddEntityFrameworkStores<ContextBase>( );


// Interfaces e repositorios
builder.Services.AddSingleton<InterfaceProduct, RepositoryProduct>( );

builder.Services.AddAuthentication( JwtBearerDefaults.AuthenticationScheme )
    .AddJwtBearer( option =>
    {
        option.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = "Teste.Securiry.Bearer",
            ValidAudience = "Teste.Securiry.Bearer",
            IssuerSigningKey = JwtSecurityKey.Create( "43443FDFDF34DF34343fdf344SDFSDFSDFSDFSDF4545354345SDFGDFGDFGDFGdffgfdGDFGDGR%" )
        };
        
        option.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine( "OnAuthenticationFailed: " + context.Exception.Message );
                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                Console.WriteLine( "OnTokenValidated: " + context.SecurityToken );
                return Task.CompletedTask;
            }
        };
    } );

var app = builder.Build( );

// Configure the HTTP request pipeline.
if ( app.Environment.IsDevelopment( ) )
{
    app.UseSwagger( );
    app.UseSwaggerUI( );
}

app.UseHttpsRedirection( );

app.UseAuthentication( );

app.UseAuthorization( );

app.MapControllers( );

app.Run( );
