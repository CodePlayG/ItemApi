using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using PersonalAssistance.Enum;
using PersonalAssistance.Model;

namespace PersonalAssistance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntryController : ControllerBase
    {
        private static List<Entry> _entries = new List<Entry>()
        {
            new Entry(){Id=1,Description = "ABC",Amount = 400,DueDate = DateTime.Now.Date, Status =Status.UnPaid},
            new Entry(){Id=2,Description ="Xyz",Amount=500, DueDate=new DateTime(2020,05,25),Status = Status.Paid},
            new Entry(){Id=3,Description="LIC", Amount=15000,DueDate =new DateTime(2020,06,25),Status =Status.UnPaid}
        };


        [HttpGet]
        public IEnumerable<Entry> GetValues()
        {
            return _entries;
        }

        [HttpPost]
        public Response AddEntry([FromBody]Entry entry)
        {
            Response response = new Response();
            int countBefore = _entries.Count;
            _entries.Add(entry);

            if (_entries.Count.Equals(countBefore + 1))
            {
                response.Message = "Successfully Added.";
            }

            return response;

        }

        [HttpPut("{id}")]
        //[Route("/id")]
        public void UpdateEntry(int id, [FromBody]Entry entry)
        {
            _entries[id] = entry;
        }

        [HttpDelete("{id}")]
        public void RemoveEntry(int id)
        {
            _entries.RemoveAt(id);
        }
    }

   
}