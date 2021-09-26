using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleApiRequest.Model
{
    public class SearchResult
    {
        public string CompanyName { get; set; }
        public double Price { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
