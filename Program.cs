using BookLibraryApi.DataBaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using BookLibraryApi.Repository;
using BookLibraryApi.Data;
using BookLibraryApi.Hash;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var contectionString = builder.Configuration.GetConnectionString("BookLibrary");

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<HashAlgorithm>();
builder.Services.AddScoped<BookShelfRepository>();
builder.Services.AddScoped<BookRepository>();
builder.Services.AddScoped<UserRepository>();

builder.Services.AddDbContext<Context>(options => 
    {
        options.UseSqlServer(contectionString);
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
app.UseAuthorization();

app.UseEndpoints(
    endpoint => endpoint.MapControllers()
);

SeedData.PopulateDb(app);
app.Run();
