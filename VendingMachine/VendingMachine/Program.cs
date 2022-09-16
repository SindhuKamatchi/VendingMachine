
using System.Globalization;
using System.Security.Claims;
using System.ServiceModel;
using System.Text;
using System.Xml;
using VendingMachine.Models;
using VendingMachine.Models.ConfigData;

var builder = WebApplication.CreateBuilder(args);

//var config = builder.Configuration.Get<ConfigData>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Configuration.GetSection("VendingServiceSettings").Get<VendingServiceSettings>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
