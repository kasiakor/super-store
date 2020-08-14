using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Moq;
using Ninject;
using SuperStore.Domain.Abstract;
using SuperStore.Domain.Entities;

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
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product> {
            new Product { Name = "Milk", Price = 2 },
            new Product { Name = "Cocoa", Price = 7 },
            new Product { Name = "Sugar", Price = 3 }
            });

            kernel.Bind<IProductRepository>().ToConstant(mock.Object);
        }
    }
}