using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities;

namespace Lte.MySqlFramework.Abstract
{
    public interface ICheckingDetailsRepository : IRepository<CheckingDetails>
    {
    }
}
