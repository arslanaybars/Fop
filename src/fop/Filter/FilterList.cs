using System.Collections.Generic;

namespace Fop.Filter
{
    public class FilterList : IFilterList
    {
        public FilterLogic Logic { get; set; }

        public IEnumerable<IFilter> Filters { get; set; }
    }
}
