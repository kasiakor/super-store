﻿using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SuperStore.Domain.Abstract;
using SuperStore.Domain.Entities;
using SuperStore.WebUI.Controllers;
using SuperStore.WebUI.Models;

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

        [TestMethod]
        public void Can_Clear_Contents()
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
            //public void Clear(){lineCollection.Clear();}
            target.Clear();

            // Assert
            Assert.AreEqual(target.Lines.Count(), 0);
        }

        //[TestMethod]
        //public void Can_Add_To_Cart()
        //{

        //    // Arrange - create the mock repository
        //    Mock<IProductRepository> mock = new Mock<IProductRepository>();
        //    mock.Setup(m => m.Products).Returns(new Product[] {
        //        new Product {ProductID = 1, Name = "P1", Category = "Apples"},
        //        new Product {ProductID = 2, Name = "P2", Category = "Oranges"},
        //    }.AsQueryable());

        //    // Arrange - create a Cart
        //    Cart cart = new Cart();

        //    // Arrange - create the controller
        //    CartController target = new CartController(mock.Object);

        //    // Act - add a product to the cart
        //    //public RedirectToRouteResult AddToCart(Cart cart, int productId, string returnUrl)
        //    target.AddToCart(cart, 1, null);
        //    target.AddToCart(cart, 2, null);

        //    // Assert
        //    Assert.AreEqual(cart.Lines.Count(), 2);
        //    Assert.AreEqual(cart.Lines.ToArray()[1].Product.ProductID, 2);
                 
        //    Trace.WriteLine(cart.Lines);
        //    //System.Collections.Generic.List`1[SuperStore.Domain.Entities.CartLine]

        //    Trace.WriteLine(cart.Lines.Count());
        //    //2

        //    Trace.WriteLine(cart.Lines.ToArray());
        //    //SuperStore.Domain.Entities.CartLine[]

        //    Trace.WriteLine(cart.Lines.ToArray()[1]);
        //    //SuperStore.Domain.Entities.CartLine

        //    Trace.WriteLine(cart.Lines.ToArray()[1].Product);
        //    //SuperStore.Domain.Entities.Product

        //    Trace.WriteLine(cart.Lines.ToArray()[1].Product.ProductID);
        //    //2
        //}

        //[TestMethod]
        //public void Adding_Product_To_Cart_Goes_To_Cart_Screen()
        //{
        //    // Arrange - create the mock repository
        //    Mock<IProductRepository> mock = new Mock<IProductRepository>();
        //    mock.Setup(m => m.Products).Returns(new Product[] {
        //        new Product {ProductID = 1, Name = "P1", Category = "Apples"},
        //    }.AsQueryable());

        //    // Arrange - create a Cart
        //    Cart cart = new Cart();

        //    // Arrange - create the controller
        //    CartController target = new CartController(mock.Object);

        //    // Act - add a product to the cart
        //    RedirectToRouteResult result = target.AddToCart(cart, 2, "myUrl");

        //    // Assert
        //    //return RedirectToAction("Index", new { returnUrl });
        //    //http:/localhost:/Cart/Index?returnUrl=myUrl
        //    Assert.AreEqual(result.RouteValues["action"], "Index");
        //    Assert.AreEqual(result.RouteValues["returnUrl"], "myUrl");

        //    Trace.WriteLine(result);
        //    //System.Web.Mvc.RedirectToRouteResult
        //}
        //[TestMethod]
        //public void Can_View_Cart_Contents()
        //{
        //    // Arrange - create a Cart
        //    Cart cart = new Cart();

        //    // Arrange - create the controller
        //    CartController target = new CartController(null);

        //    // Act - call the Index action method
        //   // public ViewResult Index(Cart cart, string returnUrl)
        //   //public class CartIndexViewModel{public Cart Cart { get; set; }public string ReturnUrl { get; set; }}
        //    CartIndexViewModel result = (CartIndexViewModel)target.Index(cart, "myUrl").ViewData.Model;

        //    // Assert
        //    Assert.AreSame(result.Cart, cart);
        //    Assert.AreEqual(result.ReturnUrl, "myUrl");

        //    Trace.WriteLine(result);
        //    //SuperStore.WebUI.Models.CartIndexViewModel
        //}

        [TestMethod]
        public void Cannot_Checkout_Empty_Cart()
        {

            // Arrange - create a mock order processor
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();
            // Arrange - create an empty cart
            Cart cart = new Cart();
            // Arrange - create shipping details
            ShippingDetails shippingDetails = new ShippingDetails();
            // Arrange - create an instance of the controller, IProductRepository repo is null
            //  public CartController(IProductRepository repo, IOrderProcessor proc)
            CartController target = new CartController(null, mock.Object);

            // Act
            //public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
            ViewResult result = target.Checkout(cart, shippingDetails);

            // Assert - check that the order hasn't been passed on to the processor
            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()),
                Times.Never());
            // Assert - check that the method is returning the default view
            Assert.AreEqual(string.Empty, result.ViewName);
            // Assert - check that I am passing an invalid model to the view
            Assert.AreEqual(false, result.ViewData.ModelState.IsValid);

            Trace.WriteLine(result.ViewName);
            //

            Trace.WriteLine(result.ViewData);
            //System.Web.Mvc.ViewDataDictionary

            Trace.WriteLine(result.ViewData.ModelState);
            //System.Web.Mvc.ModelStateDictionary
        }

        [TestMethod]
        public void Cannot_Checkout_Invalid_ShippingDetails()
        {

            // Arrange - create a mock order processor
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();
            // Arrange - create a cart with an item
            Cart cart = new Cart();
            // public void AddItem(Product product, int quantity)
            cart.AddItem(new Product(), 1);

            // Arrange - create an instance of the controller
            CartController target = new CartController(null, mock.Object);
            // Arrange - add an error to the model
           target.ModelState.AddModelError("error", "error msg to display");

            // Act - try to checkout
            ViewResult result = target.Checkout(cart, new ShippingDetails());

            // Assert - check that the order hasn't been passed on to the processor
            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()),
                Times.Never());
            // Assert - check that the method is returning the default view
            Assert.AreEqual("", result.ViewName);
            // Assert - check that I am passing an invalid model to the view
            Assert.AreEqual(false, result.ViewData.ModelState.IsValid);

            Trace.WriteLine(result.ViewData.ModelState["error"]);
            //System.Web.Mvc.ModelState

            Trace.WriteLine(result.ViewData.ModelState["error"].Errors[0]);
            //System.Web.Mvc.

            Trace.WriteLine(result.ViewData.ModelState["error"].Errors[0].ErrorMessage);
            //error msg to display
        }

        [TestMethod]
        public void Can_Checkout_And_Submit_Order()
        {
            // Arrange - create a mock order processor
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();
            // Arrange - create a cart with an item
            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);
            // Arrange - create an instance of the controller
            CartController target = new CartController(null, mock.Object);

            // Act - try to checkout
            ViewResult result = target.Checkout(cart, new ShippingDetails());

            // Assert - check that the order has been passed on to the processor
            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()),
                Times.Once());
            // Assert - check that the method is returning the Completed view
            //public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails) // if (ModelState.IsValid) return View("Completed");
            Assert.AreEqual("Completed", result.ViewName);
            // Assert - check that I am passing a valid model to the view
            Assert.AreEqual(true, result.ViewData.ModelState.IsValid);

            Trace.WriteLine(result.ViewName);
        }
    }
}
