using DataBase.APPCarrinho.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Modelos.APPCarrinho.Modelos.User;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        //policy.WithOrigins("https://localhost:7163")
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddScoped<IEndPointCrud<CongregacaoModel>,DAL<CongregacaoModel>>();

builder.Services.AddEntityFrameworkSqlServer()
    .AddDbContext<AppCarrinhoDBContext>(
        options => options.UseSqlServer(builder.Configuration.GetConnectionString("DataBase"))
    );
builder.Services.AddScoped(typeof(DataBase.APPCarrinho.Data.DAL<>));

// Configuração do Identity
builder.Services.AddIdentity<UsuarioLoginModels, IdentityRole>()
    .AddEntityFrameworkStores<AppCarrinhoDBContext>()
    .AddDefaultTokenProviders();

// Configuração do JWT
var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true
    };
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Use the CORS middleware before routing
app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();


// Usar autenticação e autorização
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
