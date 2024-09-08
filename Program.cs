using CarrinhoAPI.Data;
using CarrinhoAPI.Models;
using CarrinhoAPI.Repository.DataBase;
using CarrinhoAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddScoped<IEndPointCrud<CongregacaoModel>,DAL<CongregacaoModel>>();

builder.Services.AddEntityFrameworkSqlServer()
    .AddDbContext<Context>(
        options => options.UseSqlServer(builder.Configuration.GetConnectionString("DataBase"))
    );
builder.Services.AddScoped(typeof(DAL<>));

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

app.Run();
