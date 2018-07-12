using System.Collections.Generic;
using Lte.Domain.Regular.Attributes;

namespace Lte.Domain.Common.Geo
{
    public class CollegeBtsIdsContainer
    {
        public string CollegeName { get; set; }

        [MemberDoc("基站编号列表")]
        public IEnumerable<int> BtsIds { get; set; }
    }
}