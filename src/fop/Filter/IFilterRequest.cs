using System.Collections.Generic;

namespace Fop.Filter
{
    public interface IFilterRequest
    {
        IEnumerable<IFilterList> FilterList { get; }
    }
}
