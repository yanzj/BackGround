using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities.Kpi;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Common.Wireless.Kpi;
using Lte.MySqlFramework.Abstract.Kpi;

namespace Lte.Evaluations.DataService.Kpi
{
    public class HourUsersService
    {
        private readonly IHourUsersRepository _usersRepository;

        private static Stack<HourUsers> HourUserses { get; set; }

        public int HourUsersCount => HourUserses.Count;

        public HourUsersService(IHourUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
            if (HourUserses == null) HourUserses = new Stack<HourUsers>();
        }

        public void UploadUserses(StreamReader reader)
        {
            var originCsvs = HourUsersCsv.ReadCsvs(reader);
            foreach (var csv in originCsvs)
            {
                HourUserses.Push(csv.MapTo<HourUsers>());
            }
        }

        public async Task<bool> DumpOneUsersStat()
        {
            var stat = HourUserses.Pop();
            if (stat != null)
            {
                await _usersRepository.ImportOneAsync(stat);
            }

            return true;
        }

        public void ClearUsersStats()
        {
            HourUserses.Clear();
        }

    }
}
