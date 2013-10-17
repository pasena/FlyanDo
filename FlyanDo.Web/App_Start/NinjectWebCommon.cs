using System.Collections.Generic;

[assembly: WebActivator.PreApplicationStartMethod(typeof(FlyanDo.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(FlyanDo.Web.App_Start.NinjectWebCommon), "Stop")]

namespace FlyanDo.Web.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    using Repository;
    using Repository.Abstract;
    using Service;
    using Service.Abstract;
    using Entity;

    using Moq;
    using System.Linq;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            Bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            Bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            MockebleBinds(kernel);
        }

        private static void MockebleBinds(IKernel kernel)
        {
            kernel.Bind<IFlyService>().To<FlyService>();
            kernel.Bind<IFlyRepository>().ToConstant(CreateFlyRepository());
        }

        private static IFlyRepository CreateFlyRepository()
        {
            var flyRepo = new Mock<IFlyRepository>();

            var flys = new List<Fly>
                {
                    new Fly{Id = 1, Description = "First Fly", DateOfFly = DateTime.Now, Owner = new FlyOwner{Id = 1, Name = "Paulo" } },
                    new Fly{Id = 2, Description = "Second Fly", DateOfFly = DateTime.Now, Owner = new FlyOwner{Id = 1, Name = "Paulo" } },
                    new Fly{Id = 3, Description = "Third Fly", DateOfFly = DateTime.Now, Owner = new FlyOwner{Id = 1, Name = "Paulo" } }
                };

            flyRepo.Setup(s => s.GetAll()).Returns(flys.AsQueryable());

            return flyRepo.Object;
        }
    }
}
