using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLog;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace AQ.WebMain.Commons
{
    public class AQNLogLogger : ILogger
    {

        NLog.Logger _logger;
        public AQNLogLogger(NLog.Logger logger)
        {
            _logger = logger;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            //throw new NotImplementedException();
            return null;
        }

        public bool IsEnabled(Microsoft.Extensions.Logging.LogLevel logLevel)
        {
            return _logger.IsEnabled(ConvertLogLevel(logLevel));
            //throw new NotImplementedException();
        }

        public void Log<TState>(Microsoft.Extensions.Logging.LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            var nLogLoggerLevel = ConvertLogLevel(logLevel);
            if (_logger.IsEnabled(nLogLoggerLevel))
            {
                var message = "";
                if (formatter != null)
                {
                    message = formatter(state, exception);
                }
                var logEventInfo = new LogEventInfo(nLogLoggerLevel, "loggerName", message);
                //_logger.Log(logEventInfo);
                _logger.Log(typeof(Microsoft.Extensions.Logging.ILogger), logEventInfo);
            }
            //throw new NotImplementedException();
        }

        private static NLog.LogLevel ConvertLogLevel(Microsoft.Extensions.Logging.LogLevel logLevel)
        {
            switch (logLevel)
            {
                case Microsoft.Extensions.Logging.LogLevel.Trace:
                    return NLog.LogLevel.Trace;
                case Microsoft.Extensions.Logging.LogLevel.Debug:
                    return NLog.LogLevel.Debug;
                case Microsoft.Extensions.Logging.LogLevel.Information:
                    return NLog.LogLevel.Info;
                case Microsoft.Extensions.Logging.LogLevel.Warning:
                    return NLog.LogLevel.Warn;
                case Microsoft.Extensions.Logging.LogLevel.Error:
                    return NLog.LogLevel.Error;
                case Microsoft.Extensions.Logging.LogLevel.Critical:
                    return NLog.LogLevel.Fatal;
                case Microsoft.Extensions.Logging.LogLevel.None:
                    return NLog.LogLevel.Off;
                default:
                    return NLog.LogLevel.Debug;
            }
        }
    }

    public class NLogLoggerPrivoder : ILoggerProvider
    {

        public LogFactory _logFactory { get; }
        public NLogLoggerPrivoder()
        {
            _logFactory = LogManager.LogFactory;
        }

        public ILogger CreateLogger(string categoryName)
        {
            //return new AQNLogLogger(LogManager.GetLogger(categoryName));
            return new AQNLogLogger(_logFactory.GetLogger(categoryName));
            //throw new NotImplementedException();
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
