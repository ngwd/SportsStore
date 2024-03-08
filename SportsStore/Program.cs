using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using SportsStore.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddMvc().AddControllersAsServices();

IConfiguration Config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange:true).Build();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(Config["SportStoreProducts:ConnectionString"]
));
// builder.Services.AddScoped<IProductRepository, FakeProductRepository>();
builder.Services.AddTransient<IProductRepository, EFProductRepository>();

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
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: null,
        pattern: "{category}/Page{productPage:int}",
        defaults: new { controller = "Product", action = "List"}
    );

    endpoints.MapControllerRoute(
        name: null,
        pattern: "Page{productPage:int}",
        defaults: new { controller = "Product", action = "List", productPage = 1 }
    );
    endpoints.MapControllerRoute(
        name: null,
        pattern: "{category}",
        defaults: new { controller = "Product", action = "List", productPage = 1 }
    );
    endpoints.MapControllerRoute(
        name: null,
        pattern: "",
        defaults: new { controller = "Product", action = "List", productPage = 1 }
    );
    endpoints.MapControllerRoute(name: null, pattern: "{controller}/{action}/{id?}");
});
app.UseAuthorization();
// SeedData.EnsurePopulated(app);
app.Run();
