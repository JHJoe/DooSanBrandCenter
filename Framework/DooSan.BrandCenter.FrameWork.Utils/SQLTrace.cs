using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using System.Configuration;
using System.Data.Common;
using System.Data;
using System.Data.Odbc;

namespace DooSan.BrandCenter.FrameWork.Utils
{
    #region SQLTrace
    /// <summary>
    /// 
    /// </summary>
    public class SQLTraceClass
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetSQLTraceSettings(string name)
        {
            NameValueCollection nvCols = (NameValueCollection)ConfigurationManager.GetSection("SqlTrace");
            if (nvCols == null)
                return null;
            else
            {
                if (nvCols[name] != null)
                    return nvCols[name].ToString();
                else
                    return null;
            }
        }


        #region DB-SQL ORACLE
        /// <summary>
        /// 디버그용으로, timestamp는 찍히지 않으나 DB에 실행전 찍힌다. 
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="markEndLine"></param>
        public static void debugSQLTrace(DbCommand cmd, bool markEndLine)
        {
            if (GetSQLTraceSettings("debugSQLTraceMode") != null)
            {
                string SQLTraceMode = GetSQLTraceSettings("debugSQLTraceMode");
                string SQLTracePath = "c:\\";
                if (GetSQLTraceSettings("debugSQLTracePath") != null)
                    SQLTracePath = GetSQLTraceSettings("debugSQLTracePath");
                if (SQLTraceMode.Equals("on")) debugSQLTrace(SQLTracePath, cmd, markEndLine);
            }
        }
        /// <summary>
        /// 디버그용으로, timestamp는 찍히지 않으나 DB에 실행전 찍힌다. 
        /// </summary>
        /// <param name="SQLTracePath"></param>
        /// <param name="cmd"></param>
        /// <param name="markEndLine"></param>
        public static void debugSQLTrace(string SQLTracePath, DbCommand cmd, bool markEndLine)
        {
            //stamp
            long startTick = DateTime.Now.Ticks;

            System.Text.StringBuilder oBuilder = new System.Text.StringBuilder();

            oBuilder.Append(System.DateTime.Now.ToString());

            oBuilder.Append("\r\n");
            oBuilder.Append("ConnectionString : " + cmd.Connection.ConnectionString);

            oBuilder.Append("\r\n");
            oBuilder.Append(cmd.CommandText);

            if (cmd.Parameters.Count > 0)
            {
                if (cmd.CommandType == CommandType.Text)
                {
                    for (int iElemCnt = 0; iElemCnt < cmd.Parameters.Count; iElemCnt++)
                    {
                        oBuilder.Replace(cmd.Parameters[iElemCnt].ParameterName, SqlParameterValue2String(cmd.Parameters[iElemCnt].DbType, cmd.Parameters[iElemCnt].Value));
                    }
                }
                //else
                //{
                for (int iElemCnt = 0; iElemCnt < cmd.Parameters.Count; iElemCnt++)
                {
                    oBuilder.Append("\r\n");
                    oBuilder.Append(cmd.Parameters[iElemCnt].ParameterName);
                    oBuilder.Append(" = ");
                    oBuilder.Append(SqlParameterValue2String(cmd.Parameters[iElemCnt].DbType, cmd.Parameters[iElemCnt].Value));
                }
                //}
            }
            //  oBuilder.Append("TimeSpan Seconds:   " + (new TimeSpan(DateTime.Now.Ticks - startTick)).ToString());
            if (markEndLine) oBuilder.Append("\r\n-------------------------------------------------------------");
            string sContents = oBuilder.ToString();

            string sFileName = string.Format("{1}_debugSQLQueryTrc_{0}.log", System.Net.Dns.GetHostName().ToLower(),
                System.DateTime.Now.ToString("yyyy-MM-dd"));

            LogToFile(SQLTracePath, sFileName, sContents);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="markEndLine"></param>
        /// <param name="timeSpans"></param>
        public static void SQLTrace(DbCommand cmd, bool markEndLine, long timeSpans)
        {
            SQLTrace(cmd, markEndLine, timeSpans, null);
            //if (GetSQLTraceSettings("SQLTraceMode") != null)
            //{
            //    string SQLTraceMode = GetSQLTraceSettings("SQLTraceMode");
            //    string SQLTracePath = "c:\\";
            //    if (GetSQLTraceSettings("SQLTracePath") != null)
            //        SQLTracePath = GetSQLTraceSettings("SQLTracePath");
            //    if (SQLTraceMode.Equals("on")) SQLTrace(SQLTracePath, cmd, markEndLine, timeSpans);
            //}
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="markEndLine"></param>
        /// <param name="timeSpans"></param>
        /// <param name="ds"></param>
        public static void SQLTrace(DbCommand cmd, bool markEndLine, long timeSpans, DataSet ds)
        {
            if (GetSQLTraceSettings("SQLTraceMode") != null)
            {
                string SQLTraceMode = GetSQLTraceSettings("SQLTraceMode");
                string SQLTracePath = "c:\\";
                if (GetSQLTraceSettings("SQLTracePath") != null)
                    SQLTracePath = GetSQLTraceSettings("SQLTracePath");
                if (SQLTraceMode.Equals("on"))
                {
                    if (GetSQLTraceSettings("DataSetTrace") != null)
                    {
                        if (GetSQLTraceSettings("DataSetTrace").Equals("on"))
                            SQLTrace(SQLTracePath, cmd, markEndLine, timeSpans, ds);
                        else
                            SQLTrace(SQLTracePath, cmd, markEndLine, timeSpans, null);
                    }
                    else
                        SQLTrace(SQLTracePath, cmd, markEndLine, timeSpans, null);

                }
            }
        }


        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="SQLTracePath"></param>
        ///// <param name="cmd"></param>
        ///// <param name="markEndLine"></param>
        ///// <param name="timeSpans"></param>
        //public static void SQLTrace(string SQLTracePath, DbCommand cmd, bool markEndLine, long timeSpans)
        //{
        //    //stamp
        //    long startTick = DateTime.Now.Ticks;

        //    System.Text.StringBuilder oBuilder = new System.Text.StringBuilder();

        //    oBuilder.Append(System.DateTime.Now.ToString());
        //    oBuilder.Append("\r\n");
        //    oBuilder.Append("ConnectionString : " + cmd.Connection.ConnectionString); 
        //    oBuilder.Append("\r\n");
        //    oBuilder.Append(cmd.CommandText);

        //    if (cmd.Parameters.Count > 0)
        //    {
        //        if (cmd.CommandType == CommandType.Text)
        //        {
        //            for (int iElemCnt = 0; iElemCnt < cmd.Parameters.Count; iElemCnt++)
        //            {
        //                oBuilder.Replace(cmd.Parameters[iElemCnt].ParameterName, SqlParameterValue2String(cmd.Parameters[iElemCnt].DbType, cmd.Parameters[iElemCnt].Value));
        //            }
        //        }
        //        else
        //        {
        //            for (int iElemCnt = 0; iElemCnt < cmd.Parameters.Count; iElemCnt++)
        //            {
        //                oBuilder.Append("\r\n");
        //                oBuilder.Append(cmd.Parameters[iElemCnt].ParameterName);
        //                oBuilder.Append(" = ");
        //                oBuilder.Append(SqlParameterValue2String(cmd.Parameters[iElemCnt].DbType, cmd.Parameters[iElemCnt].Value));
        //            }
        //        }
        //    }
        //    if (timeSpans != -1) oBuilder.Append("\r\nTimeSpan Seconds:   " + (new TimeSpan(timeSpans)).ToString());
        //    if (markEndLine) oBuilder.Append("\r\n-------------------------------------------------------------");
        //    string sContents = oBuilder.ToString();

        //    string sFileName = string.Format("{1}_SQLQueryTrc_{0}.log", System.Net.Dns.GetHostName().ToLower(),
        //        System.DateTime.Now.ToString("yyyy-MM-dd"));

        //    LogToFile(SQLTracePath, sFileName, sContents);
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SQLTracePath"></param>
        /// <param name="cmd"></param>
        /// <param name="markEndLine"></param>
        /// <param name="timeSpans"></param>
        /// <param name="ds"></param>
        public static void SQLTrace(string SQLTracePath, DbCommand cmd, bool markEndLine, long timeSpans, DataSet ds)
        {
            //stamp
            long startTick = DateTime.Now.Ticks;

            System.Text.StringBuilder oBuilder = new System.Text.StringBuilder();

            oBuilder.Append(System.DateTime.Now.ToString());
            oBuilder.Append("\r\n");
            oBuilder.Append("ConnectionString : " + cmd.Connection.ConnectionString);
            oBuilder.Append("\r\n");
            oBuilder.Append(cmd.CommandText);

            if (cmd.Parameters.Count > 0)
            {
                if (cmd.CommandType == CommandType.Text)
                {
                    for (int iElemCnt = 0; iElemCnt < cmd.Parameters.Count; iElemCnt++)
                    {
                        oBuilder.Replace(cmd.Parameters[iElemCnt].ParameterName, SqlParameterValue2String(cmd.Parameters[iElemCnt].DbType, cmd.Parameters[iElemCnt].Value));
                    }
                }
                //무조건 나오게..
                //                else
                //                {
                for (int iElemCnt = 0; iElemCnt < cmd.Parameters.Count; iElemCnt++)
                {
                    oBuilder.Append("\r\n");
                    oBuilder.Append(cmd.Parameters[iElemCnt].ParameterName);
                    oBuilder.Append(" = ");
                    oBuilder.Append(SqlParameterValue2String(cmd.Parameters[iElemCnt].DbType, cmd.Parameters[iElemCnt].Value));
                }
                //                }
            }

            if (ds != null)
                oBuilder.Append(DatasetPrint(ds));

            if (timeSpans != -1) oBuilder.Append("\r\nTimeSpan Seconds:   " + (new TimeSpan(timeSpans)).ToString());
            if (markEndLine) oBuilder.Append("\r\n-------------------------------------------------------------");
            string sContents = oBuilder.ToString();

            string sFileName = string.Format("{1}_SQLQueryTrc_{0}.log", System.Net.Dns.GetHostName().ToLower(),
                System.DateTime.Now.ToString("yyyy-MM-dd"));

            LogToFile(SQLTracePath, sFileName, sContents);
        }

        private static string DatasetPrint(DataSet ds)
        {
            StringBuilder sb = new StringBuilder();

            int intPadsize = 17;
            if (GetSQLTraceSettings("DataSetTraceFixSize") != null)
            {
                if (!GetSQLTraceSettings("DataSetTraceFixSize").Equals("-1"))
                    intPadsize = Convert.ToInt32(GetSQLTraceSettings("DataSetTraceFixSize"));
            }


            if (ds != null)
            {
                //                sb.Append("\r\nDataSet:");

                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    sb.Append("\r\nDataSet" + i.ToString() + " : row count " + ds.Tables[i].Rows.Count.ToString() + "\r\n");
                    //                    sb.Append("Column");
                    sb.Append("      ");
                    for (int j = 0; j < ds.Tables[i].Columns.Count; j++)
                    {
                        //sda.FillSchema(DS, System.Data.SchemaType.Source) 와 같이 스키마가 없으면 무조건 -1.. 하지만 스키마를 채우면 비용발생
                        if (ds.Tables[i].Columns[j].MaxLength != -1)
                            sb.Append(ds.Tables[i].Columns[j].ColumnName.PadLeft(ds.Tables[i].Columns[j].MaxLength, ' ') + "|");
                        else
                            sb.Append(ds.Tables[i].Columns[j].ColumnName.PadLeft(intPadsize, ' ') + "|");
                    }

                    for (int k = 0; k < ds.Tables[i].Rows.Count; k++)
                    {
                        sb.Append("\r\n" + k.ToString().PadLeft(5, ' ') + ":");

                        for (int j = 0; j < ds.Tables[i].Columns.Count; j++)
                        {
                            if (ds.Tables[i].Columns[j].MaxLength != -1)
                                sb.Append(ds.Tables[i].Rows[k][j].ToString().PadLeft(ds.Tables[i].Columns[j].MaxLength, ' ') + "|");
                            else
                                sb.Append(ds.Tables[i].Rows[k][j].ToString().PadLeft(intPadsize, ' ') + "|");
                        }

                        if (k == ds.Tables[i].Rows.Count - 1)
                            sb.Append("\r\n");
                    }
                }

            }

            return sb.ToString();
        }

        private static string SqlParameterValue2String(DbType tp, object parameterValue)
        {
            string strReturn = "NULL";

            if (parameterValue != null)
            {
                if (parameterValue == System.DBNull.Value)
                    strReturn = "NULL";
                else
                {
                    switch (tp)
                    {
                        case DbType.String:
                        case DbType.AnsiStringFixedLength:
                        case DbType.StringFixedLength:
                        case DbType.AnsiString:
                            strReturn = string.Concat("'", (string)parameterValue, "'");
                            break;
                        //case DbType.Image:
                        //    strReturn = "<Blob>";
                        //    break;
                        //case DbType.Binary:
                        //    strReturn = "<Binary>";
                        //    break;
                        case DbType.Byte:
                            strReturn = Convert.ToString(parameterValue); //"<Byte>";
                            break;
                        default:
                            strReturn = parameterValue.ToString();
                            break;
                    }
                }
            }

            return strReturn;
        }
        /// <summary>
        /// exception용으로, 에러가 발생시 메세지에 추가하는 작업을 한다.
        /// </summary>
        /// <param name="cmd"></param>
        public static string ExceptionSQLTrace(DbCommand cmd)
        {

            System.Text.StringBuilder oBuilder = new System.Text.StringBuilder();

            //oBuilder.Append(System.DateTime.Now.ToString());
            oBuilder.Append("\r\n");
            oBuilder.Append(cmd.CommandText);

            if (cmd.Parameters.Count > 0)
            {
                if (cmd.CommandType == CommandType.Text)
                {
                    for (int iElemCnt = 0; iElemCnt < cmd.Parameters.Count; iElemCnt++)
                    {
                        oBuilder.Replace(cmd.Parameters[iElemCnt].ParameterName, SqlParameterValue2String(cmd.Parameters[iElemCnt].DbType, cmd.Parameters[iElemCnt].Value));
                    }
                }
                //else
                //{
                for (int iElemCnt = 0; iElemCnt < cmd.Parameters.Count; iElemCnt++)
                {
                    oBuilder.Append("\r\n");
                    oBuilder.Append(cmd.Parameters[iElemCnt].ParameterName);
                    oBuilder.Append(" = ");
                    oBuilder.Append(SqlParameterValue2String(cmd.Parameters[iElemCnt].DbType, cmd.Parameters[iElemCnt].Value));
                }
                //}
            }
            //  oBuilder.Append("TimeSpan Seconds:   " + (new TimeSpan(DateTime.Now.Ticks - startTick)).ToString());
            //            if (markEndLine) 
            oBuilder.Append("\r\n-------------------------------------------------------------\r\n");
            return oBuilder.ToString();
        }

        private static void LogToFile(string logFolder, string fileName, string contents)
        {
            try
            {
                if (!System.IO.Directory.Exists(logFolder))
                    System.IO.Directory.CreateDirectory(logFolder);
                System.IO.FileStream oStream;
                System.IO.StreamWriter oWriter;
                try
                {
                    oStream = new System.IO.FileStream(System.String.Concat(logFolder, "\\", fileName),
                        System.IO.FileMode.Append, System.IO.FileAccess.Write, System.IO.FileShare.ReadWrite);

                    oWriter = new System.IO.StreamWriter(oStream);
                    oWriter.WriteLine(contents);

                }
                catch //(Exception ez)
                {
                    //2008-11-19_debugDB2QueryTrc_xaiwktmp00339Fail.log 로 생김
                    oStream = new System.IO.FileStream(System.String.Concat(logFolder, "\\", fileName.Substring(0, fileName.LastIndexOf(".")) + "Fail.log"),
                    System.IO.FileMode.Append, System.IO.FileAccess.Write, System.IO.FileShare.ReadWrite);
                    oWriter = new System.IO.StreamWriter(oStream);
                    oWriter.WriteLine(contents);
                    //                    throw ez;
                }

                oWriter.Flush();
                oWriter.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        public static void SQLWriteLog(string message)
        {

            try
            {
                if (GetSQLTraceSettings("SQLTraceMode") != null)
                {
                    string SQLTraceMode = GetSQLTraceSettings("SQLTraceMode");
                    string TracePath = "c:\\";
                    string sFileName = string.Format("{0}_ServerLog_{1}.log", System.Net.Dns.GetHostName().ToLower(),
                        //				string sFileName = string.Format("{0}_AppTrace_{1}.log", System.Net.Dns.GetHostName().ToLower(), 
                        System.DateTime.Now.ToString("yyyyMMdd").Replace("-", ""));

                    string SQLTracePath = "c:\\";
                    if (GetSQLTraceSettings("SQLTracePath") != null)
                        SQLTracePath = GetSQLTraceSettings("SQLTracePath");
                    if (SQLTraceMode.Equals("on"))
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

        public static string Params2String(params object[] arrays)
        {
            string returnString = string.Empty;
            for (int i = 0; i < arrays.Length; i++)
            {
                if (i != arrays.Length - 1)
                    returnString += arrays[i].ToString() + ", ";
                else
                    returnString += arrays[i].ToString();

            }
            return returnString;
        }

    }
    #endregion SQLTrace
}
