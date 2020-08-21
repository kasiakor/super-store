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
    }
}
