using Microsoft.EntityFrameworkCore;
using ShoppingMall.Models;
using ShoppingMall.Repositories;
using ShoppingMall.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ShoppingmallContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SchoolContext"), contextOptionsBuilder =>
            {
                contextOptionsBuilder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                options.LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Name }, LogLevel.Information);
            }));

builder.Services.AddTransient<ICatalogService, CatalogService>();
builder.Services.AddTransient<IFileService, FileService>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
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
        name: "MyArea",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Products}/{action=Index}/{id?}");

app.Run();
