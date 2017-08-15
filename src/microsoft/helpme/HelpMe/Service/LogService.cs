using System;
using System.Configuration;
using System.IO;

namespace HelpMe.Service
{
    public static class LogService
    {
        static string LogPath
        {
            get
            {
                return ConfigurationManager.AppSettings["MyQuery.LogPath"] as string;
            }
        }

        public static void Write(string contents)
        {
            File.WriteAllText(string.Concat(LogPath, DateTime.Now.ToString("dd_MM_yyyy"), ".log"), contents);
        }
    }
}
