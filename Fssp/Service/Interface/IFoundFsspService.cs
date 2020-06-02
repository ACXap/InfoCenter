using Common.Data;
using Fssp.Data;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Fssp.Service
{
    public interface IFoundFsspService
    {
        ObservableCollection<RequestFound> CollectionRequest { get; }

        Task GetPerson(FoundPerson person);
        Task GetCompany(FoundCompany company);
        Task GetNumber(string number);
        void GetPersonFile(string file);
        Task<Result<RequestFound>> ProcessingList(string file);
    }
}