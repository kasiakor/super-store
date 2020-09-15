using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SuperStore.Domain.Abstract;
using SuperStore.Domain.Entities;
using SuperStore.WebUI.Controllers;

namespace SuperStore.UnitTests
{
    [TestClass]
    public class AdminTest
    {
        [TestMethod]
        public void Index_Contains_All_Products()
        {
            //Arrange - create mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
            new Product {ProductID = 1, Name = "P1"},
            new Product {ProductID = 2, Name = "P2"},
            new Product {ProductID = 3, Name = "P3"},
            new Product {ProductID = 4, Name = "P4"},
            new Product {ProductID = 5, Name = "P5"}
            });
            //Arrange - create a controller
            AdminController target = new AdminController(mock.Object);

            //Action
            IEnumerable<Product> result = (IEnumerable<Product>)target.Index().Model;

            //Assert
            Product[] myResults = result.ToArray();
            Assert.IsTrue(myResults.Length == 5);
            Assert.AreEqual(myResults[0].Name, "P1");


            Trace.WriteLine(result);
            //SuperStore.Domain.Entities.Product[]

            Trace.WriteLine(myResults);
            //SuperStore.Domain.Entities.Product[]
        }


        [TestMethod]
        public void Can_Edit_Product()
        {

            // Arrange - create the mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"},
            });

            // Arrange - create the controller
            AdminController target = new AdminController(mock.Object);

            // Act
            //public ViewResult Edit(int productId)
            Product p1 = target.Edit(1).ViewData.Model as Product;
            Product p2 = target.Edit(2).ViewData.Model as Product;
            Product p3 = target.Edit(3).ViewData.Model as Product;

            // Assert
            Assert.AreEqual(1, p1.ProductID);
            Assert.AreEqual(2, p2.ProductID);
            Assert.AreEqual(3, p3.ProductID);

            Trace.WriteLine(p1.ProductID);
            //1
        }
              [TestMethod]
        public void Cannot_Edit_Nonexistent_Product()
        {

            // Arrange - create the mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"},
            });

            // Arrange - create the controller
            AdminController target = new AdminController(mock.Object);

            // Act
            //public ViewResult Edit(int productId)
           // Product p4 = target.Edit(4).ViewData.Model as Product;
            Product p4 = (Product)target.Edit(4).ViewData.Model;

            // Assert
            Assert.IsNull( p4);

            Trace.WriteLine(p4);
        }

        [TestMethod]
        public void Can_Save_Valid_Changes()
        {

            // Arrange - create mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            // Arrange - create the controller
            AdminController target = new AdminController(mock.Object);
            // Arrange - create a product
            Product product = new Product { Name = "Test" };

            // Act - try to save the product
            //ActionResult represents the result of the action method 
            ActionResult result = target.Edit(product);

            // Assert - check that the repository was called
            //verifies that a specific invocation was performed on mock
            mock.Verify(m => m.SaveProduct(product));
            // Assert - check the method result type
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));

            Trace.WriteLine(product);
            //SuperStore.Domain.Entities.Product

            Trace.WriteLine(product.Name);
            //Test

            Trace.WriteLine(product.Description);
            // empty " "

            Trace.WriteLine(result);
            //System.Web.Mvc.RedirectToRouteResult

            Trace.WriteLine(mock);
            //Moq.Mock`1[SuperStore.Domain.Abstract.IProductRepository]
        }
    }
}
