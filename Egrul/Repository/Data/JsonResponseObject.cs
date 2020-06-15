using System.Collections.Generic;

namespace Egrul.Repository.Data
{
    public class FoundCompany
    {
        public string status { get; set; }
        public List<EntityCompany> rows { get; set; }
    }
}