using System;
using System.Web;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;
using Store.UnitTests;

[assembly: WebActivator.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(NinjectWebCommon), "Stop")]

namespace Store.UnitTests
{
  public static class NinjectWebCommon
  {
    private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

    public static void Start()
    {
      DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
      DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
      Bootstrapper.Initialize(CreateKernel);
    }

    public static void Stop() => Bootstrapper.ShutDown();

    private static IKernel CreateKernel()
    {
      var kernel = new StandardKernel();
      kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
      kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

      RegisterServices(kernel);
      return kernel;
    }

    private static void RegisterServices(IKernel kernel) { }
  }
}