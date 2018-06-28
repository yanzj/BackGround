using System;
using System.Collections.Generic;
using System.Linq;

namespace Lte.Domain.LinqToExcel.Service
{
    public static class CastService
    {
        public static T Cast<T>(this object @object)
        {
            return (T)@object.Cast(typeof(T));
        }

        public static object Cast(this object @object, Type castType)
        {
            //return null for DBNull values
            if (@object == null || @object is DBNull)
                return null;

            if (castType.IsValueType && @object.ToString() == "")
                return null;

            //checking for nullable types
            if (castType.IsGenericType &&
                castType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                castType = Nullable.GetUnderlyingType(castType);
            }
            return Convert.ChangeType(@object, castType);
        }

        public static IEnumerable<TResult> Cast<TResult>(this IEnumerable<object> list, Func<object, TResult> caster)
        {
            return list.Select(caster).ToList();
        }

        public static IEnumerable<TResult> Cast<TResult>(this IEnumerable<object> list)
        {
            var func = new Func<object, TResult>(item =>
                (TResult)Convert.ChangeType(item, typeof(TResult)));
            return list.Cast(func);
        }
    }
}
