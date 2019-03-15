using Assessment.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assessment.Web.Views
{
    public interface IViewCalculationHeaders
    {
       IEnumerable<CalculationHeader> GetCalculationHeaders();
    }

    public class ViewCalculationHeaders : IViewCalculationHeaders
    {
        private readonly EvolveContext _context;

        public ViewCalculationHeaders(EvolveContext context)
        {
           this._context = context;
        }

        public IEnumerable<CalculationHeader> GetCalculationHeaders()
        {
            return this._context.CalculationHeaders.ToList();
        }
    }
}
