using System;
using System.Data;
using System.Net;

namespace ContactsAPI.Data
{
    public class DataAccess
    {
        #region FIELDS
        private string connectionString;
        public DataTable objQueryResults;
        protected DataTable resultTable;
        #endregion

        #region PROPRETIES
        public HttpStatusCode ApiReturnCode { get; set; }
        public string ApiReturnMessage { get; set; }
        public DataRow drCurrentRow { get; set; }
        #endregion

        #region METHODS
        /// <summary>
        /// Get integer value from DataRow field
        /// </summary>
        /// <param name="colName"></param>
        /// <returns></returns>
        public int GetInt(string colName)
        {
            int value = 0;
            try
            {
                value = drCurrentRow[colName.ToString()] == DBNull.Value ? 0 : Convert.ToInt32(drCurrentRow[colName.ToString()]);
            }
            catch
            { }
            return value;
        }


        /// <summary>
        /// Get float value from DataRow field
        /// </summary>
        /// <param name="colName"></param>
        /// <returns></returns>
        public float GetFloat(string colName)
        {
            float value = 0;
            try
            {
                value = drCurrentRow[colName.ToString()] == DBNull.Value ? 0 : Convert.ToSingle(drCurrentRow[colName.ToString()]);
            }
            catch
            { }
            return value;
        }


        /// <summary>
        /// Get string value from DataRow field
        /// </summary>
        /// <param name="colName"></param>
        /// <returns></returns>
        public string GetString(string colName)
        {
            string value = "";
            try
            {
                value = drCurrentRow[colName.ToString()] == DBNull.Value ? string.Empty : drCurrentRow[colName.ToString()].ToString();
            }
            catch
            { }
            return value;
        }


        /// <summary>
        /// Get Time value from DataRow field
        /// </summary>
        /// <param name="colName"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public TimeSpan GetTime(string colName)
        {
            TimeSpan value = TimeSpan.MinValue;
            try
            {
                value = drCurrentRow[colName.ToString()] == DBNull.Value ? TimeSpan.MinValue : TimeSpan.Parse(drCurrentRow[colName.ToString()].ToString());
            }
            catch
            { 
                throw new Exception("Error parsing time value");
            }
            return value;
        }


        /// <summary>
        /// Get Date value from DataRow field
        /// </summary>
        /// <param name="colName"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public virtual DateTime GetDate(string colName)
        {
            DateTime value = DateTime.MinValue;
            try
            {
                value = drCurrentRow[colName.ToString()] == DBNull.Value ? 
                    DateTime.MinValue :  
                    DateTime.Parse(DateTime.Parse(drCurrentRow[colName.ToString()].ToString()).ToString("MM/dd/yyyy"));
            }
            catch
            {
                throw new Exception("Error parsing date value");
            }
            return value;
        }
        #endregion
    }
}