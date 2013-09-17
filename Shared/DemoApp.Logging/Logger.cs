using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Logging;

namespace DemoApp.Logging
{
    public class Logger:ILoggerFacade
    {
        private string logFile = "log.txt";

        public void Log(string message, Category category, Priority priority)
        {
            System.IO.File.AppendAllLines(logFile, new[] { DateTime.Now.ToString() + "=>" + message });
        }
    }
}
