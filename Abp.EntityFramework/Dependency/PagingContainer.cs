using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abp.EntityFramework.Dependency
{
    public class PagingContainer<T>
    {
        public IEnumerable<T> Stats { get; set; }

        public int TotalItems { get; set; }

        public int TotalPages => (int) Math.Ceiling((double) TotalItems / ItemsPerPage);

        public int ItemsPerPage { get; set; }

        public int CurrentPage { get; set; }
    }
}
