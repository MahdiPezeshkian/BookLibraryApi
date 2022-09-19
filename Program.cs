using BookLibraryApi.DataBaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using BookLibraryApi.Repository;
using BookLibraryApi.Data;
using BookLibraryApi.Hash;
using BookLibraryApi.Generatetoken;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var contectionString = builder.Configuration.GetConnectionString("BookLibrary");

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddScoped<TokenGenerator>();
builder.Services.AddScoped<HashAlgorithm>();
builder.Services.AddScoped<BookShelfRepository>();
builder.Services.AddScoped<BookRepository>();
builder.Services.AddScoped<UserRepository>();

builder.Services.AddDbContext<Context>(options =>
    {
        options.UseSqlServer(contectionString);
    });

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(option =>
    {
        option.RequireHttpsMetadata = false;
        option.SaveToken = true;

        option.TokenValidationParameters = new TokenValidationParameters()
        {
            ClockSkew = TimeSpan.Zero,
            RequireExpirationTime = true,
            ValidateLifetime = true,
            ValidateIssuer = true,
            ValidIssuer = "SampleJwtIssuer",
            ValidateAudience = true,
            ValidAudience = "SampleJwtAudience",
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("akndgakl98qauhf9qhwhasufh9ush0SHF08ASHFAUHSF8")),

        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(
    endpoint => endpoint.MapControllers()
);

SeedData.PopulateDb(app);
app.Run();
