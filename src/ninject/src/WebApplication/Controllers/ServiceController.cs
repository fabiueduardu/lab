using Ninject;
using Ninject.Extensions.Conventions;
using System;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    public class ServiceController : Controller
    {
        public ActionResult ListAssemblies()
        {
            var content = string.Concat("List: ", Environment.NewLine);

            var ninjectKernel = new StandardKernel();

            ninjectKernel.Scan(scanner =>
            {
                scanner.FromAssembliesInPath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                scanner.BindWithDefaultConventions();
                scanner.InSingletonScope();
            });

            return Content(content);
        }
    }
}