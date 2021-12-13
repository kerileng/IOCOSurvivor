using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using SurvivorApi.Models.Reports;
namespace SurvivorApi.Controllers
{
    public class SURVIVORSController : ApiController
    {
        /// <summary>
        /// Save survivors details in the database
        /// </summary>
        /// <param name="survivor">Full details about survivor</param>
        [HttpPost]
        ///
        public void Add(SURVIVOR survivor)
        {
            using (var entity = new SurvivorEntities())
            {
                entity.SURVIVORS.Add(survivor);

                entity.SaveChanges();
            }
        }

        /// <summary>
        /// Update Survivors location, using survivor Idnumber
        /// </summary>
        /// <param name="idnum">ID number to be used for searching survivor</param>
        /// <param name="lati">new latitude coordinates for survivor</param>
        /// <param name="longi">new longitude coordinates for survivor</param>
        /// <returns>return saved user details</returns>

        [HttpPost]
        public HttpResponseMessage Update(string idnum, double lati, double longi)
        {
            string message = default;

            using (var entity = new SurvivorEntities())
            {
                SURVIVOR survivor = entity.SURVIVORS.Where(x => x.IDNumber.Equals(idnum)).FirstOrDefault();
                survivor.Latitude = (decimal)lati;
                survivor.Longitude = (decimal)longi;

                entity.SURVIVORS.Attach(survivor);
                entity.Entry(survivor).Property(x => x.Latitude).IsModified = true;
                entity.Entry(survivor).Property(x => x.Longitude).IsModified = true;
                entity.SaveChanges();

                message = $"{survivor.Name} , with id number:  {survivor.IDNumber} location was successfully updated";
            }

            return Request.CreateResponse(HttpStatusCode.OK, message);
        }

        /// <summary>
        /// Report survivor who is contaminated, after third report Mark survivor as infected()
        /// </summary>
        /// <param name="infectedsurvivorid">survivor who is reported to be contaminated</param>
        /// <param name="reporter">survivor reporting the other survivor</param>
        /// <returns></returns>
        [HttpPost]
        
        public IHttpActionResult ReportSurvivor(string infectedsurvivorid)
        {
            string message = default;
            using (var entity = new SurvivorEntities())
            {
                SURVIVOR reportedSurvi = (SURVIVOR)entity.SURVIVORS.Where(x => x.IDNumber.Equals(infectedsurvivorid)).FirstOrDefault();

                if (reportedSurvi.Reported >= 3)
                {
                    message = $" {reportedSurvi.Name} , with idnumber: {reportedSurvi.IDNumber} as infected";
                }
                else if (reportedSurvi.Reported == 2)
                {
                    message = FlagInfected(reportedSurvi.IDNumber);
                }

                reportedSurvi.Reported++;
                entity.SURVIVORS.Attach(reportedSurvi);
                entity.Entry(reportedSurvi).Property(x => x.Reported).IsModified = true;

                entity.SaveChanges();

                message = $" {reportedSurvi.Name} , with idnumber: {reportedSurvi.IDNumber} was successfully reported";
            }
            return Ok(message);
        }
        /// <summary>
        /// Flag survivor as infected after been reported 3 times
        /// </summary>
        /// <param name="infectSurv">idnumber of infected survivor</param>
        /// <returns>message which include the details of survivor </returns>
        [NonAction]
        public string FlagInfected(string infectSurv)
        {
            string message = default;
            using (var entity = new SurvivorEntities())
            {
                SURVIVOR infected = (SURVIVOR)entity.SURVIVORS.Where(x => x.IDNumber.Equals(infectSurv)).FirstOrDefault();
                infected.Infected = true;

                entity.SURVIVORS.Attach(infected);
                entity.Entry(infected).Property(x => x.Infected).IsModified = true;

                entity.SaveChanges();

                message = $"Survivor {infected.Name} ,with id number:  {infected.IDNumber} is infected";
            }

            return message;
        }

        /// <summary>
        /// The method return all infected survivors from the Report class
        /// </summary>
        /// <param name="descr"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Survivors/Infected")]
        public List<SURVIVOR> Infected()
        {
            Reports repo = new Reports();

            return repo.Infected();
        }

        [HttpGet]
        [Route("Survivors/NonInfected")]
        public List<SURVIVOR> NonInfected()
        {
            Reports repo = new Reports();

            return repo.NonInfected();
        }
    }

}

