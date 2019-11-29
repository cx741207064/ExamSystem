using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace JlueCertificate.Untity
{
    public class HelperMsSQL
    {
        public static string connStr = "";

        static HelperMsSQL()
        {

            connStr = ConfigurationManager.ConnectionStrings["SQLConnection"].ToString().Trim();
        }

        /// <summary>
        /// 读取数据库链接
        /// </summary>
        private static string getConnStr()
        {
            if (string.IsNullOrEmpty(connStr))
            {
                connStr = ConfigurationManager.ConnectionStrings["SQLConnection"].ToString().Trim();
            }
            return connStr;
        }

        #region 内部方法

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="cmdtext"></param>
        /// <returns></returns>
        public static int ExecuteQueryExam(string cmdtext)
        {
            int result = 0;
            SqlConnection conn = new SqlConnection();
            try
            {
                conn.ConnectionString = connStr;
                conn.Open();
                SqlCommand cmd = new SqlCommand(cmdtext, conn);
                object obj = Convert.ToInt32(cmd.ExecuteScalar());
                result = (int)obj;
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            conn.Close();
            conn.Dispose();
            return result;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="cmdtext"></param>
        /// <returns></returns>
        public static bool ExecuteQuerySelect(string cmdtext)
        {
            bool result = false;
            SqlConnection conn = new SqlConnection();
            try
            {
                conn.ConnectionString = connStr;
                conn.Open();
                //SqlCommand cmd = new SqlCommand(cmdtext, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmdtext, conn);
                DataTable dt = new DataTable();
                da.Fill(dt); 
                int count = (int)dt.Rows[0][0];
                if (count>0){
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            conn.Close();
            conn.Dispose();
            return result;
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="cmdtext"></param>
        /// <returns></returns>
        public static int ExecuteQuery(string cmdtext)
        {
            int result = 0;
            SqlConnection conn = new SqlConnection();
            try
            {
                conn.ConnectionString = connStr;
                conn.Open();
                SqlCommand cmd = new SqlCommand(cmdtext, conn);
                result = cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            conn.Close();
            conn.Dispose();
            return result;
        }

        /// <summary>
        /// SQL执行
        /// </summary>
        /// <param name="cmdtext"></param>
        /// <param name="sqlparams"></param>
        /// <returns></returns>
        public static int ExecuteQuery(string cmdtext, SqlParameter[] sqlparams)
        {
            int result = 0;
            SqlConnection conn = new SqlConnection();
            try
            {
                conn.ConnectionString = connStr;
                conn.Open();
                SqlCommand cmd = new SqlCommand(cmdtext, conn);
                foreach (var param in sqlparams)
                {
                    cmd.Parameters.Add(param);
                }
                result = cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            conn.Close();
            conn.Dispose();
            return result;
        }

        /// <summary>
        /// SQL执行
        /// </summary>
        /// <param name="cmdtext"></param>
        /// <param name="sqlparams"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string cmdtext)
        {
            object result = null;
            SqlConnection conn = new SqlConnection();
            try
            {
                conn.ConnectionString = connStr;
                conn.Open();
                SqlCommand cmd = new SqlCommand(cmdtext, conn);
                result = cmd.ExecuteScalar();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            conn.Close();
            conn.Dispose();
            return result;
        }


        /// <summary>
        /// SQL执行
        /// </summary>
        /// <param name="cmdtext"></param>
        /// <param name="sqlparams"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string cmdtext, SqlParameter[] sqlparams)
        {
            object result = null;
            SqlConnection conn = new SqlConnection();
            try
            {
                conn.ConnectionString = connStr;
                conn.Open();
                SqlCommand cmd = new SqlCommand(cmdtext, conn);
                foreach (var param in sqlparams)
                {
                    cmd.Parameters.Add(param);
                }
                result = cmd.ExecuteScalar();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            conn.Close();
            conn.Dispose();
            return result;
        }

        /// <summary>
        /// 查询返回List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlText"></param>
        /// <returns></returns>
        public static List<T> ExecuteQueryToList<T>(string sqlText)
        {
            List<T> result = new List<T>();
            try
            {
                DataTable dt = ExecuteQueryToDataTable(sqlText);
                PropertyInfo[] Tproperties = typeof(T).GetProperties();
                DataColumnCollection dtCols = dt.Columns;

                foreach (DataRow _row in dt.Rows)
                {
                    var _t = Activator.CreateInstance(typeof(T));

                    foreach (var Tpropertie in Tproperties)
                    {
                        for (int i = 0; i < dtCols.Count; i++)
                        {
                            if (dtCols[i].ColumnName.ToLower() == Tpropertie.Name.ToLower())
                            {
                                if (Tpropertie.PropertyType == typeof(string))
                                {
                                    string obj = _row[dtCols[i]].ToString();
                                    Tpropertie.SetValue(_t, obj, null);
                                }
                                else if (Tpropertie.PropertyType == typeof(int))
                                {
                                    int obj = 0;
                                    int.TryParse(_row[dtCols[i]].ToString(), out obj);
                                    Tpropertie.SetValue(_t, obj, null);
                                }
                                else if (Tpropertie.PropertyType == typeof(DateTime) || Tpropertie.PropertyType == typeof(DateTime?))
                                {
                                    DateTime obj = new DateTime();
                                    DateTime.TryParse(_row[dtCols[i]].ToString(), out obj);
                                    Tpropertie.SetValue(_t, obj, null);
                                }
                                else if (Tpropertie.PropertyType == typeof(long))
                                {
                                    long obj = 0;
                                    long.TryParse(_row[dtCols[i]].ToString(), out obj);
                                    Tpropertie.SetValue(_t, obj, null);
                                }
                                else if (Tpropertie.PropertyType == typeof(decimal))
                                {
                                    decimal obj = 0;
                                    decimal.TryParse(_row[dtCols[i]].ToString(), out obj);
                                    Tpropertie.SetValue(_t, obj, null);
                                }
                                else if (Tpropertie.PropertyType == typeof(JObject))
                                {
                                    JObject obj;
                                    string val = (string)(_row[dtCols[i]] is DBNull ? "" : _row[dtCols[i]]);
                                    if (val == "")
                                    {
                                        obj = new JObject();
                                    }
                                    else
                                    {
                                        obj = JObject.Parse(_row[dtCols[i]].ToString());
                                    }
                                    Tpropertie.SetValue(_t, obj, null);
                                }
                                break;
                            }
                        }
                    }

                    if (_t == null)
                    {
                        continue;
                    }
                    result.Add((T)_t);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        /// <summary>
        /// 查询返回DataTable
        /// </summary>
        /// <param name="sqltext"></param>
        /// <returns></returns>
        public static DataTable ExecuteQueryToDataTable(string sqltext)
        {
            SqlConnection conn = new SqlConnection();
            DataTable result = new DataTable();
            try
            {
                conn.ConnectionString = connStr;
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqltext, conn);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter sqlDA = new SqlDataAdapter(cmd);
                sqlDA.Fill(result);
                sqlDA.Dispose();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            conn.Close();
            conn.Dispose();
            return result;
        }

        /// <summary>
        /// 查询返回DataTable
        /// </summary>
        /// <param name="sqltext"></param>
        /// <param name="sqlParams"></param>
        /// <returns></returns>
        public static DataTable ExecuteQueryToDataTable(string sqltext, SqlParameter[] sqlParams)
        {
            SqlConnection conn = new SqlConnection();
            DataTable result = new DataTable();
            try
            {
                conn.ConnectionString = connStr;
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqltext, conn);
                cmd.CommandType = CommandType.Text;
                foreach (var param in sqlParams)
                {
                    cmd.Parameters.Add(param);
                }
                SqlDataAdapter sqlDA = new SqlDataAdapter(cmd);
                sqlDA.Fill(result);
                sqlDA.Dispose();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            conn.Close();
            conn.Dispose();
            return result;
        }


        #endregion
    }
}
