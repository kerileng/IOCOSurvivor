using SurvivorApi.Models.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SurvivorApi.Controllers
{
    public class ROBOTSController : ApiController
    {
        [HttpGet]
        public List<ROBOT> LandBots()
        {
            Reports repo = new Reports();

            return repo.LandRobots();
        }

        [HttpGet]
        [Route("ROBOTS/SkyBots")]
        public List<ROBOT> SkyBots()
        {
            Reports repo = new Reports();

            return repo.SkyRobots();
        }
    }
}
