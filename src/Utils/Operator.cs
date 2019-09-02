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
            {">=", FilterOperators.GreaterOrEqualThan},
            { ">", FilterOperators.GreaterThan},
            {"<=", FilterOperators.LessOrEqualThan},
            { "<", FilterOperators.LessThan},
            {"!~=", FilterOperators.NotContains},
            {"~=", FilterOperators.Contains},
            {"!_=", FilterOperators.NotStartsWith},
            {"_=", FilterOperators.StartsWith},
            {"!|=", FilterOperators.NotEndsWith},
            {"|=", FilterOperators.EndsWith}
        };
    }
}
