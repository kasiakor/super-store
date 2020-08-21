using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsStore.Domain.Entities;
using SuperStore.Domain.Entities;

namespace SuperStore.UnitTests
{
    [TestClass]
    public class CartTests
    {

        [TestMethod]
        public void Can_Add_New_Lines()
        {

            // Arrange - create some test products
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };

            // Arrange - create a new cart
            Cart target = new Cart();

            // Act
            //AddItem(Product product, int quantity)
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            //public IEnumerable<CartLine> Lines {get { return lineCollection; }}
            CartLine[] results = target.Lines.ToArray();

            // Assert
            // public class CartLine{public Product Product { get; set; }public int Quantity { get; set; }}
            Assert.AreEqual(results.Length, 2);
            Assert.AreEqual(results[0].Product, p1);
            Assert.AreEqual(results[1].Product, p2);
            Assert.AreEqual(results[0].Quantity, 1);
            Assert.AreEqual(results[1].Quantity, 1);
        }

        [TestMethod]
        public void Can_Remove_Line()
        {

            // Arrange - create some test products
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };
            Product p3 = new Product { ProductID = 3, Name = "P3" };

            // Arrange - create a new cart
            Cart target = new Cart();
            // Arrange - add some products to the cart
            target.AddItem(p1, 1);
            target.AddItem(p2, 3);
            target.AddItem(p3, 5);
            target.AddItem(p3, 1);

            // Act
            // public void RemoveLine(Product product){lineCollection.RemoveAll(l => l.Product.ProductID == product.ProductID);}
            target.RemoveLine(p3);

            // Assert
            Assert.AreEqual(target.Lines.Where(c => c.Product == p3).Count(), 0);
            Assert.AreEqual(target.Lines.Count(), 2);
        }

        [TestMethod]
        public void Calculate_Cart_Total()
        {

            // Arrange - create some test products
            Product p1 = new Product { ProductID = 1, Name = "P1", Price = 2.00M };
            Product p2 = new Product { ProductID = 2, Name = "P2", Price = 3.00M };
            Product p3 = new Product { ProductID = 3, Name = "P3", Price = 4.00M };

            // Arrange - create a new cart
            Cart target = new Cart();
            // Arrange - add some products to the cart
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p3, 1);
            target.AddItem(p1, 2);

            // Act
            //  public decimal ComputeTotalValue(){return lineCollection.Sum(e => e.Product.Price * e.Quantity);}
            target.ComputeTotalValue();

            // Assert
            Assert.AreEqual(target.Lines.Sum(e => e.Product.Price * e.Quantity), 13);
        }
    }
}
