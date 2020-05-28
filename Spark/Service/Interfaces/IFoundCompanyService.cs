using Common.Data;
using Spark.Data.Model;
using System.Threading.Tasks;

namespace Spark.Service
{
    public interface IFoundCompanySparkService
    {
        Task<Result<CompanyInfo>> FoundCompany(string query);
        Task<Result<bool>> GetFileCompany(CompanyInfo company);
    }
}