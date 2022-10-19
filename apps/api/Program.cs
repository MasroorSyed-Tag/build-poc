using ElectronNET.API;
using ElectronNET.API.Entities;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseElectron(args);
// Add services to the container.

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");;

if (HybridSupport.IsElectronActive)
{
  var options = new BrowserWindowOptions
  {
    Show = false,
    Icon = "favicon.ico"
  };

  var window = await Electron.WindowManager.CreateWindowAsync();

  window.OnClose += () =>
  {
    Electron.App.Quit();
  };
}

app.Run();
