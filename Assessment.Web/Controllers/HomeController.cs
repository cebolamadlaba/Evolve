using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Assessment.Web.Data;
using Assessment.Web.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NServiceBus;
using Assessment.Web.Views;
using System.Web;

namespace Assessment.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMessageSession messageSession;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly EvolveContext context;
        private readonly IViewCalculationHeaders _viewCalculationHeaders;
        private readonly IViewCalculationResults _viewCalculationResults;

        public HomeController(IMessageSession messageSession, IHostingEnvironment hostingEnvironment, EvolveContext context,
               IViewCalculationHeaders viewCalculationHeaders, IViewCalculationResults viewCalculationResults)
        {
            this.messageSession = messageSession;
            this.hostingEnvironment = hostingEnvironment;
            this.context = context;
            this._viewCalculationHeaders = viewCalculationHeaders;
            this._viewCalculationResults = viewCalculationResults;
        }
     
        [HttpPost]
        [Route("Home/Upload")]
        public async Task<IActionResult> Upload()
        {

            IFormFile  file = Request.Form.Files.First();

            if (file != null)
            {
                //Only one file 
                var path = Path.Combine(hostingEnvironment.WebRootPath, file.FileName);
                using (var stream = new FileStream(path,FileMode.Create))
                {
                    await file.CopyToAsync(stream);    
                }
                
                var header = new CalculationHeader{FileName = file.FileName, Status = "Pending" , Size = (int)file.Length , UploadTime = DateTime.Now };
                await context.CalculationHeaders.AddAsync(header);
                await context.SaveChangesAsync();

                await messageSession.SendLocal(new CalculationCommand { Filename = path,HeaderId = header.Id });
                return Accepted();
            }
            return BadRequest("No file uploaded");
            
        }

        [HttpGet]
        [Route("Home/GetExistingHeaders")]
        public IEnumerable<CalculationHeader> GetExistingHeaders()
        {
            return _viewCalculationHeaders.GetCalculationHeaders();
        }

        [HttpGet]
        [Route("Home/CalculationResultsById")]
        public IEnumerable<CalculationResult>CalculationResultsById(int headerId)
        {          
            return  _viewCalculationResults.GetCalculationResults(headerId);
        }

    }
}
