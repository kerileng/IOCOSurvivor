using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SurvivorApi.Controllers
{
    public class RESOURCESController : ApiController
    {
        /// <summary>
        /// Add survicors supply
        /// </summary>
        /// <param name="resources">anything that can be of aid to the health of survivor, it can be 1 or more</param>
        public void Add(List<RESOURCE> resources)
        {
            using(var entity = new SurvivorEntities())
            {
                entity.RESOURCES.AddRange(resources);
                entity.SaveChanges();
            }
        }
    }
}
