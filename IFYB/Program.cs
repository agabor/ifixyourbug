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
using Stripe;
using System.Threading.Channels;
using IFYB.HostedServices;
using IFYB.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSystemd(); 

var stripeOptions = builder.Configuration.GetSection(StripeOptions.Stripe).Get<StripeOptions>();
StripeConfiguration.ApiKey = stripeOptions.ApiKey;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL")));
builder.Services.Configure<AppOptions>(builder.Configuration.GetSection(AppOptions.Host));
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(JwtOptions.Jwt));
builder.Services.Configure<BillingOptions>(builder.Configuration.GetSection(BillingOptions.Billing));

var priceService = new PriceService();
var prices = priceService.List();
var eurPrice = prices.First(p => p.Currency == "eur");
var usdPrice = prices.First(p => p.Currency == "usd");
stripeOptions.EurPriceId = eurPrice.Id;
stripeOptions.UsdPriceId = usdPrice.Id;

var offer = new OfferDto
{
    EurPrice = eurPrice.UnitAmountDecimal!.Value / 100,
    UsdPrice = usdPrice.UnitAmountDecimal!.Value / 100
}; 

builder.Services.AddSingleton(offer);
var configSection = builder.Configuration.GetSection(StripeOptions.Stripe);
configSection["EurPriceId"] = eurPrice.Id;
configSection["UsdPriceId"] = usdPrice.Id;
builder.Services.Configure<StripeOptions>(configSection);

var jwtOptions = builder.Configuration.GetSection(JwtOptions.Jwt).Get<JwtOptions>();
var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtOptions.Key));

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
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
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

var awsOptions = builder.Configuration.GetSection(AwsOptions.Aws).Get<AwsOptions>();
builder.Services.AddScoped<SmtpClient>(provider => {
    var smtpClient = new SmtpClient(awsOptions.SmtpHost, awsOptions.SmtpPort);
    smtpClient.EnableSsl = true;
    smtpClient.Credentials = new NetworkCredential(awsOptions.SmtpUserName, awsOptions.SmtpPassword);
    return smtpClient;
});

builder.Services.AddScoped<EmailCreationService>();
builder.Services.AddScoped<EmailSenderService>();
builder.Services.AddSingleton<Channel<IFYB.Entities.Order>>(Channel.CreateUnbounded<IFYB.Entities.Order>());
builder.Services.AddHostedService<BillingService>();

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
