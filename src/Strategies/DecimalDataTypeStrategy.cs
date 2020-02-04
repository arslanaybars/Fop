using Fop.Exceptions;
using Fop.Filter;

namespace Fop.Strategies
{
    public class DecimalDataTypeStrategy : IFilterDataTypeStrategy
    {
        public string ConvertFilterToText(IFilter filter)
        {
            switch (filter.Operator)
            {
                case FilterOperators.Equal:
                    return filter.Key + " == " + filter.Value;
                case FilterOperators.NotEqual:
                    return filter.Key + " != " + filter.Value;
                case FilterOperators.GreaterThan:
                    return filter.Key + " > " + filter.Value;
                case FilterOperators.GreaterOrEqualThan:
                    return filter.Key + " >= " + filter.Value;
                case FilterOperators.LessThan:
                    return filter.Key + " < " + filter.Value;
                case FilterOperators.LessOrEqualThan:
                    return filter.Key + " <= " + filter.Value;
                case FilterOperators.Contains:
                case FilterOperators.NotContains:
                case FilterOperators.StartsWith:
                case FilterOperators.NotStartsWith:
                case FilterOperators.EndsWith:
                case FilterOperators.NotEndsWith:
                default:
                    throw new DecimalDataTypeNotSupportedException($"String filter does not support {filter.Operator}");
            }
        }
    }
}
