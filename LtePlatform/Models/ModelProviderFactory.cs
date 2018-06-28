using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace LtePlatform.Models
{
    public static class ModelProviderFactory
    {
        // Modify this to add more default documentations.
        public static readonly IDictionary<Type, string> DefaultTypeDocumentation = new Dictionary<Type, string>
        {
            { typeof(short), "integer" },
            { typeof(int), "integer" },
            { typeof(long), "integer" },
            { typeof(ushort), "unsigned integer" },
            { typeof(uint), "unsigned integer" },
            { typeof(ulong), "unsigned integer" },
            { typeof(byte), "byte" },
            { typeof(char), "character" },
            { typeof(sbyte), "signed byte" },
            { typeof(Uri), "URI" },
            { typeof(float), "decimal number" },
            { typeof(double), "decimal number" },
            { typeof(decimal), "decimal number" },
            { typeof(string), "string" },
            { typeof(Guid), "globally unique identifier" },
            { typeof(TimeSpan), "time interval" },
            { typeof(DateTime), "date" },
            { typeof(DateTimeOffset), "date" },
            { typeof(bool), "boolean" },
        };

        public static IModelGeneratorProvider GetProvider(Type modelType)
        {
            if (DefaultTypeDocumentation.ContainsKey(modelType))
            {
                return new SimpleModelProvider();
            }

            if (modelType.IsEnum)
            {
                return new EnumModelProvider();
            }

            if (modelType.IsGenericType)
            {
                var genericArguments = modelType.GetGenericArguments();

                if (genericArguments.Length == 1)
                {
                    var enumerableType = typeof(IEnumerable<>).MakeGenericType(genericArguments);
                    if (enumerableType.IsAssignableFrom(modelType))
                    {
                        return new CollectionModelProvider(genericArguments[0]);
                    }
                }
                if (genericArguments.Length == 2)
                {
                    var dictionaryType = typeof(IDictionary<,>).MakeGenericType(genericArguments);
                    var keyValuePairType = typeof(KeyValuePair<,>).MakeGenericType(genericArguments);
                    if (dictionaryType.IsAssignableFrom(modelType) || keyValuePairType.IsAssignableFrom(modelType))
                    {
                        return new KeyValueModelProvider(genericArguments[0], genericArguments[1]);
                    }
                }
            }

            if (modelType.IsArray)
            {
                var elementType = modelType.GetElementType();
                return new CollectionModelProvider(elementType);
            }

            if (modelType == typeof(NameValueCollection))
            {
                return new KeyValueModelProvider(typeof(string), typeof(string));
            }

            if (typeof(IDictionary).IsAssignableFrom(modelType))
            {
                return new KeyValueModelProvider(typeof(object), typeof(object));
            }

            if (typeof (IEnumerable).IsAssignableFrom(modelType))
            {
                return new CollectionModelProvider(typeof (object));
            }
            return new ComplexModelProvider();
        }
    }
}