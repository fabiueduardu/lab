using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;
using System.IO;
using System.Web.SessionState;
using System.Reflection;

namespace HelpMe.Test
{
    [TestClass]
    public class BaseTest
    {
        protected string KeyAll
        {
            get
            {
                return Guid.NewGuid().ToString();
            }
        }

        protected string Key(int length = 5)
        {
            return KeyAll.Substring(0, length);
        }

        [TestInitialize]
        public void _TestInitialize()
        {
            this.OnWebContext();
            Console.WriteLine("_WebTestTestInitialize");
        }

        protected void OnWebContext()
        {
            // We need to setup the Current HTTP Context as follows:            

            // Step 1: Setup the HTTP Request
            var httpRequest = new HttpRequest("", "http://localhost/util/sql-bccown.aspx", "");

            // Step 2: Setup the HTTP Response
            var httpResponce = new HttpResponse(new StringWriter());

            // Step 3: Setup the Http Context
            var httpContext = new HttpContext(httpRequest, httpResponce);
            var sessionContainer =
                new HttpSessionStateContainer(this.Key(),
                                               new SessionStateItemCollection(),
                                               new HttpStaticObjectsCollection(),
                                               10,
                                               true,
                                               HttpCookieMode.AutoDetect,
                                               SessionStateMode.InProc,
                                               false);
            httpContext.Items["AspSession"] =
                typeof(HttpSessionState)
                .GetConstructor(
                                    BindingFlags.NonPublic | BindingFlags.Instance,
                                    null,
                                    CallingConventions.Standard,
                                    new[] { typeof(HttpSessionStateContainer) },
                                    null)
                .Invoke(new object[] { sessionContainer });

            // Step 4: Assign the Context
            HttpContext.Current = httpContext;
        }
    }
}
