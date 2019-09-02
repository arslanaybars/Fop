using System.Collections.Generic;
using Fop.Filter;
using Fop.Order;

namespace Fop
{
    public class FopRequest : IFopRequest
    {
        public IEnumerable<IFilterList> FilterList { get; set; }

        public string OrderBy { get; set; }

        public OrderDirection Direction { get; set; }

        public int PageNumber { get; set; } 

        public int PageSize { get; set; }
    }
}
