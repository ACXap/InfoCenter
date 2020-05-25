using Fssp.Repository.Data;
using System.Collections;
using System.Collections.Generic;

namespace Fssp.Service.Interface
{
    public interface IConvertResultToFile
    {
        string ConvertResult(IEnumerable<EntityResult> result, string fileName);
    }
}