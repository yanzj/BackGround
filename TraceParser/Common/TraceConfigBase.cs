using Lte.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Lte.Domain.Common.Types;

namespace TraceParser.Common
{
    public abstract class TraceConfig
    {
        public static void InitDefaults()
        {
        }
    }

    public interface IDecoder<out TConfig>
        where TConfig : TraceConfig
    {
        TConfig Decode(BitArrayInputStream input);
    }

    public abstract class DecoderBase<TConfig> : IDecoder<TConfig>
        where TConfig : TraceConfig, new()
    {
        public TConfig Decode(BitArrayInputStream input)
        {
            TraceConfig.InitDefaults();
            var config = new TConfig();
            ProcessConfig(config, input);
            return config;
        }

        protected abstract void ProcessConfig(TConfig config, BitArrayInputStream input);
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class CodeBitAttribute : Attribute
    {
        public int Position { get; set; } = 0;

        public int BitToBeRead { get; set; } = 1;
    }
    
    public static class CodeBitReaderOpertions
    {
        private static readonly Dictionary<Type, IEnumerable<Tuple<MemberInfo, int, int>>> ReaderDictionary =
            new Dictionary<Type, IEnumerable<Tuple<MemberInfo, int, int>>>();

        private static IEnumerable<Tuple<MemberInfo, int, int>> ConstructCodeBitReader(Type type)
        {
            return (from field in type.GetMembers()
                let attribute = Attribute.GetCustomAttribute(field, typeof (CodeBitAttribute)) as CodeBitAttribute
                where attribute != null
                select new Tuple<MemberInfo, int, int>(field, attribute.Position, attribute.BitToBeRead));
        }

        private static void ReadCodeBits<T>(this T source, IEnumerable<Tuple<MemberInfo, int, int>> reader,
            IBitArrayReader input, int bit)
        {
            foreach (var tuple in reader.Where(tuple => tuple.Item2 == bit))
            {
                if (tuple.Item1 is FieldInfo)
                {
                    ((FieldInfo) tuple.Item1).SetValue(source, input.ReadBitString(tuple.Item3));
                    return;
                }
                if (tuple.Item1 is PropertyInfo)
                {
                    ((PropertyInfo) tuple.Item1).SetValue(source, input.ReadBitString(tuple.Item3));
                    return;
                }
            }
            throw new Exception(typeof(T).Name + ":NoChoice had been choose");
        }

        public static void ReadCodeBits<T>(this T source, IBitArrayReader input, int bit)
        {
            if (!ReaderDictionary.ContainsKey(typeof (T)))
            {
                ReaderDictionary.Add(typeof (T), ConstructCodeBitReader(typeof(T)));
            }
            var reader = ReaderDictionary[typeof (T)];
            source.ReadCodeBits(reader, input, bit);
        }
    }
}
