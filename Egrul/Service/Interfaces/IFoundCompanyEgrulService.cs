using Common.Data;
using Egrul.Data.Model;
using System.Threading.Tasks;

namespace Egrul.Service
{
    public interface IFoundCompanyEgrulService
    {
        Task<Result<CompanyInfo>> FoundCompany(string query);

        Task<Result<bool>> GetPdfFile(CompanyInfo company);
        
    }
}