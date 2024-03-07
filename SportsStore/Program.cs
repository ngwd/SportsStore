using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using SportsStore.Models;

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=List}/{id?}");

app.MapControllerRoute(
    name: "pagination",
    pattern: "Products/Page{productPage}",
    defaults: new { controller = "Product", action = "List" }
);

/*
app.UseMvc( routes => {
    routes.MapRoute(
        name: "pagination",
        template: "Products/Page{productPage}",
        default:new { controller = "Product", action = "List" }
    );
    routes.MapRoute(
        name: "default",
        template: "{controller=Product}/{action=List}/{id?}"
    );
})
*/

// SeedData.EnsurePopulated(app);
app.Run();
