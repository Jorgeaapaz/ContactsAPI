using ContactsAPI.DataAccess;
using ContactsAPI.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactsAPI.Data
{
    public class ContactsData: DataAccess
    {
        #region PROPERTIES
        private JObject results;

        public JObject Results
        {
            get 
            { 
                if(results == null)
                    results = new JObject();
                return results; 
            }
            set { results = value; }
        }
        #endregion


        #region METHODS
        /// <summary>
        /// Get Contacts Data and total rows count for Pagination
        /// Create a JArray of the results by adding contacts data and count
        /// </summary>
        /// <param name="model"></param>
        public void getContacts(ContactsModelRequest model)
        { 
            int count = 0;
            try
            {
                ApiReturnCode = System.Net.HttpStatusCode.OK;
                ContactsDataAccessQry qry = new ContactsDataAccessQry();
                objQueryResults = qry.GetContacts(model);

                if (objQueryResults.Rows.Count > 0)
                {
                    JArray jaResult = null;
                    jaResult = Utilities.CreateJArrayToString(objQueryResults);
                    Results.Add("contacts", jaResult);
                    count =  GetContactsCount(model);
                    Results.Add("count", count);
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }


        /// <summary>
        /// Get Contacts Count for Pagination
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int GetContactsCount(ContactsModelRequest model)
        {
            int count = 0;
            try
            {
                ApiReturnCode = System.Net.HttpStatusCode.OK;
                ContactsDataAccessQry qry = new ContactsDataAccessQry();
                objQueryResults = qry.GetContactsCount(model);

                if (objQueryResults.Rows.Count > 0)
                {
                    count = Convert.ToInt32(objQueryResults.Rows[0]["count"]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            return count;
        }


        /// <summary>
        /// Get Data for Export Contacts
        /// </summary>
        /// <param name="model"></param>
        public JArray ExportContacts(ContactsModelRequest model)
        {
            JArray jaResult = null;
            try
            {
                ApiReturnCode = System.Net.HttpStatusCode.OK;
                ContactsDataAccessQry qry = new ContactsDataAccessQry();
                objQueryResults = qry.ExportContacts(model);

                if (objQueryResults.Rows.Count > 0)
                {
                    jaResult = Utilities.CreateJArrayToString(objQueryResults);
                }
                return jaResult;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            return jaResult;
        }
        #endregion
    }
}