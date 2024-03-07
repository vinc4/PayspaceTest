using Microsoft.AspNetCore.Components;
using PaySpace.Calculator.Web.Services;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddHttpClient();
builder.Services.AddControllersWithViews();
builder.Services.AddCalculatorHttpServices();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(name: "default", pattern: "{controller=Calculator}/{action=Index}/{id?}");

app.Run();