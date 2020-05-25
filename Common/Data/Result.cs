using System.Collections.Generic;

namespace Common.Data
{
    public class Result<T>
    {
        public T Object { get; set; }
        public IEnumerable<T> Objects { get; set; }
        public ErrorResult ErrorResult { get; set; }
    }
}