using System;
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
    public class ImageTests
    {
        [TestMethod]
        public void Can_Retrieve_Image_Data()
        {
            //Arrange new product with image data
            Product product = new Product
            {
                ProductID = 2,
                Name = "Test",
                ImageData = new byte[] { },
                ImageMimeType = "image/png"
            };
            // Arrange
            //mock repository
            //AsQueryable() - converts an IEnumerable to an IQueryable
            //An IQueryable will give deferred or lazy, non-cached data whereas the IEnumerable will give  immediate or eager, cached data
            //IEnumerable is suitable for querying data from in-memory collections like List, Array and so on
            //IQueryable is suitable for querying data from out-memory (like remote database, service) collections
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
            new Product {ProductID = 1, Name = " P1"},
            product,
            new Product {ProductID = 3, Name = "P3"}
            }.AsQueryable());

            // Arrange
            ProductController target = new ProductController(mock.Object);

            //Act call GetImage action
            //public FileContentResult GetImage(int productId)
            ActionResult result = target.GetImage(2);

            //Assert
            //FileResult represents a base class that is used to send binary file content to the response
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(FileResult));
            //product.ImageMimeType = image.ContentType
            Assert.AreEqual(product.ImageMimeType, ((FileResult)result).ContentType);

            Trace.WriteLine(result);
            //System.Web.Mvc.FileContentResult

            Trace.WriteLine(product.ImageMimeType);
            //image/png

            Trace.WriteLine(product.ImageData);
            //System.Byte[]

            Trace.WriteLine(product.Name);
            //Test

        }
    }
}
