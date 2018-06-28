using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Lte.Domain.Common.Wireless;
using Lte.Domain.LinqToExcel.Service;
using Lte.Domain.Regular;

namespace Lte.Domain.Common.Types
{
    public static class TypeOperations
    {
        public static MethodInfo GetParseNumberMethod(this Type t)
        {
            return t.GetMethod("Parse",
                new[] {typeof (string), typeof (NumberStyles), typeof (IFormatProvider)});
        }

        public static MethodInfo GetParseExactMethod(this Type t)
        {
            return t.GetMethod("ParseExact",
                new[] {typeof (string), typeof (string), typeof (IFormatProvider)});
        }

        public static TEnum GetEnumType<TEnum>(this string description)
            where TEnum : struct
        {
            var attribute =
                typeof (TEnum).GetCustomAttribute<EnumTypeDescriptionAttribute>();
            if (attribute == null) return default(TEnum);
            var list = attribute.TupleList;
            var defaultValue = attribute.DefaultValue;
            var tuple = list.FirstOrDefault(x => x.Item2 == description);
            return (TEnum)(tuple?.Item1 ?? defaultValue);
        }

        public static TEnum GetEnumType<TEnum>(this string description, Tuple<TEnum, string>[] alternativeList)
            where TEnum : struct
        {
            var attribute =
                   typeof(TEnum).GetCustomAttribute<EnumTypeDescriptionAttribute>();
            if (attribute == null) return default(TEnum);
            var defaultValue = attribute.DefaultValue;
            var alternativeTuple = alternativeList.FirstOrDefault(x => x.Item2 == description);
            return alternativeTuple?.Item1 ?? (TEnum)defaultValue;
        }

        public static string GetEnumDescription<TEnum>(this TEnum type)
            where TEnum : struct
        {
            var attribute =
                typeof(TEnum).GetCustomAttribute<EnumTypeDescriptionAttribute>();
            if (attribute == null) return null;
            var list = attribute.TupleList;
            var defaultValue = attribute.DefaultValue;
            var defaultDescription = list.FirstOrDefault(x => x.Item1 == defaultValue)?.Item2 ?? "其他";
            var tuple = list.FirstOrDefault(x => x.Item1.Equals(type));
            return (tuple != null) ? tuple.Item2 : defaultDescription;
        }

        public static string GetEnumDescription<TEnum>(this TEnum type, Tuple<TEnum, string>[] alternativeList)
            where TEnum : struct
        {
            var attribute =
                typeof(TEnum).GetCustomAttribute<EnumTypeDescriptionAttribute>();
            if (attribute == null) return null;
            var list = attribute.TupleList;
            var defaultValue = attribute.DefaultValue;
            var defaultDescription = list.FirstOrDefault(x => x.Item1 == defaultValue)?.Item2;
            var tuple = list.FirstOrDefault(x => x.Item1.Equals(type));
            if (tuple != null) return tuple?.Item2;
            var alternativeTuple = alternativeList.FirstOrDefault(x => x.Item1.Equals(type));
            return alternativeTuple != null ? alternativeTuple.Item2 : defaultDescription;
        }

        public static byte? GetNextStateDescription<TEnum>(this string currentStateDescription, TEnum finalState)
            where TEnum: struct
        {
            var currentState = currentStateDescription.GetEnumType<TEnum>();
            if (currentState.Equals(finalState))
                return null;
            return (byte) (currentState.Cast<byte>() + 1);
        }
    }

    [AttributeUsage(AttributeTargets.Enum)]
    public class EnumTypeDescriptionAttribute : Attribute
    {
        public Type EnumType { get; }

        public Tuple<object, string>[] TupleList { get; }

        public object DefaultValue { get; }

