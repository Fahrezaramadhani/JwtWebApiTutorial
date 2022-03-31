using AutoMapper;
using JwtWebApiTutorial.Responses;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;

namespace JwtWebApiTutorial.Helpers
{
    public class PaginationHelper
    {
        public static PaginatedResponse<TS> GetPagedResult<T, TS>(IEnumerable<T> query, SieveModel sieveModel, IMapper mapper, int count = 0)
            where T : class
            where TS : class
        {
            PaginatedResponse<TS> result = new PaginatedResponse<TS>();
            int currentPage = sieveModel.Page.Value;
            int pageSize = sieveModel.PageSize.Value;

            int totalData = count;
            int totalPage = (int)Math.Ceiling((double)totalData / pageSize);

            int skip = (currentPage - 1) * pageSize;

            result.TotalData = totalData;
            result.TotalPage = totalPage;
            result.PageSize = pageSize;
            result.CurrentPage = currentPage;
            result.Data = query.Select(data => mapper.Map<TS>(data)).ToList();

            return result;
        }

        public static async Task<PaginatedResponse<TS>> GetPagedResultAsync<T, TS>(IQueryable<T> query, SieveModel sieveModel, IMapper mapper, int count = 0)
            where T : class
            where TS : class
        {
            PaginatedResponse<TS> result = new PaginatedResponse<TS>();
            int currentPage = sieveModel.Page.Value;
            int pageSize = sieveModel.PageSize.Value;

            int totalData = count;
            int totalPage = (int)Math.Ceiling((double)totalData / pageSize);

            int skip = (currentPage - 1) * pageSize;

            result.TotalData = totalData;
            result.TotalPage = totalPage;
            result.PageSize = pageSize;
            result.CurrentPage = currentPage;
            result.Data = await query.Select(data => mapper.Map<TS>(data)).ToListAsync();

            return result;
        }
    }
}
