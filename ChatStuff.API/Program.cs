using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ChatStuff.Infrastructure.Data;

using System.Text;
using ChatStuff.Core.Interfaces;
using ChatStuff.Infrastructure.Repositories;
using ChatStuff.Core.Services;
using ChatStuff.Core.Entities;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Register Entity Framework
builder.Services.AddIdentity<ChatStuffUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
//Register AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);

//Get the database connection string from appsettings.json
var connString = builder.Configuration.GetConnectionString("DefaultConnection");
//Register the DbContext with PostgreSQL provider
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connString));


//Configure JWT
var jwtKey = builder.Configuration.GetSection("JWTSettings:Key").Value; //TODO: add check if key is null
var key = Encoding.ASCII.GetBytes(jwtKey);

builder.Services.AddAuthentication(config =>
{
    config.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(config =>
    {
        config.RequireHttpsMetadata = false; //For development phase 
        config.SaveToken = true;
        config.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = false,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    }).AddCookie();

// Register Dependencies
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IFriendRepository, FriendRepository>();
builder.Services.AddScoped<IFriendServices, FriendService>();
builder.Services.AddScoped<ITokenClaimsService, TokenClaimsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
