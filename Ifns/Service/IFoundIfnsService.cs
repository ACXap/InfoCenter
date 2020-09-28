using Common.Data;
using Ifns.Data;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Ifns.Service
{
    public interface IFoundIfnsService
    {
        ObservableCollection<EntityIfns> CollectionIfns { get; }
        Task<Result<bool>> GetAll();
        Result<bool> SaveFile(string name = null);
        Task<Result<bool>> ProcessList(string file);
    }
}