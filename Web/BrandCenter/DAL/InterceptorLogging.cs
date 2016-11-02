using System;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.SqlServer;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection;
using System.Linq;
using DooSan.BrandCenter.FrameWork.Utils;

namespace BrandCenter.DAL
{
    public class InterceptorLogging : DbCommandInterceptor
    {
        //private ILogger _logger = new Logger();
        private readonly Stopwatch _stopwatch = new Stopwatch();

        public override void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            SQLTraceClass.debugSQLTrace(command, true);

            base.ScalarExecuting(command, interceptionContext);
            _stopwatch.Restart();
        }

        public override void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            _stopwatch.Stop();
            if (interceptionContext.Exception != null)
            {
                new AppException(string.Format("Error executing command: {0}", command.CommandText), interceptionContext.Exception);
                //_logger.Error(interceptionContext.Exception, "Error executing command: {0}", command.CommandText);
            }
            else
            {
                SQLTraceClass.SQLTrace(command, true, _stopwatch.Elapsed.Ticks);
                //_logger.TraceApi("SQL Database", "InterceptorLogging.ScalarExecuted", _stopwatch.Elapsed, "Command: {0}: ", command.CommandText);
            }
            base.ScalarExecuted(command, interceptionContext);
        }

        public override void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            SQLTraceClass.debugSQLTrace(command, true);

            base.NonQueryExecuting(command, interceptionContext);
            _stopwatch.Restart();
        }

        public override void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            _stopwatch.Stop();
            if (interceptionContext.Exception != null)
            {
                new AppException(string.Format("Error executing command: {0}", command.CommandText), interceptionContext.Exception);
                //_logger.Error(interceptionContext.Exception, "Error executing command: {0}", command.CommandText);
            }
            else
            {
                SQLTraceClass.SQLTrace(command, true, _stopwatch.Elapsed.Ticks);
                //_logger.TraceApi("SQL Database", "InterceptorLogging.NonQueryExecuted", _stopwatch.Elapsed, "Command: {0}: ", command.CommandText);
            }
            base.NonQueryExecuted(command, interceptionContext);
        }

        public override void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            SQLTraceClass.debugSQLTrace(command, true);

            base.ReaderExecuting(command, interceptionContext);
            _stopwatch.Restart();
        }
        public override void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {

            _stopwatch.Stop();
            if (interceptionContext.Exception != null)
            {
                new AppException(string.Format("Error executing command: {0}", command.CommandText), interceptionContext.Exception);
                //_logger.Error(interceptionContext.Exception, "Error executing command: {0}", command.CommandText);
            }
            else
            {
                SQLTraceClass.SQLTrace(command, true, _stopwatch.Elapsed.Ticks);
                //_logger.TraceApi("SQL Database", "InterceptorLogging.ReaderExecuted", _stopwatch.Elapsed, "Command: {0}: ", command.CommandText);
            }
            base.ReaderExecuted(command, interceptionContext);
        }
    }
}