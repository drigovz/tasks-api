using System;
using System.IO;
using Microsoft.Extensions.Logging;

namespace TasksApi.Logging
{
    public class CustomLogger : ILogger
    {
        private readonly string loggerName;
        private readonly CustomLoggerProviderConfiguration loggerConfig;

        public CustomLogger(string name, CustomLoggerProviderConfiguration config)
        {
            loggerName = name;
            loggerConfig = config;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel == loggerConfig.LogLevel;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            string message = $"{logLevel.ToString()}: {eventId.Id} - {formatter(state, exception)}";
            WriteInFile(message);
        }

        protected void WriteInFile(string message)
        {
            string url = @"C:\dev\CoreProjects\Core\TasksApi\logs\log.txt";
            using (StreamWriter sw = new StreamWriter(url, true))
            {  
                try
                {
                    sw.WriteLine(message);
                    sw.Close();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}