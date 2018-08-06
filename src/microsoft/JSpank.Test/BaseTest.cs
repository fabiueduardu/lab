using JSpank.Test.Helpers.Providers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.SessionState;

namespace JSpank.Test
{
    public class BaseTest
    {
        string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["Db"].ConnectionString;

            }
        }

        protected string Key5
        {
            get
            {
                return this.Key(05);
            }
        }

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

        protected string ClearStringWithRegex(string value)
        {
            return Regex.Replace(value, "[^0-9a-zA-Z]+", "");
        }

        protected void OnDb(string[] queries)
        {
            using (var conn = new SqlConnection(this.ConnectionString))
            {
                conn.Open();
                foreach (var query in queries)
                    using (var command = new SqlCommand(query, conn))
                        command.ExecuteNonQuery();
            }
        }

        protected IEnumerable<object> OnDbScalar(string[] queries)
        {
            using (var conn = new SqlConnection(this.ConnectionString))
            {
                conn.Open();

                foreach (var query in queries)
                    using (var command = new SqlCommand(query, conn))
                        yield return command.ExecuteScalar();
            }
        }

        protected IEnumerable<IDictionary<string, object>> OnReader(string query)
        {
            using (var conn = new SqlConnection(this.ConnectionString))
            {
                conn.Open();
                IDictionary<string, object> columns = null;
                using (var command = new SqlCommand(query, conn))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        columns = new Dictionary<string, object>();
                        for (var i = 0; i < reader.FieldCount; i++)
                            columns.Add(reader.GetName(i), reader[i]);

                        yield return columns;
                    }
                }

                conn.Close();
            }
        }

        protected void OnWebContext(string login = null)
        {
            HttpContext httpContext = null;
            this.OnWebContext(login, ref httpContext);
        }

        protected void OnWebContext(string login, ref HttpContext httpContext)
        {
            // We need to setup the Current HTTP Context as follows:            

            // Step 1: Setup the HTTP Request
            var httpRequest = new HttpRequest("", "https://www.google.com.br/", "");

            // Step 2: Setup the HTTP Response
            var httpResponce = new HttpResponse(new StringWriter());

            // Step 3: Setup the Http Context
            httpContext = new HttpContext(httpRequest, httpResponce);
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
            if (!string.IsNullOrEmpty(login))
                httpContext.User = new IPrincipalModel(login);

            HttpContext.Current = httpContext;
        }

        [TestInitialize]
        public void TestInitialize()
        {
            //Change data directory if necessary
            //  AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Resources"));
        }
    }
}
