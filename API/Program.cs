using System.Text;
using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<ITokenService,TokenService>();
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddCors();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
   .AddJwtBearer(options =>
   {
        var tokenKey=builder.Configuration["TokenKey"] 
            ?? throw new Exception("Token Not found - Program.cs");
        options.TokenValidationParameters= new TokenValidationParameters 
        {
            ValidateIssuerSigningKey=true,
            IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)),
            ValidateIssuer=false,
            ValidateAudience=false
        };
   });
var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

app.UseCors(x=>x.AllowAnyHeader().AllowAnyMethod()
        .WithOrigins("http://localhost:4200" ,"https://localhost:4200"));
// Configure the HTTP request pipeline.


app.UseHttpsRedirection();


app.MapControllers();

app.Run();
