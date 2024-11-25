using ContactsAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ContactsAPI.DataAccess
{
    public class ContactsQryExecute
    {
        #region FIELDS
        private readonly string connectionString = "Server=tcp:jaapdemo.database.windows.net,1433;Initial Catalog=CatalogOne;Persist Security Info=False;User ID=dbadmin;Password=Pa11word;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        #endregion


        #region CONSTUCTOR
        public ContactsQryExecute()
        { }
        #endregion


        #region METHODS
        /// <summary>
        /// Gets List of Contact model from Azure Database
        /// using ADO.NET connection, query, command, reader
        /// return list of response model
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<ContactsModelResponse> GetContacts(string query)
        {
            List<ContactsModelResponse> contacts = new List<ContactsModelResponse>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ContactsModelResponse contact = new ContactsModelResponse
                        {
                            contactId = reader.GetString(0),
                            plant = reader.GetString(1),
                            location = reader.GetString(2),
                            function = reader.GetString(3),
                            contactName = reader.GetString(4),
                            phoneWork = reader.GetString(5),
                            email = reader.GetString(6)
                        };
                        contacts.Add(contact);
                    }
                }
            }
            return contacts;
        }


        /// <summary>
        /// Get Contacts Count from Azure Database
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<ContactsCountModelResponse> GetContactsCount(string query)
        {
            List<ContactsCountModelResponse> contactsCount = new List<ContactsCountModelResponse>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ContactsCountModelResponse contact = new ContactsCountModelResponse
                        {
                            count = reader.GetInt32(0).ToString()
                        };
                        contactsCount.Add(contact);
                    }
                }
            }
            return contactsCount;
        }
        #endregion

    }
}