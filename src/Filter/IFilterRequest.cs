namespace Fop.Filter;

public interface IFilterRequest
{
    IEnumerable<IFilterList> FilterList { get; }
}