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
            DataTypeStrategies.Add(FilterDataTypes.String, new StringDataTypeStrategy());
            DataTypeStrategies.Add(FilterDataTypes.Char, new CharDataTypeStrategy());
            DataTypeStrategies.Add(FilterDataTypes.DateTime, new DateTimeDataTypeStrategy());
        }

        public static (IQueryable<T>, int) ApplyFop<T>(this IQueryable<T> source, IFopRequest request)
        {
            int totalRowsAfterFiltering = 0;

            // Filter
            if (request.FilterList.Any())
            {
                var whereExpression = string.Empty;

                for (var i = 0; i < request.FilterList.Count(); i++)
                {
                    var filterList = request.FilterList.ToArray()[i];

                    whereExpression += GenerateDynamicWhereExpression(source, filterList);

                    if (i < request.FilterList.Count() - 1)
                    {
                        whereExpression += ConvertLogicSyntax(FilterLogic.And);
                    }
                }

                var interpreter = new Interpreter();
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

        private static string GenerateDynamicWhereExpression<T>(IQueryable<T> source, IFilterList filterList)
        {
            var dynamicExpressoBuilder = new StringBuilder();

            for (var i = 0; i < filterList.Filters.Count(); i++)
            {
                var filter = filterList.Filters.ToArray()[i];
                dynamicExpressoBuilder.Append(ConvertFilterToText(filter));

                if (i < filterList.Filters.Count() - 1)
                {
                    dynamicExpressoBuilder.Append(ConvertLogicSyntax(filterList.Logic));
                }
            }

            return "(" + dynamicExpressoBuilder + ")";
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
