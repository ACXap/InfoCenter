using Spark.Repository.Data;
using System.Collections.Generic;

namespace Spark.Repository
{
    public interface IRepositorySpark
    {
        List<EntityCompanyInfo> FoundCompany(string query);

        EntityCompany GetCompany(string quert);
    }
}