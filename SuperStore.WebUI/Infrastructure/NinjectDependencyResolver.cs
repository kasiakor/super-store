using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using Ninject;
using SuperStore.Domain.Abstract;
using SuperStore.Domain.Concrete;

namespace SuperStore.WebUI.Infrastructure
{

    public class NinjectDependencyResolver : IDependencyResolver
    {
        //kernel is an object resp for resolving dependencies and creating new objects
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            // put bindings here
            //Mock<IProductRepository> mock = new Mock<IProductRepository>();
            //mock.Setup(m => m.Products).Returns(new List<Product> {
            //new Product { Name = "Milk", Price = 2 },
            //new Product { Name = "Cocoa", Price = 7 },
            //new Product { Name = "Sugar", Price = 3 }
            //});

            //kernel.Bind<IProductRepository>().ToConstant(mock.Object);

            //use real repository, add binding
            //It tells Ninject to create instances of the EFProductRepository class to service requests for the IProductRepository interface
            kernel.Bind<IProductRepository>().To<EFProductRepository>();

            //instance of this class is demanded by the EmailOrderProcessor constructor
            EmailSettings emailSettings = new EmailSettings
            {
                WriteAsFile = bool.Parse(ConfigurationManager
                   .AppSettings["Email.WriteAsFile"] ?? "false")
            };

            kernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>()
                .WithConstructorArgument("settings", emailSettings);
        }
    }
}