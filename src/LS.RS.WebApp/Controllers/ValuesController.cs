using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading.Tasks;
using LC.RS.Synchronization.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Services.Remoting.Client;

namespace LS.RS.WebApp.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly FabricClient fabricClient;

        public ValuesController(FabricClient fabricClient)
        {
            this.fabricClient = fabricClient;
        }

        // GET api/values
        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            var serviceUri = new Uri("fabric:/LS.RS.Fabric/LC.RS.Synchronization");
            var proxy = ServiceProxy.Create<ISynchronization>(serviceUri);
            var r = await proxy.Synchronize();

            return new string[] { "value1", "value2", r.ToString() };
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
