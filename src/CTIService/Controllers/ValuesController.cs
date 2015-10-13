using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using log4net;
using ECS.Common.Logging;
using System.Reflection;

namespace CTIService.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        protected static readonly ILog Log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            Log.Info(new LogObject("EventName", "ValuesController Get"));

            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
