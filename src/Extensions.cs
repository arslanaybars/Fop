using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using DynamicExpresso;
using Fop.Filter;
using Fop.Strategies;

namespace Fop
{
    public static class Extensions
    {
        private static readonly Dictionary<FilterDataTypes, IFilterDataTypeStrategy> DataTypeStrategies =
            new Dictionary<FilterDataTypes, IFilterDataTypeStrategy>();

        static Extensions()
        {
            DataTypeStrategies.Add(FilterDataTypes.Int, new IntDataTypeStrategy());
            DataTypeStrategies.Add(FilterDataTypes.Float, new FloatDataTypeStrategy());
            DataTypeStrategies.Add(FilterDataTypes.Double, new DoubleDataTypeStrategy());
            DataTypeStrategies.Add(FilterDataTypes.Long, new LongDataTypeStrategy());
            DataTypeStrategies.Add(FilterDataTypes.Decimal, new DecimalDataTypeStrategy());
            DataTypeStrategies.Add(FilterDataTypes.String, new StringDataTypeStrategy());
            DataTypeStrategies.Add(FilterDataTypes.Char, new CharDataTypeStrategy());
            DataTypeStrategies.Add(FilterDataTypes.DateTime, new DateTimeDataTypeStrategy());
            DataTypeStrategies.Add(FilterDataTypes.Boolean, new BooleanDataTypeStrategy());
            DataTypeStrategies.Add(FilterDataTypes.Enum, new EnumDataTypeStrategy());
            DataTypeStrategies.Add(FilterDataTypes.Guid, new GuidTypeStrategy());
        }

        public static (IQueryable<T>, int) ApplyFop<T>(this IQueryable<T> source, IFopRequest request)
        {
            int totalRowsAfterFiltering = source.Count();

            // Filter
            if (request.FilterList != null && request.FilterList.Any())
            {
                var whereExpression = string.Empty;
                var enumTypes = new List<KeyValuePair<string, string>>();

                for (var i = 0; i < request.FilterList.Count(); i++)
                {
                    var filterList = request.FilterList.ToArray()[i];

                    (string generatedWhereExpression, List<KeyValuePair<string, string>> generatedEnumTypes) = GenerateDynamicWhereExpression(source, filterList);
                    whereExpression += generatedWhereExpression;
                    enumTypes.AddRange(generatedEnumTypes);

                    if (i < request.FilterList.Count() - 1)
                    {
                        whereExpression += ConvertLogicSyntax(FilterLogic.Or);
                    }
                }

                var interpreter = new Interpreter().EnableAssignment(AssignmentOperators.None);
                foreach (var enumType in enumTypes)
                {
                    var t = Type.GetType($"{enumType.Key}, {enumType.Value}");
                    interpreter.Reference(t);
                }
                var expression = interpreter.ParseAsExpression<Func<T, bool>>(whereExpression, typeof(T).Name);
                source = source.Where(expression);

                totalRowsAfterFiltering = source.Count();
            }

            // Order
            if (!string.IsNullOrEmpty(request.OrderBy))
            {
                source = source.OrderBy(request.OrderBy + " " + request.Direction);
            }

            // Paging
            if (request.PageNumber > 0 && request.PageSize > 0)
            {
                source = source.Skip(request.PageSize * (request.PageNumber - 1)).Take(request.PageSize);
            }

            return (source, totalRowsAfterFiltering);
        }

        #region Helpers

        private static (string, List<KeyValuePair<string, string>>) GenerateDynamicWhereExpression<T>(IQueryable<T> source, IFilterList filterList)
        {
            var dynamicExpressoBuilder = new StringBuilder();
            var kvp = new List<KeyValuePair<string, string>>();

            for (var i = 0; i < filterList.Filters.Count(); i++)
            {
                var aa = dynamicExpressoBuilder.ToString();
                var filter = filterList.Filters.ToArray()[i];

                if (filter.DataType == FilterDataTypes.Enum)
                {
                    kvp.Add(new KeyValuePair<string, string>(filter.Fullname, filter.Assembly));
                }

                dynamicExpressoBuilder.Append(ConvertFilterToText(filter));
                var bb = dynamicExpressoBuilder.ToString();
                if (i < filterList.Filters.Count() - 1)
                {
                    dynamicExpressoBuilder.Append(ConvertLogicSyntax(filterList.Logic));
                    var cc = dynamicExpressoBuilder.ToString();
                }
            }

            var dd = "(" + dynamicExpressoBuilder + ")";
            return ("(" + dynamicExpressoBuilder + ")", kvp);
        }

        private static string ConvertLogicSyntax(FilterLogic filterListLogic)
        {
            switch (filterListLogic)
            {
                case FilterLogic.And:
                    return " && ";
                case FilterLogic.Or:
                    return " || ";
            }

            throw new ArgumentOutOfRangeException(nameof(filterListLogic), filterListLogic, null);
        }

        public static string ConvertFilterToText(IFilter filter)
        {
            return DataTypeStrategies[filter.DataType].ConvertFilterToText(filter);
        }

        #endregion
    }
}
