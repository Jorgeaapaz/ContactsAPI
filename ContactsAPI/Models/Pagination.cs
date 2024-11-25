using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactsAPI.Models
{
    public class Pagination
    {
        public int page { get; set; }
        public int rowsPerPage { get; set; }
        public string sortBy { get; set; }
        public bool descending { get; set; }
        public int initialRow { get; set; }
    }
}