        public EnumTypeDescriptionAttribute(Type enumType, object defaultValue)
        {
            EnumType = enumType;
            DefaultValue = defaultValue;
            TupleList = WirelessConstants.EnumDictionary[EnumType.Name];
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class AutoMapPropertyResolveAttribute : Attribute
    {
        public string PeerMemberName { get; }

        public Type TargetType { get; }

        public Type ResolveActionType { get; }

        public AutoMapPropertyResolveAttribute(string peerMemberName, Type targetType, Type resolvActionType = null)
        {
            PeerMemberName = peerMemberName;
            TargetType = targetType;
            ResolveActionType = resolvActionType;
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class AutoMapConverterAttribute : Attribute
    {
        public Type SourceType { get; }

        public Type ConverterType { get; }

        public AutoMapConverterAttribute(Type sourceType, Type converterType)
        {
            SourceType = sourceType;
            ConverterType = converterType;
        }
    }

    public class IntToBoolTransform : ValueResolver<int, bool>
    {
        protected override bool ResolveCore(int source)
        {
            return source == 1;
        }
    }

    public class PositiveBoolTransform : ValueResolver<int, bool>
    {
        protected override bool ResolveCore(int source)
        {
            return source > 0;
        }
    }

    public class ZeroBoolTransform : ValueResolver<int, bool>
    {
        protected override bool ResolveCore(int source)
        {
            return source == 0;
        }
    }

    public class SectorIdTransform : ValueResolver<int, byte>
    {
        protected override byte ResolveCore(int source)
        {
            return source > 255 ? (byte) 255 : (byte) source;
        }
    }

    public class DateTimeTransform : ValueResolver<DateTime, DateTime>
    {
        protected override DateTime ResolveCore(DateTime source)
        {
            return source < new DateTime(2000, 1, 1) ? new DateTime(2200, 1, 1) : source;
        }
    }

    public class IndoorDescriptionToOutdoorBoolTransform : ValueResolver<string, bool>
    {
        protected override bool ResolveCore(string source)
        {
            return source.Trim() == "否";
        }
    }

    public class YesToBoolTransform : ValueResolver<string, bool>
    {
        protected override bool ResolveCore(string source)
        {
            return source == "是";
        }
    }

    public class YesNoTransform : ValueResolver<bool, string>
    {
        protected override string ResolveCore(bool source)
        {
            return source ? "是" : "否";
        }
    }

    public class IndoorBoolTransform : ValueResolver<string, bool>
    {
        protected override bool ResolveCore(string source)
        {
            return source == "室内";
        }
    }

    public class IndoorDescriptionTransform : ValueResolver<bool, string>
    {
        protected override string ResolveCore(bool source)
        {
            return source ? "室内" : "室外";
        }
    }

    public class OutdoorDescriptionTransform : ValueResolver<bool, string>
    {
        protected override string ResolveCore(bool source)
        {
            return source ? "室外" : "室内";
        }
    }

    public class FddTransform : ValueResolver<string, bool>
    {
        protected override bool ResolveCore(string source)
        {
            return source.IndexOf("FDD", StringComparison.Ordinal) >= 0;
        }
    }

    public class NotNullTransform : ValueResolver<object, bool>
    {
        protected override bool ResolveCore(object source)
        {
            return source != null;
        }
    }

    public class NullableIntTransform : ValueResolver<int?, int>
    {
        protected override int ResolveCore(int? source)
        {
            return source ?? -1;
        }
    }

    public class NullableZeroIntTransform : ValueResolver<int?, int>
    {
        protected override int ResolveCore(int? source)
        {
            return source ?? 0;
        }
    }

    public class NullableZeroTransform : ValueResolver<double?, double>
    {
        protected override double ResolveCore(double? source)
        {
            return source ?? 0;
        }
    }
    
    public class SemiCommaTransform : ValueResolver<string, List<string>>
    {
        protected override List<string> ResolveCore(string source)
        {
            return string.IsNullOrEmpty(source) ? new List<string>() : source.Split(';').ToList();
        }
    }

    public class IpAddressTransform : ValueResolver<IpAddress, int>
    {
        protected override int ResolveCore(IpAddress source)
        {
            return source.AddressValue;
        }
    }

    public class IpByte4Transform : ValueResolver<IpAddress, byte>
    {
        protected override byte ResolveCore(IpAddress source)
        {
            return source.IpByte4;
        }
    }

    public class DoubleTransform : ValueResolver<double, double>
    {
        protected override double ResolveCore(double source)
        {
            return source*2;
        }
    }

    public class ByteTransform : ValueResolver<double, double>
    {
        protected override double ResolveCore(double source)
        {
            return source*8;
        }
    }

    public class MegaTransform : ValueResolver<long, double>
    {
        protected override double ResolveCore(long source)
        {
            return (double) source/(1024*1024);
        }
    }

    public class ThousandTransform : ValueResolver<int, double>
    {
        protected override double ResolveCore(int source)
        {
            return (double) source/1000;
        }
    }

    public class FirstLittleBracketContentsTransform : ValueResolver<string, string>
    {
        protected override string ResolveCore(string source)
        {
            return source.GetSplittedFields(new[] {'(', ')'})[0];
        }
    }

    public class SecondLittleBracketContentsTransform : ValueResolver<string, string>
    {
        protected override string ResolveCore(string source)
        {
            return source.GetSplittedFields(new[] {'(', ')'})[1];
        }
    }

    public class FirstMiddleBracketContentsTransform : ValueResolver<string, string>
    {
        protected override string ResolveCore(string source)
        {
            return source.GetSplittedFields(new[] {'[', ']'})[0];
        }
    }

    public class SecondMiddleBracketContentsTransform : ValueResolver<string, string>
    {
        protected override string ResolveCore(string source)
        {
            return source.GetSplittedFields(new[] { '[', ']' })[1];
        }
    }

    public class DateTimeNowTransform : ValueResolver<object, DateTime>
    {
        protected override DateTime ResolveCore(object source)
        {
            return DateTime.Now;
        }
    }

    public class DateTimeNowLabelTransform : ValueResolver<string, string>
    {
        protected override string ResolveCore(string source)
        {
            return "[" + DateTime.Now + "]" + source;
        }
    }

    public class StringToIntTransform : ValueResolver<string, int>
    {
        protected override int ResolveCore(string source)
        {
            return source.ConvertToInt(0);
        }
    }

    public class StringToDoubleTransform : ValueResolver<string, double>
    {
        protected override double ResolveCore(string source)
        {
            return source.ConvertToDouble(0);
        }
    }

    public class StringToDateTimeTransform : ValueResolver<string, DateTime>
    {
        protected override DateTime ResolveCore(string source)
        {
            return source.ConvertToDateTime(new DateTime(2200, 1, 1));
        }
    }

    public class FirstBracketCellIdTransform : ValueResolver<string, int>
    {
        protected override int ResolveCore(string source)
        {
            return string.IsNullOrEmpty(source) ? 0 : source.GetSubStringInFirstPairOfChars('[', ']').ConvertToInt(1);
        }
    }
}
