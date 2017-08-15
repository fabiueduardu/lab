using System;
using System.Linq;
using System.Configuration;
using System.Web.Configuration;
using System.Web.UI.Design;

namespace HelpMe.Service
{
    public static class StartService
    {
        public static void Run()
        {
            LogService.Write(DateTime.Now.Second.ToString());
            RegisterHttpHandler();
        }

        static void RegisterHttpHandler()
        {
            
            var configuration = WebConfigurationManager.OpenWebConfiguration("/Util");
            var section = (HttpHandlersSection)configuration.GetSection("system.webserver/handlers");
            if (section == null)
            {
                var group = configuration.GetSectionGroup("system.webserver");
                if (group == null)
                {
                    configuration.SectionGroups.Add("system.webserver", new ConfigurationSectionGroup());
                    group = configuration.GetSectionGroup("system.webserver");
                }

                group.Sections.Add("handlers", new HttpHandlersSection());
                section = (HttpHandlersSection)configuration.GetSection("system.webserver/handlers");
            }

            var handler = new HttpHandlerAction("HelpMe", "HelpMe.Handler.QueryHandler, HelpMe", "GET", true);

            section.Handlers.Remove(handler);
            section.Handlers.Add(handler);
            configuration.Save(ConfigurationSaveMode.Minimal);
        }
    }
}
