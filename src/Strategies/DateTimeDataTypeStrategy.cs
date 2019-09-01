using System;
using System.Globalization;
using Fop.Exceptions;
using Fop.Filter;

namespace Fop.Strategies
{
    public class DateTimeDataTypeStrategy : IFilterDataTypeStrategy
    {
        public string ConvertFilterToText(IFilter filter)
        {
            switch (filter.Operator)
            {
                case FilterOperators.Equal:
                    return filter.Key + " == Convert.ToDateTime(\"" + filter.Value + "\")";
                case FilterOperators.NotEqual:
                    return filter.Key + " != Convert.ToDateTime(\"" + filter.Value + "\")";
                case FilterOperators.GreaterThan:
                    return filter.Key + " > Convert.ToDateTime(\"" + filter.Value + "\")";
                case FilterOperators.GreaterOrEqualThan:
                    return filter.Key + " >= Convert.ToDateTime(\"" + filter.Value + "\")";
                case FilterOperators.LessThan:
                    return filter.Key + " < Convert.ToDateTime(\"" + filter.Value + "\")";
                case FilterOperators.LessOrEqualThan:
                    return filter.Key + " <= Convert.ToDateTime(\"" + filter.Value + "\")";
                case FilterOperators.Contains:
                case FilterOperators.NotContains:
                case FilterOperators.StartsWith:
                case FilterOperators.NotStartsWith:
                case FilterOperators.EndsWith:
                case FilterOperators.NotEndsWith:
                default:
                    throw new DateTimeDataTypeNotSupportedException($"String filter does not support {filter.Operator}");

            }
        }
    }
}
