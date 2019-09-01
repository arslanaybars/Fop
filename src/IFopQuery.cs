namespace Fop
{
    public interface IFopQuery
    {
        string Filter { get; set; }

        string Order { get; set; }

        int PageNumber { get; set; }

        int PageSize { get; set; }
    }
}
