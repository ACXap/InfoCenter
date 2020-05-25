using Common.Data;
using Egrul.Data.Model;
using System.Threading.Tasks;

namespace Egrul.Service.Interfaces
{
    public interface IFoundCompanyEgrulService
    {
        Task<Result<CompanyInfo>> GetCollectionCompany(string query);

        Task<Result<bool>> GetPdfFile(CompanyInfo company);
        
    }
}