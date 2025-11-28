using EcommerceApp.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApp.Shared.Extensions
{
    // Shared/Extensions/IQueryableExtensions.cs
    public static class IQueryableExtensions
    {
        public static async Task<PagedResponse<TDto>> ToPagedResponseAsync<TEntity, TDto>(
            this IQueryable<TEntity> query,
            int page,
            int pageSize,
            Func<TEntity, TDto> map) where TEntity : class
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 20;

            var totalRecords = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
            var skip = (page - 1) * pageSize;

            var items = await query
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();

            var data = items.Select(map);

            return new PagedResponse<TDto>
            {
                Data = data,
                Page = page,
                PageSize = pageSize,
                TotalRecords = totalRecords,
                TotalPages = totalPages
            };
        }
    }

}
