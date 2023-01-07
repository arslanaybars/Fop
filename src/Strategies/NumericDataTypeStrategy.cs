using Fop.Exceptions;
using Fop.Filter;

namespace Fop.Strategies;

/// <summary>
/// All numeric behave the same
/// </summary>
public abstract  class NumericDataTypeStrategy : IFilterDataTypeStrategy
{
    public virtual string ConvertFilterToText(IFilter filter)
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
                return $"{filter.Key}.ToString().Contains(\"{filter.Value}\")";

            case FilterOperators.NotContains:
                return $"!{filter.Key}.ToString().Contains(\"{filter.Value}\")";

            case FilterOperators.StartsWith:
                return $"{filter.Key}.ToString().StartsWith(\"{filter.Value}\")";

            case FilterOperators.NotStartsWith:
                return $"!{filter.Key}.ToString().StartsWith(\"{filter.Value}\")";

            case FilterOperators.EndsWith:
                return $"{filter.Key}.ToString().EndsWith(\"{filter.Value}\")";

            case FilterOperators.NotEndsWith:
                return $"!{filter.Key}.ToString().EndsWith(\"{filter.Value}\")";

            default:
                throw new IntDataTypeNotSupportedException($"String filter does not support {filter.Operator}");
        }
    }
}