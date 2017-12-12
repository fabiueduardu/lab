using System;

namespace HelloWorldNetStandard
{
    public class HelloWorldService
    {
        public void Hello(string value)
        {

            Console.WriteLine("Hello: {0}  ", value);
            Console.WriteLine(new ClassLibrary_Reference.Service().FullName);
            Console.WriteLine(new ClassLibrary_ReferencePackage.Service().FullName);
        }
    }
}
