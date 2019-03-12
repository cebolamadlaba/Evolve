using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assessment.Web.Data
{
    public class CalculationHeader
    {
        public int Id { get; set; }
        public DateTime UploadTime { get; set; }
        public int Size { get; set; }
        public string FileName { get; set; }
        public string Status { get; set; } = "Pending";
        public DateTime CompletedTime { get; set; }
    }
}
