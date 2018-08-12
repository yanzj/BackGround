using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abp.EntityFramework.Dependency
{
    public interface IDateSpanQuery<out T>
    {
        T Query(DateTime begin, DateTime end);
    }

}
