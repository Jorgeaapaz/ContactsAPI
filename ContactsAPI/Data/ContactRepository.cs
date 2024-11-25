using ContactsAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ContactsAPI.Data
{
    public class ContactRepository
    {
        #region FIELDS
        private readonly string connectionString = "Server=tcp:jaapdemo.database.windows.net,1433;Initial Catalog=CatalogOne;Persist Security Info=False;User ID=dbadmin;Password=Pa11word;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        #endregion


        #region CONSTUCTOR
        public ContactRepository()
        { }
        #endregion


        #region METHODS
        /// <summary>
        /// Gets List of Contact model from Azure Database
        /// connection, query, command, reader, and model are used
        /// </summary>
        /// <returns></returns>
        public List<ContactsModelResponse> GetContacts()
        {
            List<ContactsModelResponse> contacts = new List<ContactsModelResponse>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                SELECT [CONTACT_ID]
                      ,[PLANT]
                      ,[LOCATION]
                      ,[FUNCTION]
                      ,[CONTACT]
                      ,[PHONE_WORK]
                      ,[EMAIL]
                FROM [dbo].[Contacts_Demo]";

                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ContactsModelResponse contact = new ContactsModelResponse
                        {
                            CONTACT_ID = reader.GetString(0),
                            PLANT = reader.GetString(1),
                            LOCATION = reader.GetString(2),
                            FUNCTION = reader.GetString(3),
                            CONTACT = reader.GetString(4),
                            PHONE_WORK = reader.GetString(5),
                            EMAIL = reader.GetString(6)
                        };
                        contacts.Add(contact);
                    }
                }
            }

            return contacts;
        }
        #endregion
    }
}