using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Lte.Domain.Common;
using Lte.Domain.Regular.Attributes;

namespace Lte.Domain.Regular
{
    public static class SecureValue
    {
        public static Tuple<int, int> GetDivsionIntTuple<TObject>(this TObject kpiObject, int division)
        {
            var kpiProperty = (typeof(TObject)).GetCustomAttribute<IncreaseNumberKpiAttribute>();
            if (kpiProperty == null) return new Tuple<int, int>(0, 0);
            var range = kpiProperty.IndexRange;
            division = Math.Max(0, Math.Min(range, division));
            var low = 0;
            var high = 0;
            for (var i = 0; i < range; i++)
            {
                var property = (typeof(TObject)).GetProperty(kpiProperty.KpiPrefix + i + kpiProperty.KpiAffix);
                if (property==null) continue;
                var value = (int) property.GetValue(kpiObject, null);
                if (i < division)
                    low += value;
                else
                {
                    high += value;
                }
            }
            return new Tuple<int, int>(low, high);
        }

        public static Tuple<long, long> GetDivsionLongTuple<TObject>(this TObject kpiObject, int division)
        {
            var kpiProperty = (typeof(TObject)).GetCustomAttribute<IncreaseNumberKpiAttribute>();
            if (kpiProperty == null) return new Tuple<long, long>(0, 0);
            var range = kpiProperty.IndexRange;
            division = Math.Max(0, Math.Min(range, division));
            var low = 0L;
            var high = 0L;
            for (var i = 0; i < range; i++)
            {
                var property = (typeof(TObject)).GetProperty(kpiProperty.KpiPrefix + i + kpiProperty.KpiAffix);
                if (property == null) continue;
                var value = (long)property.GetValue(kpiObject, null);
                if (i < division)
                    low += value;
                else
                {
                    high += value;
                }
            }
            return new Tuple<long, long>(low, high);
        }

        public static void Increase<T>(this T source, T increment)
        {
            var properties = (typeof(T)).GetProperties();
            foreach (var property in from property in properties.Where(property => property.CanWrite  && property.CanRead)
                let attribute = property.GetCustomAttribute<ArraySumProtectionAttribute>()
                where attribute == null
                select property)
            {
                ;
                switch (property.PropertyType.Name)
                {
                    case "Int16":
                        var value16
                            =
                            (short)
                                ((short) property.GetValue(increment, null) + (short) property.GetValue(source, null));
                        property.SetValue(source, value16, null);
                        break;
                    case "Int32":
                        property.SetValue(source,
                            (int) property.GetValue(increment, null) + (int) property.GetValue(source, null),
                            null);
                        break;
                    case "Int64":
                        property.SetValue(source,
                            (long) property.GetValue(increment, null) + (long) property.GetValue(source, null),
                            null);
                        break;
                    case "Double":
                        property.SetValue(source,
                            (double) property.GetValue(increment, null) + (double) property.GetValue(source, null),
                            null);
                        break;
                }
            }
        }

        public static void DividedBy<T>(this T source, int scalor)
        {
            var properties = (typeof(T)).GetProperties();

            foreach (var property in from property in properties.Where(property => property.CanWrite)
                                     let attribute = property.GetCustomAttribute<ArraySumProtectionAttribute>()
                                     where attribute == null
                                     select property)
            {
                switch (property.PropertyType.Name)
                {
                    case "Int16":
                        var value16 = (short)((short)property.GetValue(source, null) / scalor);
                        property.SetValue(source, value16, null);
                        break;
                    case "Int32":
                        property.SetValue(source, (int)property.GetValue(source, null) / scalor, null);
                        break;
                    case "Int64":
                        property.SetValue(source, (long)property.GetValue(source, null) / scalor, null);
                        break;
                    case "Double":
                        property.SetValue(source, (double)property.GetValue(source, null) / scalor, null);
                        break;
                }
            }
        }

        public static T Average<T>(this IEnumerable<T> sourceList)
            where T : class, new()
        {
            if (sourceList == null) return null;
            IEnumerable<T> enumerable = sourceList as T[] ?? sourceList.ToArray();
            var result = enumerable.ArraySum();
            result.DividedBy(enumerable.Count());
            return result;
        }

