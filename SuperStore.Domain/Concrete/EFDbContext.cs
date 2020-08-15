using SuperStore.Domain.Entities;
using System.Data.Entity;

namespace SuperStore.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        //DbContext class automatically defines a property for the table
        //the name of the property specifies the table: Products
        //the type parameter of the DbSet result specifies the model type: Product
        // EF should use the Product model type to represent rows in the Products table
        public DbSet<Product> Products { get; set; }
    }
}
