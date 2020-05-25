using Common.Data;
using Spark.Data.Model;
using System.Threading.Tasks;

namespace Spark.Service.Interfaces
{
    public interface IFoundCompanySparkService
    {
        Task<Result<CompanyInfo>> GetCollectionCompany(string query);
        Task<Result<bool>> GetPdfFile(CompanyInfo company);
    }
}