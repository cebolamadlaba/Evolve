using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Assessment.Web.Infrastructure
{
    public interface IFileParser
    {
        IEnumerable<string> Read(string filename);
        Calculation ProcessLine(string line);
    }
    public class FileParser : IFileParser
    {
        public Calculation ProcessLine(string line)
        {
            var tokens = line.Split(';');
            var inputA = double.Parse(tokens[1]);
            var inputB = double.Parse(tokens[2]);
            var inputC = double.Parse(tokens[3]);
            double result = CalculateResult(tokens[0], inputA, inputB, inputC);
           return new Calculation(tokens[0],inputA,inputB,inputC,result);

        }
        private double CalculateResult(string formula,double inputA,double inputB,double inputC)
        {
            var formular1regex = new Regex("A x B / C");
            var formular2regex = new Regex("A mod B x C");
            var formular3regex = new Regex("A^C - √B x C");

            if(formular1regex.IsMatch(formula))
            {
                return inputA * (inputB / inputC);
            }

            if (formular2regex.IsMatch(formula))
            {
                return inputA % (inputB * inputC);
            }

            if (formular3regex.IsMatch(formula))
            {
                return Math.Pow(inputA,inputC) - (Math.Sqrt(inputB)*inputC);
            }
            throw new FormatException("Could not parse the formula");
        }
      
        public IEnumerable<string> Read(string filename)
        {
            using (StreamReader sr = File.OpenText(filename))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    yield return line;
                }
            }
        }
    }
}
