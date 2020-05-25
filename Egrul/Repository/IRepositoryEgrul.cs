using Egrul.Repository.Data;
using System.Collections.Generic;

namespace Egrul.Repository
{
    public interface IRepositoryEgrul
    {
        List<EntityCompany> GetCollectionCompany(string query);

        bool GetFile(string token, string fileName);
    }
}