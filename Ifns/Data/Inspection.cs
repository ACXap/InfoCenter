using System.Collections.Generic;

namespace Ifns.Data
{
    public class Inspection
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Municipality> Municipalities { get; set; }
    }
}