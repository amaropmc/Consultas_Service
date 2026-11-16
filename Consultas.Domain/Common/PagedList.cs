using System;
using System.Collections.Generic;
using System.Text;

namespace Consultas.Domain.Common
{
    public class PagedList<T>
    {
        public IReadOnlyList<T> Items { get; }
        public int TotalCount { get; }
        public int PageNumber { get; }
        public int PageSize { get; }

        public int TotalPages =>
            (int)Math.Ceiling((double)TotalCount / PageSize);

        public PagedList(
            IReadOnlyList<T> items,
            int totalCount,
            int pageNumber,
            int pageSize)
        {
            Items = items;
            TotalCount = totalCount;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
