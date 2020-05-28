using System.Collections.Generic;

namespace Common.Data
{
    public class Result<T>
    {
        public T Item { get; set; }
        public IEnumerable<T> Items { get; set; }
        public ErrorResult ErrorResult { get; set; }
    }
}