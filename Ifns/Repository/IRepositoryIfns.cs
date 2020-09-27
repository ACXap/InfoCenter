using Ifns.Data;
using System.Collections.Generic;

namespace Ifns.Repository
{
    public interface IRepositoryIfns
    {
        List<Region> GetRegions();
        List<Municipality> GetMunicipalities(Inspection insp);
        EntityIfns GetEntityIfns(Municipality mun, Inspection insp);
    }
}