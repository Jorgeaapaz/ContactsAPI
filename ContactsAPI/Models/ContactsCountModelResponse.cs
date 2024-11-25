using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactsAPI.Models
{
    public class ContactsCountModelResponse
    {
        public string count;

        public string COUNT { get =>count; set => count=value; }
    }
}