using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.DTOs
{
    public class ContactbulkOptionsServiceResponseDTO<T>
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public T? Payload { get; set; }
    }
    public class ContactbulkOptionsServiceResponseDTO
    {
        public string Status { get; set; }
        public string Message { get; set; }
    }
}
