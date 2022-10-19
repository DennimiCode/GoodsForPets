using System.Configuration;

using Microsoft.EntityFrameworkCore;

namespace GoodsForPets.Models
{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //ConfigurationManager.ConnectionStrings["GoodForPetsMSSQL"].ConnectionString
            //"Server= sql.bsite.net\\MSSQL2016;Database=backdennimi_GoodsForPets;User Id=backdennimi_GoodsForPets;Password=@X*9vNKkVp2^GP;"
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["GoodForPetsMSSQL"].ConnectionString);
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<BaseMaterial> BaseMaterials { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<GrocerySet> GrocerySets { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Mixture> Mixtures { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductInfo> ProductsInfo { get; set; }
        public DbSet<RawMaterial> RawMaterials { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<SubProduct> SubProducts { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
