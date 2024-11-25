using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ContactsAPI.Data
{
    public class Utilities
    {
        #region METHODS
        /// <summary>
        /// Create a JArray from a DataTable
        /// </summary>
        /// <param name="resultTable"></param>
        /// <param name="forceStringFormatCols"></param>
        /// <returns></returns>
        internal static JArray CreateJArrayToString(DataTable resultTable, List<string> forceStringFormatCols = null )
        {
            JArray result = new JArray();
            JObject obj;

            if (resultTable != null)
            {
                foreach (DataRow row in resultTable.Rows)
                {
                    obj = new JObject();
                    foreach (DataColumn col in resultTable.Columns)
                    {
                        if (forceStringFormatCols != null && forceStringFormatCols.Contains(col.ColumnName))
                        {
                            obj.Add(col.ColumnName, row[col].ToString());
                        }
                        else
                        {
                            obj.Add(col.ColumnName, GetValue(row[col]));
                        }
                    }
                    result.Add(obj);
                }
            }
            return result;
        }


        /// <summary>
        /// Get the JToken value based on the object type
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static JToken GetValue(object value)
        {
            if (value == null || value == DBNull.Value)
            {
                return JValue.CreateNull();
            }
            else if (value is DateTime)
            {
                return new JValue(((DateTime)value).ToString("yyyy-MM-ddTHH:mm:ss"));
            }
            else if (value is bool)
            {
                return new JValue((bool)value);
            }
            else if (value is byte)
            {
                return new JValue((byte)value);
            }
            else if (value is byte[])
            {
                return new JValue((byte[])value);
            }
            else if (value is decimal)
            {
                return new JValue((decimal)value);
            }
            else if (value is double)
            {
                return new JValue((double)value);
            }
            else if (value is float)
            {
                return new JValue((float)value);
            }
            else if (value is int)
            {
                return new JValue((int)value);
            }
            else if (value is long)
            {
                return new JValue((long)value);
            }
            else if (value is sbyte)
            {
                return new JValue((sbyte)value);
            }
            else if (value is short)
            {
                return new JValue((short)value);
            }
            else if (value is uint)
            {
                return new JValue((uint)value);
            }
            else if (value is ulong)
            {
                return new JValue((ulong)value);
            }
            else if (value is ushort)
            {
                return new JValue((ushort)value);
            }
            else
            {
                return new JValue(value.ToString());
            }
        }
        #endregion
    }
}