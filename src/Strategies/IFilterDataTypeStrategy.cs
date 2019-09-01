using Fop.Filter;

namespace Fop.Strategies
{
    public interface IFilterDataTypeStrategy
    {
        string ConvertFilterToText(IFilter filter);
    }
}
