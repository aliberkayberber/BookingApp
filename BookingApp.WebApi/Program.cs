using BookingApp.Business.DataProtection;
using BookingApp.Business.Operations.User;
using BookingApp.Data.Context;
using BookingApp.Data.Repositories;
using BookingApp.Data.UnitOfWork;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<BookingAppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>)); // generic olduðu için typeof kullandýk

builder.Services.AddScoped<IUnitOfWork , UnitOfWork>();

builder.Services.AddScoped<IUserService , UserManager>();

builder.Services.AddScoped<IDataProtection , DataProtection>();

var keysDirectory = new DirectoryInfo(Path.Combine(builder.Environment.ContentRootPath, "App_Data", "Keys"));

builder.Services.AddDataProtection()
       .SetApplicationName("BookingApp")
       .PersistKeysToFileSystem(keysDirectory);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
