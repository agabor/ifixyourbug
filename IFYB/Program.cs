using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Net.Mail;
using System.Net;
using IFYB.Services;
using System.Security.Claims;
using IFYB;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL")));

var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["JwtKey"]));

builder.Services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = builder.Configuration["JwtIssuer"],
                    ValidAudience = builder.Configuration["JwtAudience"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key
                };
            });
builder.Services.AddSingleton<SecurityKey>(key);


builder.Services.AddAuthorization(options =>
{
   options.AddPolicy(Policies.AdminOnly, policy => policy.RequireClaim(ClaimTypes.Role, Roles.Admin));
   options.AddPolicy(Policies.ClientOnly, policy => policy.RequireClaim(ClaimTypes.Role, Roles.Client));
});

builder.Services.AddScoped<SmtpClient>(provider => {
    var smtpClient = new SmtpClient("email-smtp.eu-central-1.amazonaws.com", 587);
    smtpClient.EnableSsl = true;
    smtpClient.Credentials = new NetworkCredential(builder.Configuration["AwsSmtpUserName"], builder.Configuration["AwsSmtpPassword"]);
    return smtpClient;
});

builder.Services.AddScoped<EmailService>();

var app = builder.Build();

app.Use(async (context, next) =>
{
    try
    {
        await next.Invoke();
    } catch (UnauthorizedAccessException)
    {
        context.Response.StatusCode = 401;
    }
});

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseSpa(spa =>
    {
        spa.UseProxyToSpaDevelopmentServer("http://localhost:8080");
    });
}

app.Run();
