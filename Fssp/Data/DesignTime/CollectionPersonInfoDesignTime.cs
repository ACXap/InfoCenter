using Fssp.Data.Model;
using GalaSoft.MvvmLight;
using System.Collections.Generic;

namespace Fssp.Data.DesignTime
{
    public class CollectionPersonInfoDesignTime:ViewModelBase
    {
        public List<RequestFoundPerson> CollectionRequest { get; set; }
    }
}