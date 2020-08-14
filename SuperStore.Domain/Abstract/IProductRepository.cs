using SuperStore.Domain.Entities;
using System.Collections.Generic;

namespace SuperStore.Domain.Abstract
{
   public  interface IProductRepository
    {
        // A class that depends on the IProductRepository interface can obtain Product objects without needing to know anything about where they are coming from or how the implementation class will deliver them
        IEnumerable<Product> Products { get; }
    }
}
