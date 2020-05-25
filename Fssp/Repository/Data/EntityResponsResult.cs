using System.Collections.Generic;

namespace Fssp.Repository.Data
{
    public class EntityResponsResult
    {
        public string Exception { get; set; }
        public int Code { get; set; }
        public List<EntityQuery> CollectionQuery { get; set; }
    }

    public class EntityQuery
    {
        public int Status { get; set; }
        public int Type { get; set; }
        public string Name { get; set; }
        public int Region { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Number { get; set; }

        public List<EntityResult> CollectionResult { get; set; }
    }
}