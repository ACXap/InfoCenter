using Egrul.Data.Model;
using System.Collections.Generic;

namespace Egrul.Data.DesignTime
{
    public class CollectionCompanyInfoDesignTime
    {
        public List<CompanyInfo> CollectionCompanyInfo { get; } = new List<CompanyInfo>();

        public CompanyInfo CurrentCompany { get; } = new CompanyInfo();
    }
}