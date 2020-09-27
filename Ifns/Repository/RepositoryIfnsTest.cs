using Ifns.Data;
using Ifns.Service;
using System.Collections.Generic;
using System.IO;

namespace Ifns.Repository
{
    public class RepositoryIfnsTest : IRepositoryIfns
    {
        private IfnsParser _parser = new IfnsParser();

        public EntityIfns GetEntityIfns(Municipality mun, Inspection insp)
        {
            var ifns = _parser.ParsEntityIfns(File.ReadAllText("testCollectionIfns.txt"));
            return ifns;
        }

        public List<Municipality> GetMunicipalities(Inspection insp)
        {
            var mun = _parser.ParsMunicipalities(File.ReadAllText("testCollectionMun.txt"));
            return mun;
        }

        public List<Region> GetRegions()
        {
            var region = _parser.ParsRegion(File.ReadAllText("testCollectionRegion.txt"));
            return region;
        }
    }
}