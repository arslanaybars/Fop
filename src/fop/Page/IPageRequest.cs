namespace Fop.Page
{
    public interface IPageRequest
    {
        int PageNumber { get; set; }

        int PageSize { get; set; }
    }
}
