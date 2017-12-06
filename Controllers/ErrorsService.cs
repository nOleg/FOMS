using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DMZ_Page.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {   
        // POST api/values
        [HttpPost]
        public void Post([FromBody]object err)
        {
            var vr=JsonConvert.DeserializeObject<dynamic>(err.ToString());
            HomeController.errLst.Add(new SrvError(){Message=vr.err.ToString(),TimeStamp=DateTime.Now});
        }
    }

    public class SrvError
    {
        public int ID{get;set;}
        public string Message{get;set;}
        public DateTime TimeStamp{get;set;}
    }
}
