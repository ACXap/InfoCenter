using Common.Data;
using Fssp.Data;
using System.Threading.Tasks;

namespace Fssp.Service.Interface
{
    public interface IFoundPersonFsspService
    {
        Task<Result<RequestFoundPerson>> GetPerson(FoundPerson person);
        void GetPersonFile(string file);      
    }
}