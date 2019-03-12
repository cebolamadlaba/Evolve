using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assessment.Web.Infrastructure
{
    public class CalculationCommand : IMessage
    {
        public int HeaderId { get; set; }
        public string Filename { get; set; }
    }
}
