using Rosreestr.Repository.Data;
using System.Collections.Generic;

namespace Rosreestr.Repository
{
    public interface IRepositoryRosreestr
    {
        List<EntityFoundRealEstate> FoundRealEstate(string query);
        IRealEstate GetRealEstate(string query);
    }
}