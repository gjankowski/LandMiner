using LandMines.BusinessLogic;
using LandMines.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddSingleton<PlayerFactory>();
builder.Services.AddSingleton<GameEngine>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
       Path.Combine(Directory.GetCurrentDirectory(), "images")),
    RequestPath = "/images"
});

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");


app.Run();
