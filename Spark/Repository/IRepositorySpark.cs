using Spark.Repository.Data;
using System.Collections.Generic;

namespace Spark.Repository
{
    public interface IRepositorySpark
    {
        List<EntityCompanyInfo> GetCollectionCompany(string query);

        EntityCompany GetCompany(string quert);
    }
}