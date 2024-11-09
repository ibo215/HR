namespace HR.ViewModels
{
    public class PaginationMetaData
    {
        public int PageSize { get; set; }
        public int CurreentPage { get; set; }
        public int TotalPageCount { get; set; }
        public int TotalItemCount { get; set; }
        public PaginationMetaData(int totalItemCount, int pageSize, int currentPage)
        {
            TotalPageCount = (int)Math.Ceiling(totalItemCount / (double)pageSize);
            TotalItemCount = totalItemCount;
            PageSize = pageSize;
            CurreentPage = currentPage;
        }
    }
}
