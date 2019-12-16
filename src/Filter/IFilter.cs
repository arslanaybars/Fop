namespace Fop.Filter
{
    public interface IFilter
    {
        FilterOperators Operator { get; set; }

        FilterDataTypes DataType { get; set; }

        string Key { get; set; }

        string Value { get; set; }

        string Assembly { get; set; }

        string Fullname { get; set; }
    }
}
