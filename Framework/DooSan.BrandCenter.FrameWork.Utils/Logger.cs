using DooSan.BrandCenter.FrameWork.Utils;
using System;
using System.Diagnostics;
using System.Text;

namespace DooSan.BrandCenter.FrameWork.Utils
{
    public class Logger : ILogger
    {
        public void Information(string message)
        {
            new AppException(string.Format(message));
            //Trace.TraceInformation(message);
        }

        public void Information(string fmt, params object[] vars)
        {
            new AppException(string.Format(fmt, vars));
            //Trace.TraceInformation(fmt, vars);
        }

        public void Information(Exception exception, string fmt, params object[] vars)
        {
            new AppException(string.Format(fmt, vars), exception);
            //Trace.TraceInformation(FormatExceptionMessage(exception, fmt, vars));
        }

        public void Warning(string message)
        {
            new AppException(message);
            //Trace.TraceWarning(message);
        }

        public void Warning(string fmt, params object[] vars)
        {
            new AppException(string.Format(fmt, vars));
            //Trace.TraceWarning(fmt, vars);
        }

        public void Warning(Exception exception, string fmt, params object[] vars)
        {
            new AppException(string.Format(fmt, vars), exception);
            //Trace.TraceWarning(FormatExceptionMessage(exception, fmt, vars));
        }

        public void Error(string message)
        {
            new AppException(message);
            //Trace.TraceError(message);
        }

        public void Error(string fmt, params object[] vars)
        {
            new AppException(string.Format(fmt, vars));
            //Trace.TraceError(fmt, vars);
        }

        public void Error(Exception exception, string fmt, params object[] vars)
        {
            new AppException(string.Format(fmt, vars), exception);
            //Trace.TraceError(FormatExceptionMessage(exception, fmt, vars));
        }

        public void TraceApi(string componentName, string method, TimeSpan timespan)
        {
            new AppException(string.Format("{0}.{1}", componentName, method));
            //TraceApi(componentName, method, timespan, "");
        }

        public void TraceApi(string componentName, string method, TimeSpan timespan, string fmt, params object[] vars)
        {
            string message = String.Concat("Component:", componentName, ";Method:", method, ";Timespan:", timespan.ToString(), ";params:", SQLTraceClass.Params2String(vars));
            SQLTraceClass.SQLWriteLog(message);

            //new AppException(string.Format("{0}.{1} TimeSpan: {2}", componentName, method, timespan.ToString()),  );
            //TraceApi(componentName, method, timespan, string.Format(fmt, vars));
        }
        public void TraceApi(string componentName, string method, TimeSpan timespan, string properties)
        {
            string message = String.Concat("Component:", componentName, ";Method:", method, ";Timespan:", timespan.ToString(), ";Properties:", properties);
            //Trace.TraceInformation(message);
            //AppException.SQLWriteLog(message);
            SQLTraceClass.SQLWriteLog(message);
        }

        private static string FormatExceptionMessage(Exception exception, string fmt, object[] vars)
        {
            var sb = new StringBuilder();
            sb.Append(string.Format(fmt, vars));
            sb.Append(" Exception: ");
            sb.Append(exception.ToString());
            return sb.ToString();
        }
    }

    public interface ILogger
    {
        void Information(string message);
        void Information(string fmt, params object[] vars);
        void Information(Exception exception, string fmt, params object[] vars);

        void Warning(string message);
        void Warning(string fmt, params object[] vars);
        void Warning(Exception exception, string fmt, params object[] vars);

        void Error(string message);
        void Error(string fmt, params object[] vars);
        void Error(Exception exception, string fmt, params object[] vars);

        void TraceApi(string componentName, string method, TimeSpan timespan);
        void TraceApi(string componentName, string method, TimeSpan timespan, string properties);
        void TraceApi(string componentName, string method, TimeSpan timespan, string fmt, params object[] vars);

    }

}