namespace WebsiteManager.Models
{
    public class PaginationDetails
    {
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public string SearchText { get; set; }
        public int SortBy { get; set; }
    }
}
