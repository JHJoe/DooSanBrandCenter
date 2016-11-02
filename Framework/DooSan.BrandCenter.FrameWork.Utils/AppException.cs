using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Configuration;
using System.Reflection;

namespace DooSan.BrandCenter.FrameWork.Utils
{
    public enum LayerTypes { Global, Presentation, Business, Data };
    public class AppException : System.ApplicationException
    {
        #region 전역변수

        private string _strPageMessage = "";
        private string _strTime = "";
        const string _strEXCver = "0.1"; //테스트용 version
        private string _strBizName = "";
        private string _strAppDomainName = "";
        private LayerTypes enLayerType;
        private EventLogEntryType enErrorType;

        #endregion 전역변수

        #region 생성자

        public AppException()
            : base()
        {
            WriteLog("예상하지 못한 에러입니다.");
        }

        public AppException(string message)
            : base(message)
        {
            this.enLayerType = LayerTypes.Presentation;

            WriteLog(message);
        }

        public AppException(string message, string pageErrMessage)
            : base(message)
        {
            this.enLayerType = LayerTypes.Presentation;
            this._strPageMessage = pageErrMessage;

            WriteLog(message);
        }

        public AppException(string message, bool Logbool)
            : base(message)
        {
            if (Logbool)
                WriteLog(message);
        }
        //public AppException(string message, string source)
        //    : base(message)
        //{
        //    WriteLog(source + "\r\n" + message);
        //}


        public AppException(string message, System.Exception innerException)
            : base(message, innerException)
        {

            this.enLayerType = LayerTypes.Presentation;

            if (innerException.InnerException != null)
            {
                innerException = innerException.InnerException;
            }

            //			this.enErrorType = EventLogEntryType.Information;

            if (innerException != null)
            {
                LogEvent(MakeLogMsg(message, ref innerException));
                WriteLog(MakeLogMsg(message, ref innerException));
            }
        }
        public AppException(LayerTypes LayerTypes, string message, System.Exception innerException)
            : base(message, innerException)
        {
            this.enLayerType = LayerTypes;

            if (innerException.InnerException != null)
            {
                innerException = innerException.InnerException;
            }

            //			this.enErrorType = EventLogEntryType.Information;

            if (innerException != null)
            {
                LogEvent(MakeLogMsg(message, ref innerException));
                WriteLog(MakeLogMsg(message, ref innerException));
            }
        }



        //global 에러에 들어가는 가능한 에러는 throw를 하지 않는걸 원칙으로 한다.

        public AppException(LayerTypes LayerTypes, string BizName, EventLogEntryType ErrorType, string message, ref System.Exception innerException)
            : base(message, innerException)
        {
            this.enLayerType = LayerTypes;
            this._strBizName = BizName;
            this.enErrorType = ErrorType;
            this._strAppDomainName = AppDomain.CurrentDomain.FriendlyName;


            //			if ( innerException.InnerException == null )
            //				innerException = innerException.InnerException;

            if (innerException != null)
                LogEvent(MakeLogMsg(message, ref innerException), LayerTypes, BizName, ErrorType);
        }




        /*
                public AppException(LayerTypes LayerTypes, string BizName,EventLogEntryType ErrorType, string message, ref Exception innerException) 
                {

                    this.enLayerType = LayerTypes;
                    this._strBizName = BizName;
                    this.enErrorType = ErrorType;

                    if(innerException != null)
                        LogEvent( MakeLogMsg(message, ref innerException), LayerTypes, BizName, ErrorType );
			
                }
		
                public AppException(string message, ref Exception innerException) 
                {

        //			this.enErrorType = EventLogEntryType.Information;

                    if(innerException != null)
                        LogEvent( MakeLogMsg(message, ref innerException) );
			
                }

        */

        #endregion 생성자

        #region 속성



        public string PageErrMessage
        {

            get
            {
                return _strPageMessage;
            }

        }

        public string PageErrTime
        {
            get
            {
                return _strTime;
            }
        }

        public EventLogEntryType ErrorType
        {
            get
            {
                return enErrorType;
            }
        }
        public string BizName
        {
            get
            {
                return _strBizName;
            }
        }
        public LayerTypes LayerName
        {
            get
            {
                return enLayerType;
            }

        }

        #endregion 속성

        #region 메소드

        public static string GetMethodName()
        {
            StackFrame stackFrame = new StackFrame(1, false);
            string msg = string.Empty;
            MethodBase methodBase = stackFrame.GetMethod();

            msg += methodBase.DeclaringType.FullName + "->";
            msg += methodBase.Name + "()";

            return msg;
        }


        #endregion


        #region 로그처리


        private string MakeLogMsg(string message, ref System.Exception innerException)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(DateTime.Now.ToString());
            sb.Append(Environment.NewLine);
            sb.Append("---------------------------------------------------------");
            sb.Append(Environment.NewLine);

            //				sb.Append(innerException.ToString());

            sb.Append("\r\n[구  분] ");
            sb.Append("\r\nLayer: ").Append(enLayerType);

            if (_strBizName != "")
                sb.Append("\r\nBisiness:").Append(_strBizName);

            sb.Append("\r\n ");

