namespace MWalks.API.DTOs
{
    public class PaginationMetadata
    {
        public int TotalRecords { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int? NextPage { get; set; }
        public int? PrevPage { get; set; }
    }
}
