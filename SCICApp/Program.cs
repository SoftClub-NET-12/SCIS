using Infrastructure.Data;
using Infrastructure.Services.CategoryService;
using Infrastructure.Services.LocationService;
using Infrastructure.Services.PriceHistoryService;
using Infrastructure.Services.ProductService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(configure =>
{
    configure.UseNpgsql(connectionString:
        builder.Configuration.GetConnectionString("Connection")
    );
});



// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<ILocationService, LocationService>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IPriceHistoryService, PriceHistoryService>();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.Run();
