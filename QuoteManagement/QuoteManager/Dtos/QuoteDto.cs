using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuoteManagement.Dtos
{
    public class QuoteDto
    {
        public int QuoteID { get; set; }
        public string QuoteType { get; set; }
        public string Contact { get; set; }
        public string Task { get; set; }
        public string DueDate { get; set; }
        public string TaskType { get; set; }
        public bool IfCompleted { get; set; }
        public int? QuoteNumber { get; set; }
    }
}