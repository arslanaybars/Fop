using System.Collections.Generic;
using Fop.Filter;

namespace Fop.Utils
{
    public class Operator
    {
        public static Dictionary<string, FilterOperators> Dictionary => new Dictionary<string, FilterOperators>
        {
            {"==", FilterOperators.Equal},
            {"!=", FilterOperators.NotEqual},
            {">", FilterOperators.GreaterThan},
            {">=", FilterOperators.GreaterOrEqualThan},
            {"<", FilterOperators.LessThan},
            {"<=", FilterOperators.LessOrEqualThan},
            {"~=", FilterOperators.Contains},
            {"!~=", FilterOperators.NotContains},
            {"_=", FilterOperators.StartsWith},
            {"!_=", FilterOperators.NotStartsWith},
            {"|=", FilterOperators.EndsWith},
            {"!|=", FilterOperators.NotEndsWith}
        };
    }
}
