using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assessment.Web.Data
{
    public class CalculationResult
    {
        public int Id { get; set; }
        public int CalculationHeaderId { get; set; }
        public virtual CalculationHeader CalculationHeader { get; set; }
        public string Formular { get; set; }
        public double InputA { get; set; }
        public double InputB { get; set; }
        public double InputC { get; set; }
        public double Result { get; set; }
        public string Message { get; set; }
    }
}
