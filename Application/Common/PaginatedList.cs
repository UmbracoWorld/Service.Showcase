using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Service.Showcase.Application.Common;

public class PaginatedList<T>
{
    public object PageIndex { get; private set; }
    public object TotalPages { get; private set; }

    public IEnumerable<T> Items { get; private set; }


    public PaginatedList(IEnumerable<T> items, int count, int pageIndex, int pageSize)
    {
        PageIndex = pageIndex;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);

        Items = items;
    }


    public bool HasPreviousPage => (int)PageIndex > 1;

    public bool HasNextPage => (int)PageIndex < (int)TotalPages;

    public static async Task<PaginatedList<T>> CreateAsync(IEnumerable<T> source, int pageIndex, int pageSize)
    {
        var enumerable = source.ToList();
        var count = enumerable.Count;
        var items = enumerable.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        return new PaginatedList<T>(items, count, pageIndex, pageSize);
    }
}