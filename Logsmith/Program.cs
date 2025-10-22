using Logsmith.Api.Data;
using Logsmith.Api.Interface;
using Logsmith.Api.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// 의존성 주입
builder.Services.AddScoped<IEventService, EventService>();

// DbContext 등록
var conn = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<LogSmithDbContext>(op => op.UseSqlServer(conn));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<LogSmithDbContext>();
    db.Database.Migrate();
}

app.Run();
