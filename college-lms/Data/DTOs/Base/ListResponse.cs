namespace college_lms.Data.DTOs.Base;

public class ListResponse<T> : DataResponse<List<T>>
{
    public required Pagination Pagination { get; set; }
}
