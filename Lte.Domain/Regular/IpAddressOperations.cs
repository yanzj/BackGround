using System;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Common.Wireless.ENodeb;

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

        public static int GetRruRackId(this string rruNumber, ENodebFactory factory)
        {
            var fields = rruNumber.GetSplittedFields('_');
            if (fields.Length == 0) return 0;
            var rackField = fields[fields.Length - 1];
            switch (factory)
            {
                case ENodebFactory.Datang:
                    return rackField.ConvertToInt(0);
                case ENodebFactory.Zte:
                    var zteFields = rackField.GetSplittedFields('-');
                    return zteFields.Length == 0 ? 0 : zteFields[0].ConvertToInt(0);
                case ENodebFactory.Huawei:
                    var huaweiFields = rackField.GetSplittedFields('-');
                    return huaweiFields.Length < 2 ? 0 : huaweiFields[1].ConvertToInt(0);
                default:
                    return 0;
            }
        }
    }
}
