using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Fop.Exceptions;
using Fop.Filter;
using Fop.Order;
using Fop.Utils;

namespace Fop.FopExpression
{
    public class FopExpressionBuilder<T>
    {
        public static IFopRequest Build(string filter, string order, int pageNumber, int pageSize)
        {
            var request = new FopRequest();
            if (!string.IsNullOrEmpty(filter))
            {
                request.FilterList = FilterExpressionBuilder(filter);
            }

            if (!string.IsNullOrEmpty(order))
            {
                var (orderBy, direction) = OrderExpressionBuilder(order);
                request.OrderBy = orderBy;
                request.Direction = direction;
            }

            if (pageNumber > 0 && pageSize > 0)
            {
                request.PageNumber = pageNumber;
                request.PageSize = pageSize;
            }

            return request;
        }

        private static (string, OrderDirection) OrderExpressionBuilder(string order)
        {
            order = order.ToLower();

            var orderParts = order.Split(';');

            var direction = orderParts.Length > 1 ?
                orderParts[1] == "desc" 
                    ? OrderDirection.Desc 
                    : OrderDirection.Asc
                : OrderDirection.Asc;

            return (orderParts[0], direction);
        }

        private static IEnumerable<IFilterList> FilterExpressionBuilder(string filter)
        {
            //filter = filter.ToLower();
            var genericType = typeof(T);
            var genericTypeName = genericType.Name;
            var multipleLogicParts = filter.Split('$');
            var filterList = new IFilterList[multipleLogicParts.Length];

            for (var i = 0; i < multipleLogicParts.Length; i++)
            {
                var multipleLogicPart = multipleLogicParts[i];
                var filterLogicParts = multipleLogicPart.Split(';');

                var logicOperator = filterLogicParts[filterLogicParts.Length - 1];
                var (filterLogicPartLength, logicOperatorEnum) = FilterLogicPartLength(logicOperator, filterLogicParts);


                filterList[i] = new FilterList
                {
                    Filters = new Filter.Filter[filterLogicPartLength],
                    Logic = logicOperatorEnum
                };

                for (var j = 0; j < filterLogicPartLength; j++)
                {
                    var filterLogicPart = filterLogicParts[j];

                    var (key, value) = Operator.Dictionary.FirstOrDefault(x => filterLogicPart.Contains(x.Key));

                    if (key == null)
                    {
                        throw new FilterOperatorNotFoundException(
                            $"{filterLogicPart} is not found in our Operator.Dictionary");
                    }

                    var filterObject = filterLogicPart.Split(key);

                    // var property = genericProperties.FirstOrDefault(x => x.Name.ToLower() == filterObject[0]);
                    var propertyInfos = new List<PropertyInfo>();
                    var property = GetPropertyValue(genericType, filterObject[0], propertyInfos);
                    var lastProperty = property.LastOrDefault();
                    ((Filter.Filter[])filterList[i].Filters)[j] = new Filter.Filter
                    {
                        Operator = value,
                        DataType = GetFilterDataTypes(lastProperty),
                        Key = genericTypeName + "." + property.Select(x => x.Name).Aggregate((a, b) => a + "." + b),
                        Value = filterObject[1],
                        Assembly = lastProperty?.Module.Name.Replace(".dll", string.Empty),
                        Fullname = lastProperty?.PropertyType.FullName
                    };
                }
            }

            return filterList;
        }

        public static List<PropertyInfo> GetPropertyValue(Type baseType, string propertyName, List<PropertyInfo> propertyInfos)
        {
            var parts = propertyName.Split('.');

            if (parts.Length > 1)
            {

                var propertyInfo = baseType.GetProperty(parts[0],
                    BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                var propName = parts.Skip(1).Aggregate((a, i) => a + "." + i);
                propertyInfos.Add(propertyInfo);
                return GetPropertyValue(propertyInfo?.PropertyType, propName, propertyInfos);

                //return GetPropertyValue(baseType.GetProperty(parts[0],
                //        BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)?.PropertyType,
                //     parts.Skip(1).Aggregate((a, i) => a + "." + i));
            }

            propertyInfos.Add(baseType.GetProperty(propertyName,
                BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance));

            return propertyInfos;
        }

        private static (int, FilterLogic) FilterLogicPartLength(string logicOperator, string[] filterLogicParts)
        {
            var filterLogicPartLength = logicOperator != "and" && logicOperator != "or"
                ? filterLogicParts.Length
                : filterLogicParts.Length - 1;

            var logicOperatorEnum = logicOperator != "and" && logicOperator != "or"
                ? FilterLogic.And
                : logicOperator == "and"
                    ? FilterLogic.And
                    : logicOperator == "or"
                        ? FilterLogic.Or
                        : throw new LogicOperatorNotFoundException($"{logicOperator} is not found");

            return (filterLogicPartLength, logicOperatorEnum);
        }

        #region [ Helpers ]

        private static FilterDataTypes GetFilterDataTypes(PropertyInfo pi)
        {
            var propertyName = pi.PropertyType.IsGenericType &&
                               pi.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>)
                ? pi.PropertyType.GetGenericArguments()[0].Name
                : pi.PropertyType.Name;

            if (pi.PropertyType.IsEnum)
            {
                return FilterDataTypes.Enum;
            }

            if (propertyName == "Int32" ||
                propertyName == "UInt16" ||
                propertyName == "Int16")
            {
                return FilterDataTypes.Int;
            }

            if (propertyName == "Int64" ||
                propertyName == "UInt64")
            {
                return FilterDataTypes.Long;
            }
            if (propertyName == "Decimal" ||
                propertyName == "Double")
            {
              return FilterDataTypes.Decimal;
            }
            if (propertyName == "String")
            {
                return FilterDataTypes.String;
            }

            if (propertyName == "Char")
            {
                return FilterDataTypes.Char;
            }

            if (propertyName == "DateTime")
            {
                return FilterDataTypes.DateTime;
            }

            if (propertyName == "Boolean")
            {
                return FilterDataTypes.Boolean;
            }

            if (propertyName == "Guid")
            {
                return FilterDataTypes.Guid;
            }

            throw new ArgumentOutOfRangeException();
        }

        #endregion


    }
}
