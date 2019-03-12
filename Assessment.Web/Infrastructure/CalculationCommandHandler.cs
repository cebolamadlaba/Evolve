using Assessment.Web.Data;
using EFCore.BulkExtensions;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Assessment.Web.Infrastructure
{
    public class CalculationCommandHandler : IHandleMessages<CalculationCommand>
    {
        private readonly IFileParser parser;
        private readonly EvolveContext _context;

        public CalculationCommandHandler(IFileParser parser, EvolveContext context)
        {
            this.parser = parser;
            this._context = context;
        }
        public async Task Handle(CalculationCommand message, IMessageHandlerContext context)
        {
            var result =new List<Calculation>();
            var errors = new List<CalculationResult>{};
            parser.Read(message.Filename).AsParallel().ForAll( line =>
            {
                try
                {
                    result.Add(parser.ProcessLine(line));
                }
               
                catch (Exception ex)
                {
                    //add the line info
                    errors.Add(new CalculationResult()
                    {
                        Message = ex.Message,
                        CalculationHeaderId = message.HeaderId,
                        Formular = line.Split(';')[0]
                    });

                }
            });

            //Set Completed time
            var header = await _context.CalculationHeaders.FindAsync(message.HeaderId);
             header.CompletedTime = DateTime.Now;
             header.Status = "Processed";
             _context.Update(header);
             await _context.SaveChangesAsync();

            //Bulk perfoms better for large records
            _context.BulkInsert(result.Select(r => new CalculationResult { CalculationHeaderId = message.HeaderId,
                Formular = r.Formula,InputA = r.A , InputB =r.B,InputC = r.C , Result = r.Result
            }).ToList());

            if(errors.Any()){
            _context.BulkInsert(errors);
            } 

            
        }
    }
}
