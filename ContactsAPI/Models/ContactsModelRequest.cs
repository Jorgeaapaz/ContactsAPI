using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactsAPI.Models
{
    public class ContactsModelRequest: Pagination
    {
        public string contactId;
        public string plant;
        public string location;
        public string function;
        public string contactName;
        public string phoneWork;
        public string email;

        public string CONTACT_ID { get =>contactId; set => contactId=value; }
        public string PLANT { get => plant; set => plant = value; }
        public string LOCATION { get => location; set => location = value; }
        public string FUNCTION { get => function; set => function = value; }
        public string CONTACT { get => contactName; set => contactName = value; }
        public string PHONE_WORK { get => phoneWork; set => phoneWork = value; }
        public string EMAIL { get => email; set => email = value; }
        public string EXPORT_COLUMNS { get; set; } = "";

    }
}