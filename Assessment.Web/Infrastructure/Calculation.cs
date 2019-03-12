using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assessment.Web.Infrastructure
{
    public struct Calculation
    {
        public string Formula { get; set; }
        public double A { get; set; }
        public double B { get; set; }
        public double C { get; set; }
        public double Result { get; set; }
        public Calculation(string formula, double first, double second, double third, double result)
        {
            Formula = formula;
            A = first;
            B = second;
            C = third;
            Result = result;
        }
    }
}
