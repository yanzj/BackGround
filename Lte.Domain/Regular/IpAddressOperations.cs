using System;
using Lte.Domain.Common.Types;

namespace Lte.Domain.Regular
{
    public static class IpAddressOperations
    {
        public static string GetString(this IIpAddress address)
        {
            return address.IpByte1 + "." + address.IpByte2 + "."
                + address.IpByte3 + "." + address.IpByte4;
        }

        public static bool SetAddress(this IIpAddress address, string addressString)
        {
            if (!addressString.IsLegalIp()) { return false; }
            string[] parts = addressString.GetSplittedFields('.');
            address.IpByte1 = Convert.ToByte(parts[0]);
            address.IpByte2 = Convert.ToByte(parts[1]);
            address.IpByte3 = Convert.ToByte(parts[2]);
            address.IpByte4 = Convert.ToByte(parts[3]);
            return true;
        }
    }
}
