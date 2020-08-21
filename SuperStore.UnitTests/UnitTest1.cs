using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SuperStore.Domain.Abstract;
using SuperStore.Domain.Entities;
using SuperStore.WebUI.Controllers;
using SuperStore.WebUI.HtmlHelpers;
using SuperStore.WebUI.Models;

namespace SuperStore.UnitTests
{

    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void Can_Paginate()
        {

            // Arrange
            //mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
            new Product {ProductID = 1, Name = " P1"},
            new Product {ProductID = 2, Name = "P2"},
            new Product {ProductID = 3, Name = "P3"},
            new Product {ProductID = 4, Name = "P4"},
            new Product {ProductID = 5, Name = "P5"}
            });
            //mock repository injected to the constructor of the class
            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            // Act
            //we call List method to request a specific page
            //IEnumerable<Product> result = (IEnumerable<Product>)controller.List(2).Model;
            //ProductsListViewModel result = (ProductsListViewModel)controller.List(2).Model;
            ProductsListViewModel result = (ProductsListViewModel)controller.List(null, 2).Model;


            // Assert
            Product[] prodArray = result.Products.ToArray();
            Assert.IsTrue(prodArray.Length == 2);
            Assert.AreEqual(prodArray[0].Name, "P4");
            Assert.AreEqual(prodArray[1].Name, "P5");
        }

        [TestMethod]
        public void Can_Generate_Page_Links()
        {

            // Arrange - define an HTML helper - we need to do this
            // in order to apply the extension method
            HtmlHelper myHelper = null;

            // Arrange - create PagingInfo data
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 1,
                TotalItems = 16,
                ItemsPerPage = 10
            };

            // Arrange - set up the delegate using a lambda expression
            Func<int, string> pageUrlDelegate = i => "Page" + i;

            // Act
            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            // Assert
            //Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>"+@"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>"+@"<a class=""btn btn-default"" href=""Page3"">3</a>",
            //    result.ToString());
            Assert.AreEqual(@"<a class=""btn btn-default btn-primary selected"" href=""Page1"">1</a>" + @"<a class=""btn btn-default"" href=""Page2"">2</a>",
              result.ToString());
        }

        [TestMethod]
        public void Can_Send_Pagination_View_Model()
        {

            // Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
            new Product {ProductID = 1, Name = "P1"},
            new Product {ProductID = 2, Name = "P2"},
            new Product {ProductID = 3, Name = "P3"},
            new Product {ProductID = 4, Name = "P4"},
            new Product {ProductID = 5, Name = "P5"}
            });

            // Arrange
            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            // Act
            //ProductsListViewModel result = (ProductsListViewModel)controller.List(1).Model;
            ProductsListViewModel result = (ProductsListViewModel)controller.List(null, 1).Model;

            // Assert
            PagingInfo pageInfo = result.PagingInfo;
            Assert.AreEqual(pageInfo.CurrentPage, 1);
            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
            Assert.AreEqual(pageInfo.TotalItems, 5);
            Assert.AreEqual(pageInfo.TotalPages, 2);
        }

        [TestMethod]
        public void Indicates_Selected_Category()
        {

            // Arrange
            //mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
            new Product {ProductID = 1, Name = " P1", Category = "Shoes"},
            new Product {ProductID = 2, Name = "P2", Category = "Tops"}
            });
            //mock repository injected to the constructor of the class
            //create the controller
            NavController target = new NavController(mock.Object);

            //define the category to be selected`1
            string categoryToSelect = "Shoes";

            // Act
            string result = target.Menu(categoryToSelect).ViewBag.SelectedCategory;

            // Assert
            Assert.AreEqual(categoryToSelect, result);
        }

        [TestMethod]
        public void Generate_Category_Specific_Product_Count()
        {

            // Arrange
            //mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
            new Product {ProductID = 1, Name = " P1", Category = "Shoes"},
            new Product {ProductID = 2, Name = "P2", Category = "Tops"},
            new Product {ProductID = 3, Name = " P1", Category = "Shoes"},
            new Product {ProductID = 4, Name = "P2", Category = "Tops"},
            new Product {ProductID = 5, Name = " P1", Category = "Shoes"},
            new Product {ProductID = 6, Name = "P2", Category = "Tops"},
            new Product {ProductID = 7, Name = "P2", Category = "Tops"}
            });
            //mock repository injected to the constructor of the class
            //create the controller
            ProductController controller = new ProductController(mock.Object);

            //define the category to be selected`1
            string categoryToSelect1 = "Shoes";
            string categoryToSelect2 = "Tops";

            // Act
            int result1 =((ProductsListViewModel)controller.List(categoryToSelect1).Model).PagingInfo.TotalItems;
            int result2 = ((ProductsListViewModel)controller.List(categoryToSelect2).Model).PagingInfo.TotalItems;
            int result3 = ((ProductsListViewModel)controller.List(null).Model).PagingInfo.TotalItems;

            // Assert
            Assert.AreEqual(result1, 3);
            Assert.AreEqual(result2, 4);
            Assert.AreEqual(result3, 7);
        }
    }
}
