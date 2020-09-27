using System.Collections.Generic;

namespace Ifns.Data
{
    public class Region
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Inspection> Inspections { get; set; }
    }
}