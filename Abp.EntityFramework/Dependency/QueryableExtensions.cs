using System.Collections.Generic;
using System.Linq;
using Abp.Domain.Entities;
using Lte.Domain.Common.Wireless;

namespace Abp.EntityFramework.Dependency
{
    /// <summary>
    /// Extension methods for <see cref="IQueryable"/> and <see cref="IQueryable{T}"/>.
    /// </summary>
    public static class MyQueryableExtensions
    {
        public static List<T> FilterTownList<T, TTown>(this IEnumerable<T> query, List<TTown> towns)
            where T : ITownId
            where TTown : Entity
        {
            return (from q in query join t in towns on q.TownId equals t.Id select q).ToList();
        }
    }
}