using SuperStore.Domain.Abstract;
using SuperStore.Domain.Entities;
using System.Collections.Generic;

namespace SuperStore.Domain.Concrete
{
    //repository class. It implements the IProductRepository interface and uses an instance of EFDbContext to retrieve data from the database using the EF
    public class EFProductRepository : IProductRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Product> Products
        {
            get { return context.Products; }
        }

        public void SaveProduct(Product product)
        {
           if (product.ProductID == 0)
            {
                context.Products.Add(product);
            }

           else
            {
                Product dbEntry = context.Products.Find(product.ProductID);

                if (dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;
                }
            }
            context.SaveChanges();
        }
    }
}
