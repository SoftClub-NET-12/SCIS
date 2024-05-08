using Infrastructure.Data;
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
