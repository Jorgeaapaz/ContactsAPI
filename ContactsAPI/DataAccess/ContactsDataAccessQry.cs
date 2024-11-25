using ContactsAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ContactsAPI.DataAccess
{
    public class ContactsDataAccessQry
    {
        #region FIELDS
        string strGenericQry = string.Empty;
        #endregion


        #region METHODS
        /// <summary>
        /// Method to create custom SQL Query based on Pagination/Filter Model,
        /// get results as DataTable
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public DataTable GetContacts(ContactsModelRequest model)
        {
            DataTable dtQueryResults = null;

            try
            {
                // Get the query string
                strGenericQry = "SELECT [CONTACT_ID] , [PLANT], [LOCATION], [FUNCTION], [CONTACT], [PHONE_WORK], [EMAIL] FROM [dbo].[Contacts_Demo] WHERE {CONTACT_ID} AND  {PLANT} AND  {LOCATION} AND  {FUNCTION} AND  {CONTACT} AND  {PHONE_WORK} AND  {EMAIL} {OREDER_BY} {SORT_DIRECTION} OFFSET {OFFSET} ROWS FETCH NEXT {ROWS_PP} ROWS ONLY";

                // Dertmine the Sort By Column
                if(string.IsNullOrEmpty(model.sortBy))
                {
                    strGenericQry = strGenericQry.Replace("{OREDER_BY}", "ORDER BY [CONTACT_ID]");
                }
                else
                {
                    strGenericQry = strGenericQry.Replace("{OREDER_BY}", "ORDER BY [" + model.sortBy + "]");
                }

                // Determine the Sort Order
                if(model.descending)
                {
                    strGenericQry = strGenericQry.Replace("{SORT_DIRECTION}", "DESC");
                }
                else
                {
                    strGenericQry = strGenericQry.Replace("{SORT_DIRECTION}", "ASC");
                }

                // Determine Offset and Rows Per Page
                strGenericQry = strGenericQry.Replace("{OFFSET}", model.initialRow.ToString());
                strGenericQry = strGenericQry.Replace("{ROWS_PP}", model.rowsPerPage.ToString());

                // Determine the Filter fields
                strGenericQry = string.IsNullOrEmpty(model.contactId) ?
                    strGenericQry.Replace("{CONTACT_ID}", "1=1") :
                    strGenericQry.Replace("{CONTACT_ID}", $"UPPER([CONTACT_ID]) LIKE UPPER(\'{model.contactId}\')"); 

                strGenericQry = string.IsNullOrEmpty(model.plant) ?
                    strGenericQry.Replace("{PLANT}", "1=1") :
                    strGenericQry.Replace("{PLANT}", $"UPPER([PLANT]) LIKE UPPER(\'{model.plant}\')"); 

                strGenericQry = string.IsNullOrEmpty(model.location) ?
                    strGenericQry.Replace("{LOCATION}", "1=1") :
                    strGenericQry.Replace("{LOCATION}", $"UPPER([LOCATION]) LIKE UPPER(\'{model.location}\')"); 

                strGenericQry = string.IsNullOrEmpty(model.function) ?
                    strGenericQry.Replace("{FUNCTION}", "1=1") :
                    strGenericQry.Replace("{FUNCTION}", $"UPPER([FUNCTION]) LIKE UPPER(\'{model.function}\')"); 

                strGenericQry = string.IsNullOrEmpty(model.contactName) ?
                    strGenericQry.Replace("{CONTACT}", "1=1") :
                    strGenericQry.Replace("{CONTACT}", $"UPPER([CONTACT]) LIKE UPPER(\'{model.contactName}\')"); 

                strGenericQry = string.IsNullOrEmpty(model.phoneWork) ?
                    strGenericQry.Replace("{PHONE_WORK}", "1=1") :
                    strGenericQry.Replace("{PHONE_WORK}", $"UPPER([PHONE_WORK]) LIKE UPPER(\'{model.phoneWork}\')"); 

                strGenericQry = string.IsNullOrEmpty(model.email) ?
                    strGenericQry.Replace("{EMAIL}", "1=1") :
                    strGenericQry.Replace("{EMAIL}", $"UPPER([EMAIL]) LIKE UPPER(\'{model.email}\')"); 

                // Execute the Query
                ContactsQryExecute service = new ContactsQryExecute();
                List<ContactsModelResponse> dtQueryResultsList = service.GetContacts(strGenericQry);

                // Convert List to DataTable
                dtQueryResults = ConvertToDataTable(dtQueryResultsList);                

                // Return the results
                return dtQueryResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Method to get total record count
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        internal DataTable GetContactsCount(ContactsModelRequest model)
        {
            DataTable dtQueryResults = null;

            try
            {
                // Get the query string
                strGenericQry = "SELECT count(*) AS [COUNT] FROM [dbo].[Contacts_Demo] WHERE {CONTACT_ID} AND  {PLANT} AND  {LOCATION} AND  {FUNCTION} AND  {CONTACT} AND  {PHONE_WORK} AND  {EMAIL}";

                // Determine the Filter fields
                strGenericQry = string.IsNullOrEmpty(model.contactId) ?
                    strGenericQry.Replace("{CONTACT_ID}", "1=1") :
                    strGenericQry.Replace("{CONTACT_ID}", $"UPPER([CONTACT_ID]) LIKE UPPER(\'{model.contactId}\')");

                strGenericQry = string.IsNullOrEmpty(model.plant) ?
                    strGenericQry.Replace("{PLANT}", "1=1") :
                    strGenericQry.Replace("{PLANT}", $"UPPER([PLANT]) LIKE UPPER(\'{model.plant}\')");

                strGenericQry = string.IsNullOrEmpty(model.location) ?
                    strGenericQry.Replace("{LOCATION}", "1=1") :
                    strGenericQry.Replace("{LOCATION}", $"UPPER([LOCATION]) LIKE UPPER(\'{model.location}\')");

                strGenericQry = string.IsNullOrEmpty(model.function) ?
                    strGenericQry.Replace("{FUNCTION}", "1=1") :
                    strGenericQry.Replace("{FUNCTION}", $"UPPER([FUNCTION]) LIKE UPPER(\'{model.function}\')");

                strGenericQry = string.IsNullOrEmpty(model.contactName) ?
                    strGenericQry.Replace("{CONTACT}", "1=1") :
                    strGenericQry.Replace("{CONTACT}", $"UPPER([CONTACT]) LIKE UPPER(\'{model.contactName}\')");

                strGenericQry = string.IsNullOrEmpty(model.phoneWork) ?
                    strGenericQry.Replace("{PHONE_WORK}", "1=1") :
                    strGenericQry.Replace("{PHONE_WORK}", $"UPPER([PHONE_WORK]) LIKE UPPER(\'{model.phoneWork}\')");

                strGenericQry = string.IsNullOrEmpty(model.email) ?
                    strGenericQry.Replace("{EMAIL}", "1=1") :
                    strGenericQry.Replace("{EMAIL}", $"UPPER([EMAIL]) LIKE UPPER(\'{model.email}\')");

                // Execute the Query
                ContactsQryExecute service = new ContactsQryExecute();
                List<ContactsCountModelResponse> dtQueryResultsList = service.GetContactsCount(strGenericQry);

                // Convert List to DataTable
                dtQueryResults = ConvertContactCountToDataTable(dtQueryResultsList);

                // Return the results
                return dtQueryResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Method to get data to export Contacts to Excel
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public DataTable ExportContacts(ContactsModelRequest model)
        {
            DataTable dtQueryResults = null;

            try
            {
                // Get the query string
                strGenericQry = "SELECT [CONTACT_ID] , [PLANT], [LOCATION], [FUNCTION], [CONTACT], [PHONE_WORK], [EMAIL] FROM [dbo].[Contacts_Demo] WHERE {CONTACT_ID} AND  {PLANT} AND  {LOCATION} AND  {FUNCTION} AND  {CONTACT} AND  {PHONE_WORK} AND {EMAIL} ORDER BY [CONTACT_ID] ASC";

                // Determine the Filter fields
                strGenericQry = string.IsNullOrEmpty(model.contactId) ?
                    strGenericQry.Replace("{CONTACT_ID}", "1=1") :
                    strGenericQry.Replace("{CONTACT_ID}", $"UPPER([CONTACT_ID]) LIKE UPPER(\'{model.contactId}\')");

                strGenericQry = string.IsNullOrEmpty(model.plant) ?
                    strGenericQry.Replace("{PLANT}", "1=1") :
                    strGenericQry.Replace("{PLANT}", $"UPPER([PLANT]) LIKE UPPER(\'{model.plant}\')");

                strGenericQry = string.IsNullOrEmpty(model.location) ?
                    strGenericQry.Replace("{LOCATION}", "1=1") :
                    strGenericQry.Replace("{LOCATION}", $"UPPER([LOCATION]) LIKE UPPER(\'{model.location}\')");

                strGenericQry = string.IsNullOrEmpty(model.function) ?
                    strGenericQry.Replace("{FUNCTION}", "1=1") :
                    strGenericQry.Replace("{FUNCTION}", $"UPPER([FUNCTION]) LIKE UPPER(\'{model.function}\')");

                strGenericQry = string.IsNullOrEmpty(model.contactName) ?
                    strGenericQry.Replace("{CONTACT}", "1=1") :
                    strGenericQry.Replace("{CONTACT}", $"UPPER([CONTACT]) LIKE UPPER(\'{model.contactName}\')");

                strGenericQry = string.IsNullOrEmpty(model.phoneWork) ?
                    strGenericQry.Replace("{PHONE_WORK}", "1=1") :
                    strGenericQry.Replace("{PHONE_WORK}", $"UPPER([PHONE_WORK]) LIKE UPPER(\'{model.phoneWork}\')");

                strGenericQry = string.IsNullOrEmpty(model.email) ?
                    strGenericQry.Replace("{EMAIL}", "1=1") :
                    strGenericQry.Replace("{EMAIL}", $"UPPER([EMAIL]) LIKE UPPER(\'{model.email}\')");

                // Execute the Query
                ContactsQryExecute service = new ContactsQryExecute();
                List<ContactsModelResponse> dtQueryResultsList = service.GetContacts(strGenericQry);

                // Convert List to DataTable
                dtQueryResults = ConvertToDataTable(dtQueryResultsList);

                // Return the results
                return dtQueryResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Method to convert List of ContactsModelResponse to DataTable
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public DataTable ConvertToDataTable(List<ContactsModelResponse> data)
        {
            DataTable dataTable = new DataTable(typeof(ContactsModelResponse).Name);

            // Get all the properties
            PropertyInfo[] Props = typeof(ContactsModelResponse).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                // Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }

            foreach (ContactsModelResponse item in data)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    // Inserting property values to DataTable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }

            return dataTable;
        }


        /// <summary>
        /// Method to convert List of ContactsCountModelResponse to DataTable
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public DataTable ConvertContactCountToDataTable(List<ContactsCountModelResponse> data)
        {
            DataTable dataTable = new DataTable(typeof(ContactsCountModelResponse).Name);

            // Get all the properties
            PropertyInfo[] Props = typeof(ContactsCountModelResponse).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                // Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }

            foreach (ContactsCountModelResponse item in data)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    // Inserting property values to DataTable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }

            return dataTable;
        }

        #endregion
    }
}