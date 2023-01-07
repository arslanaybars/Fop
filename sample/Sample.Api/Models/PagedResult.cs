namespace Sample.Api.Models;

public class PagedResult<T> 
{

    public PagedResult(T data, int totalCount, int pageNumber, int pageSize)
    {
        Data = data;
        TotalCount = totalCount;
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalPages = ((totalCount - 1) / pageSize) + 1;
    }


    public T Data { get; set; }

    public int TotalCount { get; set; }

    public int PageNumber { get; set; }

    public int PageSize { get; set; }
        
    public int TotalPages { get; set; }
}