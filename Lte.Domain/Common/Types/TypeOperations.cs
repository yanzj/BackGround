using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Lte.Domain.LinqToExcel.Service;

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
}
