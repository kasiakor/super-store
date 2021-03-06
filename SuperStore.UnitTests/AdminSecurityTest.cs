﻿using System;
using System.Diagnostics;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SuperStore.WebUI.Controllers;
using SuperStore.WebUI.Infrastructure.Abstract;
using SuperStore.WebUI.Models;

namespace SuperStore.UnitTests
{
    [TestClass]
    public class AdminSecurityTest
    {
        [TestMethod]
        public void Can_Login_With_Valid_Credentials()
        {
            // Arrange - create the mock authentication provider
            Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
            mock.Setup(m => m.Authenticate("admin", "secret")).Returns(true);

            // Arrange - create view model object
            LoginViewModel model = new LoginViewModel
            {
                UserName = "admin",
                Password = "secret"
            };

            // Arrange - create the controller
            AccountController target = new AccountController(mock.Object);

            //Act - authenticate user
            //public ActionResult Login(LoginViewModel model, string returnUrl)
            ActionResult result = target.Login(model, "/MyURL");

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectResult));
            Assert.AreEqual("/MyURL", ((RedirectResult)result).Url);

            Trace.WriteLine(result);
            //System.Web.Mvc.RedirectResult

        }

        [TestMethod]
        public void Can_Login_With_Invalid_Credentials()
        {
            // Arrange - create the mock authentication provider
            Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
            mock.Setup(m => m.Authenticate("badUser", "badPass")).Returns(false);

            // Arrange - create view model object
            LoginViewModel model = new LoginViewModel
            {
                UserName = "badUser",
                Password = "badPass"
            };

            // Arrange - create the controller
            AccountController target = new AccountController(mock.Object);

            //Act - authenticate user
            //public ActionResult Login(LoginViewModel model, string returnUrl)
            ActionResult result = target.Login(model, "/MyURL");

            // Assert
            //ModelState.AddModelError("error", "Incorrect username or password"); return View();
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsFalse(((ViewResult)result).ViewData.ModelState.IsValid);

            Trace.WriteLine(result);
            //System.Web.Mvc.ViewResult

            Trace.WriteLine(target.ModelState[" "].Errors[0].ErrorMessage);
            //Incorrect username or password
        }
    }
}
