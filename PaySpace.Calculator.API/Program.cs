using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using PaySpace.Calculator.API.Security;
using PaySpace.Calculator.Data;
using PaySpace.Calculator.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    // Configure JWT bearer authentication options here
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "Issuer",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("eyJhbGciOiJIUzI1NiJ9.eyJSb2xlIjoiQWRtaW4iLCJJc3N1ZXIiOiJJc3N1ZXIiLCJVc2VybmFtZSI6IlBheXNjYWxlQmFja2VuZCIsImV4cCI6MTcwOTc5NTIyMywiaWF0IjoxNzA5Nzk1MjIzfQ.27sePOp_uT53IpKhNDaomKyBLoDz_KT5TQ4NI0oWhDs"))
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(DummyTokenAuthorizationPolicy.PolicyName, policy =>
    {
        policy.Requirements.Add(new DummyTokenAuthorizationRequirement());
    });
});

builder.Services.AddSingleton<IAuthorizationHandler, DummyTokenAuthorizationHandler>();

builder.Services.AddMapster();

builder.Services.AddCalculatorServices();
builder.Services.AddDataServices(builder.Configuration);

var app = builder.Build();



app.UseHttpsRedirection();
app.UseRouting();


app.UseAuthorization();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();
app.InitializeDatabase();

app.Run();
