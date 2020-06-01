using Common.Data;
using Rosreestr.Repository.Data;
using System.Threading.Tasks;

namespace Rosreestr.Service.Interface
{
    public interface IFoundRosreestrService
    {
        Task<Result<EntityFoundRealEstate>> FoundRealEstate(string query);

        Task<Result<IRealEstate>> GetRealEstate(EntityFoundRealEstate estate);

        Task<Result<EntityRealEstate>> GetList(string file);

        Task<Result<bool>> ProcessList();
    }
}