        public static T ArraySum<T>(this IEnumerable<T> sourceList) where T : class, new()
        {
            var result = new T();
            IEnumerable<T> enumerable = sourceList as T[] ?? sourceList.ToArray();
            if (!enumerable.Any()) return result;
            enumerable.ElementAt(0).CloneDateTimeValue(result);
            foreach (var item in enumerable)
            {
                result.Increase(item);
            }
            return result;
        }

        public static T ArrayAggration<T>(this IEnumerable<T> sourceList, Action<T> setDefaultValues) 
            where T : class, new()
        {
            var result = new T();
            setDefaultValues(result);
            var properties = (typeof(T)).GetProperties();
            var enumerable = sourceList as T[] ?? sourceList.ToArray();

            foreach (var property in from property in properties.Where(property => property.CanWrite)
                let attribute = property.GetCustomAttribute<ArraySumProtectionAttribute>()
                where attribute == null
                select property)
            {
                var averageAttribute = property.GetCustomAttribute<ArrayAverageAttribute>();
                var maxAttribute = property.GetCustomAttribute<ArrayMaxAttribute>();
                var minAttribute = property.GetCustomAttribute<ArrayMinAttribute>();
                switch (property.PropertyType.Name)
                {
                    case "Int16":
                        if (averageAttribute != null)
                        {
                            property.SetValue(result, (short)enumerable.Average(x => (short) property.GetValue(x)));
                        }
                        else if (maxAttribute != null)
                        {
                            property.SetValue(result, enumerable.Max(x => (short) property.GetValue(x)));
                        }
                        else if (minAttribute != null)
                        {
                            property.SetValue(result, enumerable.Min(x => (short) property.GetValue(x)));
                        }
                        else
                        {
                            property.SetValue(result, (short) enumerable.Sum(x => (short) property.GetValue(x)));
                        }
                        break;
                    case "Int32":
                        if (averageAttribute != null)
                        {
                            property.SetValue(result, (int)enumerable.Average(x => (int)property.GetValue(x)));
                        }
                        else if (maxAttribute != null)
                        {
                            property.SetValue(result, enumerable.Max(x => (int)property.GetValue(x)));
                        }
                        else if (minAttribute != null)
                        {
                            property.SetValue(result, enumerable.Min(x => (int)property.GetValue(x)));
                        }
                        else
                        {
                            property.SetValue(result, enumerable.Sum(x => (int)property.GetValue(x)));
                        }
                        break;
                    case "Int64":
                        if (averageAttribute != null)
                        {
                            property.SetValue(result, (long)enumerable.Average(x => (long)property.GetValue(x)));
                        }
                        else if (maxAttribute != null)
                        {
                            property.SetValue(result, enumerable.Max(x => (long)property.GetValue(x)));
                        }
                        else if (minAttribute != null)
                        {
                            property.SetValue(result, enumerable.Min(x => (long)property.GetValue(x)));
                        }
                        else
                        {
                            property.SetValue(result, enumerable.Sum(x => (long)property.GetValue(x)));
                        }
                        break;
                    case "Double":
                        if (averageAttribute != null)
                        {
                            property.SetValue(result, enumerable.Average(x => (double)property.GetValue(x)));
                        }
                        else if (maxAttribute != null)
                        {
                            property.SetValue(result, enumerable.Max(x => (double)property.GetValue(x)));
                        }
                        else if (minAttribute != null)
                        {
                            property.SetValue(result, enumerable.Min(x => (double)property.GetValue(x)));
                        }
                        else
                        {
                            property.SetValue(result, enumerable.Sum(x => (double)property.GetValue(x)));
                        }
                        break;
                    case "String":
                        property.SetValue(result, property.GetValue(enumerable[0])?.ToString());
                        break;
                }
            }
            return result;
        }

        private static void CloneDateTimeValue<T>(this T source, T destination)
            where T : class, new()
        {
            var properties = (typeof(T)).GetProperties();
            foreach (var property in properties
                .Where(property => property.CanRead && property.CanWrite)
                .Where(property => (property.PropertyType.Name != "Int16" && property.PropertyType.Name != "Int32"
                && property.PropertyType.Name != "Int64" && property.PropertyType.Name != "Double")
                || property.GetCustomAttribute<ArraySumProtectionAttribute>() != null))
            {
                property.SetValue(destination, property.GetValue(source, null), null);
            }
        }
    }
}
