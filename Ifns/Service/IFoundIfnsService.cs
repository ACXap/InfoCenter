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
    }
}