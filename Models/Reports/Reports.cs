using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurvivorApi.Models.Reports
{
    public class Reports
    {
        public List<SURVIVOR> Infected()
        {
            List<SURVIVOR> infectionList = new List<SURVIVOR>();
            using (var entity = new SurvivorEntities())
            {
                 var cont = entity.SURVIVORS.Where(x => x.Infected == true).ToList();

                    foreach (var con in cont)
                    {
                        SURVIVOR surv = con;
                        infectionList.Add(surv);
                    }
            }

            return infectionList;
        }
        public List<SURVIVOR> NonInfected()
        { 
           List<SURVIVOR> noninfectionList = new List<SURVIVOR>();
           using (var entity = new SurvivorEntities())
            {
               var cont = entity.SURVIVORS.Where(x=>x.Infected == false).ToList();

                    foreach (var con in cont)
                    {
                        SURVIVOR surv = con;
                    noninfectionList.Add(surv);
                    }
           }

            return noninfectionList;
        }

        public List<ROBOT> LandRobots()
        {
            List<ROBOT> landrobots = new List<ROBOT>();
            using (var entity = new SurvivorEntities())
            {
                var cont = entity.ROBOTS.Where(x => x.category.Equals("land")).ToList();

                foreach (var con in cont)
                {
                    ROBOT surv = con;
                    landrobots.Add(surv);
                }
            }

            return landrobots;
        }

        public List<ROBOT> SkyRobots()
        {
            List<ROBOT> flyingrobots = new List<ROBOT>();
            using (var entity = new SurvivorEntities())
            {
                var cont = entity.ROBOTS.Where(x => x.category.Equals("fly")).ToList();

                foreach (var con in cont)
                {
                    ROBOT surv = con;
                    flyingrobots.Add(surv);
                }
            }

            return flyingrobots;
        }

    }
}
