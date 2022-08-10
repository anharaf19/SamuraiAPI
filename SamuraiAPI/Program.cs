using Microsoft.EntityFrameworkCore;
using SamuraiAPI.Data;
using SamuraiAPI.Data.DAL;
using SamuraiAPI.Helpers;
using SamuraiAPI.Services;
using System.Text.Json.Serialization;
using UsersAPI.Data.DAL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//menambahkan konfigurasi EF
builder.Services.AddDbContext<SamuraiDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("SamuraiConnection")).EnableSensitiveDataLogging());

//menambahkan automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//inject class DAL
builder.Services.AddScoped<ISamurai, SamuraiDAL>();
builder.Services.AddScoped<ISword, SwordDAL>();
builder.Services.AddScoped<IElement, ElementDAL>();
builder.Services.AddScoped<IUser, UserDAL>();

// configure strongly typed settings object
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

// configure DI for application services
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

//app.UseHttpsRedirection();

//app.UseAuthorization();


app.UseMiddleware<JwtMiddleware>();

app.MapControllers();

app.Run();
