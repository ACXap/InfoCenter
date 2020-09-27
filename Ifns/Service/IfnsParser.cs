using Ifns.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace Ifns.Service
{
    public class IfnsParser
    {
        public List<Region> ParsRegion(string data)
        {
            List<Region> result = new List<Region>();

            var stopString = "var BIG_TREE = false;";
            var startString = "var TREE = [\"ROOT\",\"SOUN_ADDRNO_UL\",0,";

            var startIndex = data.IndexOf(startString);
            var stopIndex = data.IndexOf(stopString);

            var resultString = data.Substring(startIndex + startString.Length, stopIndex - startIndex - startString.Length - 12);

            JArray obj = (JArray)JsonConvert.DeserializeObject(resultString);

            foreach (var reg in obj)
            {
                Region region = new Region();
                region.Id = reg.Children().ElementAt(0).ToString();
                region.Name = reg.Children().ElementAt(1).ToString();
                region.Inspections = new List<Inspection>();

                // Ненецкий автономный круг у него нет инспекций
                if (reg.Children().Count() == 3) continue;

                JArray col = (JArray)reg.Children().ElementAt(3);
                foreach (var insp in col)
                {
                    Inspection inspection = new Inspection();
                    inspection.Id = insp.Children().ElementAt(0).ToString();
                    inspection.Name = insp.Children().ElementAt(1).ToString();
                    region.Inspections.Add(inspection);
                }

                result.Add(region);
            }

            return result;
        }

        public List<Municipality> ParsMunicipalities(string data)
        {
            List<Municipality> result = new List<Municipality>();

            var collection = JsonConvert.DeserializeObject<CollectionMun>(data);

            foreach (var colMun in collection.OktmmfList)
            {
                result.Add(new Municipality()
                {
                    Id = colMun.Key,
                    Name = colMun.Value
                });
            }

            return result;
        }

        public EntityIfns ParsEntityIfns(string data)
        {
            EntityIfns result;

            result = JsonConvert.DeserializeObject<EntityIfns>(data);

            return result;
        }
    }
}