            if (innerException.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException sqlErr = (System.Data.SqlClient.SqlException)innerException;
                //                System.Data.OracleClient.OracleException sqlErr = (System.Data.OracleClient.OracleException)innerException;
                sb.Append("\r\n[SqlException] ");

                if (message != "")
                    sb.Append("\r\nUserMessage: ").Append(message);

                sb.Append("\r\n" + "ErrorMsg: " + sqlErr.Message);

                sb.Append("\r\nException Type: ").Append(sqlErr.GetType());
                //					sb.Append("\r\nErrors: ").Append(sqlErr.Errors);

                //					sb.Append("\r\nServer: ").Append(sqlErr.Server);
                //					sb.Append("\r\nState: ").Append(sqlErr.State);
                sb.Append("\r\nSource: ").Append(sqlErr.Source);
                sb.Append("\r\nTargetSite: ").Append(sqlErr.TargetSite);
                //					sb.Append("\r\nHelpLink: ").Append(sqlErr.HelpLink);

                sb.Append("\r\nSourceLine: ").Append(sqlErr.ToString());

                if (message.Equals(string.Empty))
                    this._strPageMessage = sqlErr.Message;
                else
                    this._strPageMessage = message;

            }

            //     파일 타입 방식
            //			else if (innerException.GetType() == typeof(IOException) )	{		}



            else
            {
                sb.Append("\r\n[" + innerException.GetType() + "]");

                if (message != "")
                    sb.Append("\r\nUserMessage: ").Append(message);

                sb.Append("\r\n" + "ErrorMsg: " + innerException.Message);
                sb.Append("\r\nException Type: ").Append(innerException.GetType());
                sb.Append("\r\nSource: ").Append(innerException.Source);
                sb.Append("\r\nTargetSite: ").Append(innerException.TargetSite);

                sb.Append("\r\nSourceLine: ").Append(innerException.ToString());
                //					sb.Append("\r\nSourceLine: ").Append(innerException.Source);

                //					sb.Append("\r\n" + "DetailMsg: " + innerException.Message);

                if (message.Equals(string.Empty))
                    this._strPageMessage = innerException.Message;
                else
                    this._strPageMessage = message;

            }

            sb.Append(Environment.NewLine);
            sb.Append("---------------------------------------------------------");
            sb.Append(Environment.NewLine);

            this._strTime = DateTime.Now.ToString();
            return sb.ToString();

        }


        /*
                public override string ToString()
                {

                    return ".LCException " + 
                        _strEXCver	+ " : |" + 
                        _strBizName		+ "|" +
                        strLayerName	+ "|";
                }
        */





        //Unhandled Exception Log 처리 : 실파일 방식 
        private void WriteLog(string message)
        {

            try
            {

                if (ConfigurationManager.AppSettings["ErrorTraceMode"] != null)
                {
                    string TraceMode = ConfigurationManager.AppSettings["ErrorTraceMode"];
                    string TracePath = "c:\\";
                    string sFileName = string.Format("{0}_ServerLog_{1}.log", System.Net.Dns.GetHostName().ToLower(),
                        //				string sFileName = string.Format("{0}_AppTrace_{1}.log", System.Net.Dns.GetHostName().ToLower(), 
                        System.DateTime.Now.ToString("yyyyMMdd").Replace("-", ""));

                    if (ConfigurationManager.AppSettings["ErrorLogPath"] != null)
                        TracePath = ConfigurationManager.AppSettings["ErrorLogPath"];
                    if (TraceMode.Equals("on"))
                    {

                        System.Text.StringBuilder oBuilder = new System.Text.StringBuilder();
                        //                        oBuilder.Append(System.DateTime.Now.ToString());
                        //                        oBuilder.Append("\r\n");
                        oBuilder.Append(message);
                        //                        oBuilder.Append("\r\n-------------------------------------------------------------");

                        LogToFile(TracePath, sFileName, oBuilder.ToString());
                    }
                }

            }
            catch
            {
            }
        }
        

        private static void LogToFile(string logFolder, string fileName, string contents)
        {
            if (!System.IO.Directory.Exists(logFolder))
                System.IO.Directory.CreateDirectory(logFolder);

            System.IO.FileStream oStream = new System.IO.FileStream(System.String.Concat(logFolder, "\\", fileName),
                System.IO.FileMode.Append, System.IO.FileAccess.Write, System.IO.FileShare.ReadWrite);

            System.IO.StreamWriter oWriter = new System.IO.StreamWriter(oStream);
            oWriter.WriteLine(contents);

            oWriter.Flush();
            oWriter.Close();
        }


        //handled Exception Log처리 :윈도우 이벤트방식
        private static void LogEvent(string sMessage, LayerTypes LayerTypes, string _strBizName, EventLogEntryType ErrorType)
        {
            try
            {
                string sSource = LayerTypes + "." + _strBizName;

                if (!EventLog.SourceExists(sSource))
                    EventLog.CreateEventSource(sSource, "IAN_Error");

                EventLog Log = new EventLog();
                Log.Source = sSource;
                Log.WriteEntry(sMessage, ErrorType);

                Log.Dispose();
                Log.Close();
            }
            catch
            {
            }
        }

        //Unhandled Exception Log처리 
        //이벤트 로그 처리부분은 하드코딩 : 윈도우 이벤트방식
        private static void LogEvent(string sMessage)
        {
            try
            {
                //보이는것과 달리 source이름이 유니크하다 
                //레지스트리에서 직접 삭제 HKEY_LOCAL_MACHINE\SYSTEM\ControlSet001\Services\Eventlog\ 
                //삭제는 DeleteEventSource 와 Delete 로 한다.
                if (!EventLog.SourceExists("GlobalError"))
                {
                    EventLog.CreateEventSource("GlobalError", "IAN_Error");
                }

                // Inserting into event log
                EventLog Log = new EventLog();
                Log.Source = "GlobalError";
                Log.WriteEntry(sMessage, EventLogEntryType.Error);

                Log.Dispose();
                Log.Close();


            }
            catch
            {
            }
        }

        #endregion
    }
}
