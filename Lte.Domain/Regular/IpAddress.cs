using Lte.Domain.Common;

namespace Lte.Domain.Regular
{

    public class IpAddress : IIpAddress
    {
        public byte IpByte1 { get; set; }
        public byte IpByte2 { get; set; }
        public byte IpByte3 { get; set; }
        public byte IpByte4 { get; set; }

        public IpAddress()
        { }

        public IpAddress(string ip)
        {
            bool result = this.SetAddress(ip);
            if (!result) { this.SetAddress("0.0.0.0"); }
        }

        public string AddressString
        {
            get { return this.GetString(); }
        }

        public int AddressValue
        {
            get
            {
                return IpByte1 << 24 | IpByte2 << 16 | IpByte3 << 8 | IpByte4;
            }
            set
            {
                IpByte1 = (value >> 24).GetLastByte();
                IpByte2 = (value >> 16).GetLastByte();
                IpByte3 = (value >> 8).GetLastByte();
                IpByte4 = value.GetLastByte();
            }
        }
    }
}
