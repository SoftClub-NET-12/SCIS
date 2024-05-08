using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class DataContext:DbContext
{

    public DataContext(DbContextOptions<DataContext> options) :base(options)
    {
        
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<PriceHistory> PriceHistories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductSupplier> ProductSuppliers { get; set; }
    public DbSet<Purchase> Purchases { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<Stock> Stocks { get; set; }
    public DbSet<StockProduct> StockProducts { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<User> Users { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
