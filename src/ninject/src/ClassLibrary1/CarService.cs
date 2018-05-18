using System;

namespace ClassLibrary1
{
    public class CarService : ICarService
    {
        public string Name => throw new NotImplementedException();

        public string Run()
        {
            return this.GetType().FullName;
        }
    }
}
