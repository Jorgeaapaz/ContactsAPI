using ClosedXML.Excel;
using ContactsAPI.Data;
using ContactsAPI.Models;
using DocumentFormat.OpenXml.Spreadsheet;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ContactsAPI.Controllers
{
    public class ContactsController : ApiController
    {
        #region FIELDS
        #endregion

        #region METHODS
        /// <summary>
        /// Basic POC to get all contacts from Azure SQL Database
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Contacts/GetAllContacts")]
        public IHttpActionResult GetAllContacts()
        {
            List<ContactsModelResponse> contacts = new List<ContactsModelResponse>();            
            contacts = new ContactsAPI.Data.ContactRepository().GetContacts();
            return Ok(contacts);
        }


        /// <summary>
        /// POST Controller to get contacts from Azure SQL Database
        /// based on Pagination/Filter Model
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Contacts/GetContacts")]
        public IHttpActionResult GetContacts(ContactsModelRequest model)
        {
            ContactsData Qry = new ContactsData();
            Qry.getContacts(model);
            return ResponseMessage(Request.CreateResponse(Qry.ApiReturnCode, Qry.Results));
        }


        /// <summary>
        /// POST Controller to export contacts to Excel
        /// Note: NuGet Package ClosedXML ver 0.95.4 is used to create Excel
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Contacts/ExportContacts")]
        public HttpResponseMessage ExportContacts(ContactsModelRequest model)
        {
            HttpResponseMessage result = null;

            // Get the list of columns to export
            var colList = JsonConvert.DeserializeObject<List<ExportColumnsFromUIModel>>(model.EXPORT_COLUMNS);

            try
            {
                ContactsData Qry = new ContactsData();
                JArray jArray = Qry.ExportContacts(model);
                var cols = new List<string>();

                if (jArray != null)
                {
                    // Create Excel workbook
                    using(var workbook = new XLWorkbook())
                    {
                        var workSheet = workbook.Worksheets.Add("Azure Contacts");

                        // Add headers
                        var headers = ((JObject)jArray[0]).Properties().Select(p => p.Name).ToArray();
                        for(int i = 0; i < headers.Length ; i++)
                        {
                            if(colList.Where(w => w.field.Equals(headers[i])).Count() > 0)
                            {
                                workSheet.Cell(1, i+1).Value = colList.Where(w => w.field.Equals(headers[i])).Select(s => s.label).FirstOrDefault();
                                workSheet.Cell(1, i + 1).Style.Fill.BackgroundColor = XLColor.FromHtml("#bdbdbd");
                                workSheet.Cell(1, i + 1).RichText.SetFontColor(XLColor.Black);
                            }
                        }

                        // Add data
                        for(int i = 0; i < jArray.Count; i++)
                        {
                            var row = jArray[i];
                            for (int j = 0; j < headers.Length; j++)
                            {
                                if (colList.Where(w => w.field.Equals(headers[j])).Count() > 0)
                                {
                                    var cellValue = ((JObject)row)[headers[j]].ToString();
                                    workSheet.Cell(i + 2, j + 1).SetValue(cellValue);
                                }
                            }
                        }
                        workSheet.Columns().AdjustToContents();

                        // Create the Excel file
                        using(var stream = new MemoryStream())
                        {
                            workbook.SaveAs(stream);
                            var content = stream.ToArray();

                            result = Request.CreateResponse(HttpStatusCode.OK);
                            result.Content = new ByteArrayContent(content);
                            result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                            result.Content.Headers.ContentDisposition.FileName = "Contacts.xlsx";
                        }
                    }
                }
                else
                {
                    result = Request.CreateResponse(HttpStatusCode.NoContent);
                    return result;
                }                
            }
            catch (Exception ex)
            {
                result = Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return result;
        }
        #endregion


        #region Beginner Code
        //// GET: api/Contacts
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET: api/Contacts/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST: api/Contacts
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT: api/Contacts/5
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE: api/Contacts/5
        //public void Delete(int id)
        //{
        //}
        #endregion
    }
}
