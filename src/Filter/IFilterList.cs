namespace Fop.Filter;

public interface IFilterList
{
    FilterLogic Logic { get; set; }

    IEnumerable<IFilter> Filters { get; set; }
}