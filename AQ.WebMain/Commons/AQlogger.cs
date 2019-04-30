using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using NLog;
using ILogger = Microsoft.Extensions.Logging.ILogger;
using LogLevel = NLog.LogLevel;

namespace AQ.WebMain
{
    public class NLogLogger : ILogger
    {
        private string _category;
        Logger _nLogger;
        public NLogLogger(string category, Logger nLogger)
        {
            _category = category;
            _nLogger = nLogger;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
            //throw new NotImplementedException();
        }

        public bool IsEnabled(Microsoft.Extensions.Logging.LogLevel logLevel)
        {
            return _nLogger.IsEnabled(ConvertLogLevel(logLevel));
            //throw new NotImplementedException();
        }

        public void Log<TState>(Microsoft.Extensions.Logging.LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            var nLogLevel = ConvertLogLevel(logLevel);
            if (_nLogger.IsEnabled(nLogLevel))
            {
                var message = string.Empty;
                if (formatter != null)
                {
                    message = formatter(state, exception);
                    var nLogEventInfo = LogEventInfo.Create(nLogLevel, eventId.Name, message);
                    nLogEventInfo.Exception = exception;

                    //nLogEventInfo.Parameters = "";
                    //nLogEventInfo.Parameters["EventId.Name"] = eventId.Name;
                    //nLogEventInfo.Parameters["EventId"] = eventId;

                    _nLogger.Log(nLogEventInfo);
                }
            }
            //throw new NotImplementedException();
        }

        private static LogLevel ConvertLogLevel(Microsoft.Extensions.Logging.LogLevel logLevel)
        {
            switch (logLevel)
            {
                case Microsoft.Extensions.Logging.LogLevel.Trace:
                    return LogLevel.Trace;
                case Microsoft.Extensions.Logging.LogLevel.Debug:
                    return LogLevel.Debug;
                case Microsoft.Extensions.Logging.LogLevel.Information:
                    return LogLevel.Info;
                case Microsoft.Extensions.Logging.LogLevel.Warning:
                    return LogLevel.Warn;
                case Microsoft.Extensions.Logging.LogLevel.Error:
                    return LogLevel.Error;
                case Microsoft.Extensions.Logging.LogLevel.Critical:
                    return LogLevel.Fatal;
                case Microsoft.Extensions.Logging.LogLevel.None:
                    return LogLevel.Off;
                default:
                    return LogLevel.Debug;
            }
        }
    }


    public class NLogLoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            Console.WriteLine(categoryName);
            return new NLogLogger(categoryName, LogManager.GetLogger(categoryName));
            //throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
