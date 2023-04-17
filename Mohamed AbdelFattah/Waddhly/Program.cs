using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Waddhly.Data;
using Waddhly.Helpers;
using Waddhly.Models;
using Waddhly.Services;
using Waddhly.Services.Chat;
using Waddhly.UtilityService;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddScoped<IEmailsender, EmailService>();


builder.Services.AddControllers();
string dbConfig = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Configuration.GetSection("JWT");

builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(dbConfig);
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(o =>
    {
        o.RequireHttpsMetadata = false;
        o.SaveToken = false;
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["JwtAuthentication:Issuer"],
            ValidAudience = builder.Configuration["Jw,tAuthentication:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
        };
    }
    );

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddCors(options =>

{

	options.AddPolicy(name: "CorsPolicyAllHosts",

	builder =>

	{

		builder.AllowAnyOrigin();

		builder.AllowAnyMethod();

		builder.AllowAnyHeader();

	});

});


var app = builder.Build();
app.UseCors("CorsPolicyAllHosts");
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
app.MapHub<ChatHub>("/Chat");

app.Run();
