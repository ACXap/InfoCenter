using Common.Data;
using Spark.Data.Model;
using System.Threading.Tasks;

namespace Spark.Service.Interfaces
{
    public interface ILoadCompanyService
    {
        Task<Result<Company>> GetCompany(string query);
    }
}