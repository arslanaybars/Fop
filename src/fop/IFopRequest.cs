using Fop.Filter;
using Fop.Order;
using Fop.Page;

namespace Fop
{
    public interface IFopRequest : IFilterRequest, IOrderRequest, IPageRequest
    {
    }
}
