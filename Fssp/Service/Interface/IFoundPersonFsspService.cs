using Common.Data;
using Fssp.Data;
using System.Threading.Tasks;

namespace Fssp.Service.Interface
{
    public interface IFoundPersonFsspService
    {
        Task<Result<RequestFoundPerson>> GetPerson(FoundPerson person);
        Task<Result<RequestFoundPerson>> GetCompany(FoundCompany company);
        Task<Result<RequestFoundPerson>> GetNumber(string number);
        void GetResult(RequestFoundPerson request);
        void GetPersonFile(string file);      
    }
}