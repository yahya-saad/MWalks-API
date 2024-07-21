namespace MWalks.API.Utilities
{
    public class PaginationService
    {
        public async Task<(IQueryable<T> Data, PaginationMetadata Pagination)> GetPaginatedResultAsync<T>(
            IQueryable<T> queryable,
            int page,
            int limit) where T : class
        {
            var totalCount = await queryable.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)limit);
            var currentPage = page;
            var nextPage = currentPage < totalPages ? currentPage + 1 : (int?)null;
            var prevPage = currentPage > 1 ? currentPage - 1 : (int?)null;

            var paginatedData = queryable
                .Skip((currentPage - 1) * limit)
                .Take(limit);

            var pagination = new PaginationMetadata
            {
                TotalRecords = totalCount,
                CurrentPage = currentPage,
                TotalPages = totalPages,
                NextPage = nextPage,
                PrevPage = prevPage
            };

            return (Data: paginatedData, Pagination: pagination);
        }
    }
}
