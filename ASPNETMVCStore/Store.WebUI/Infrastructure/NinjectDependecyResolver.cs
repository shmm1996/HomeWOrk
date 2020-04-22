using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using Ninject;
using Store.Domain.Abstract;
using Store.Domain.Concrete;
using Store.WebUI.Infrastructure.Abstract;
using Store.WebUI.Infrastructure.Concrete;

namespace Store.WebUI.Infrastructure
{
  public class NinjectDependencyResolver : IDependencyResolver
  {
    private readonly IKernel _kernel;

    public NinjectDependencyResolver(IKernel kernelParam)
    {
      _kernel = kernelParam;
      AddBindings();
    }

    public object GetService(Type serviceType) => _kernel.TryGet(serviceType);
    public IEnumerable<object> GetServices(Type serviceType) => _kernel.GetAll(serviceType);

    private void AddBindings()
    {
      _kernel.Bind<IProductsRepository>().To<EFProductsRepository>();
      var emailSettings = new EmailSettings
      {
        WriteAsFile = bool.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false")
      };

      _kernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>().WithConstructorArgument("settings", emailSettings);
      _kernel.Bind<IAuthProvider>().To<FormAuthProvider>();
    }
  }
}