namespace ECommerceUI.Dtos
{
    public class SearchPage
    {
        public long TotalCount { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } =10;
        public long PageLinkCount { get; set; }
        public List<ECommerceList> List { get; set; }
        public ECommerceSearch Search { get; set; }
        public int StartPage()
        {
            return Page-5<=0?1:Page-5;
        }
        public long EndPage()
        {

            return Page + 5 >= PageLinkCount ? PageLinkCount : Page + 5;
        }

        public string CreatePageUrl(HttpRequest request, long page, int pageSize)
        {
            var fullUrl = new Uri($"{request.Scheme}://{request.Host}{request.Path}{request.QueryString}").AbsoluteUri;
            if (fullUrl.Contains("page",StringComparison.OrdinalIgnoreCase))
            {
                fullUrl = fullUrl.Replace($"Page={Page}", $"Page={page}", StringComparison.OrdinalIgnoreCase);
                fullUrl = fullUrl.Replace($"PageSize={PageSize}", $"Page={pageSize}", StringComparison.OrdinalIgnoreCase);
            }
            else
            {
                fullUrl = $"{fullUrl}?Page={page}";
                fullUrl = $"{fullUrl}&PageSize={pageSize}";
            }
            return fullUrl;
        }
    }
}
