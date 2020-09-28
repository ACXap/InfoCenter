using Ifns.Data;
using System.Collections.Generic;
using System.Linq;

namespace Ifns.Service
{
    public static class ServiceConverter
    {
        public static IEnumerable<Municipality> ConvertStringToMun(IEnumerable<string> str)
        {
            return str.Select(x =>
            {
                return new Municipality() { Id = x };
            });
        }

        public static IEnumerable<Inspection> ConvertStringToIfns(IEnumerable<string> str)
        {
            return str.Select(x =>
            {
                return new Inspection() { Id = x };
            });
        }
    }
}