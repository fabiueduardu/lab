using Ninject.Modules;

namespace ClassLibrary1
{
    public class InterfaceMappings : NinjectModule
    {
        public override void Load()
        {
             Bind<ICarService>().To<CarService>().InSingletonScope();
        }
    }
}
