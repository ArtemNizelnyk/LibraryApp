using LibraryApp.BLL.DTO;
using LibraryApp.BLL.Infrastructure;
using LibraryApp.BLL.Interfaces;
using LibraryApp.BLL.Services;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryApp.UI.DI
{
   
    //Класс для подключения зависимостей между сервисами
        public class NinjectDependencyResolver : IDependencyResolver
        {
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
                kernel.Bind<IOrderService>().To<OrderService>();
                kernel.Bind<IService<UserDTO>>().To<UserService>();
                kernel.Bind<IService<BookDTO>>().To<BookService>();
                kernel.Bind<IService<AuthorDTO>>().To<AuthorService>();
            }
        }
    }
