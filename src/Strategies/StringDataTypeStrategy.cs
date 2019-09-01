using Fop.Exceptions;
using Fop.Filter;

namespace Fop.Strategies
{
    public class StringDataTypeStrategy : IFilterDataTypeStrategy
    {
        public string ConvertFilterToText(IFilter filter)
        {
            switch (filter.Operator)
            {
                case FilterOperators.Equal:
                    return filter.Key + " == \"" + filter.Value + "\"";
                case FilterOperators.NotEqual:
                    return filter.Key + " != \"" + filter.Value + "\"";
                case FilterOperators.Contains:
                    return filter.Key + ".Contains(\"" + filter.Value + "\")";
                case FilterOperators.NotContains:
                    return "!" + filter.Key + ".Contains(\"" + filter.Value + "\")";
                case FilterOperators.StartsWith:
                    return filter.Key + ".StartsWith(\"" + filter.Value + "\")";
                case FilterOperators.NotStartsWith:
                    return "!" + filter.Key + ".StartsWith(\"" + filter.Value + "\")";
                case FilterOperators.EndsWith:
                    return filter.Key + ".EndsWith(\"" + filter.Value + "\")";
                case FilterOperators.NotEndsWith:
                    return "!" + filter.Key + ".EndsWith(\"" + filter.Value + "\")";
                case FilterOperators.GreaterThan:
                case FilterOperators.GreaterOrEqualThan:
                case FilterOperators.LessThan:
                case FilterOperators.LessOrEqualThan:
                default:
                    throw new StringDataTypeNotSupportedException($"String filter does not support {filter.Operator}");
            }
        }
    }
}
