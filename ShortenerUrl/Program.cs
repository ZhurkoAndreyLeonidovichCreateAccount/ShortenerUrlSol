using Microsoft.EntityFrameworkCore;
using ShortenerUrl.BLL;
using ShortenerUrl.BLL.Interfaces;
using ShortenerUrl.BLL.Providers;
using ShortenerUrl.BLL.Services;
using ShortenerUrl.DAL.Data;
using ShortenerUrl.DAL.Interfaces;
using ShortenerUrl.DAL.Repository;
using ShortenerUrl.DAL.Services;
using System.Diagnostics.CodeAnalysis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(typeof(AppMappingProfile));
var connectingString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => 
{
    options.UseMySql(connectingString, ServerVersion.AutoDetect(connectingString));
});
builder.Services.AddScoped<IShortendUrlRepository, ShortendUrlRepository>();
builder.Services.AddScoped<UrlShorteningProvider>();
builder.Services.AddScoped<IServiceShortenerUrl, ServiceShortenerUrl>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHostedService<AppDbInitializingService>();
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

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=ShortendUrls}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=ShortendUrls}/{action=Index}");

    endpoints.MapControllerRoute(
        name: "shortUrlRoute",
        pattern: "{code}",
        defaults: new { controller = "ShortendUrls", action = "GoToSite" });
        //constraints: new { shortUrl = new ShortUrlConstraint() }); // ShortUrlConstraint - пользовательская реализация IActionConstraint
});

app.Run();


