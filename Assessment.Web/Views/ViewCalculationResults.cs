using Assessment.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assessment.Web.Views
{
    public interface IViewCalculationResults
    {
       IEnumerable<CalculationResult> GetCalculationResults(int headerId);
    }

    public class ViewCalculationResults : IViewCalculationResults
    {
        private readonly EvolveContext _context;

        public ViewCalculationResults(EvolveContext context)
        {
            this._context = context;
        }

        public IEnumerable<CalculationResult> GetCalculationResults(int headerId)
        {
            return this._context.CalculationResults.Where(x=>x.CalculationHeaderId== headerId)
                                                   .ToList();
        } 
    }
}
