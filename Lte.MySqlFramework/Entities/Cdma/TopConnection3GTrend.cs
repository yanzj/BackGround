using Lte.Domain.Common.Wireless;

namespace Lte.MySqlFramework.Entities.Cdma
{
    public class TopConnection3GTrend : IBtsIdQuery
    {
        public int BtsId { get; set; }

        public int CellId { get; set; }

        public byte SectorId { get; set; }

        public int TopDates { get; set; }

        public int WirelessDrop { get; set; }

        public int ConnectionAttempts { get; set; }

        public int ConnectionFails { get; set; }

        public double LinkBusyRate { get; set; }
    }
}