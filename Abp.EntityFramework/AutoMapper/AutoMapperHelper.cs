using System;
using System.Reflection;
using Abp.Collections.Extensions;
using AutoMapper;
using Lte.Domain.Common.Types;

namespace Abp.EntityFramework.AutoMapper
{
    public static class AutoMapperHelper
    {
        public static void CreateMap(Type type, IMapperConfiguration cfg)
        {
            CreateMap<AutoMapFromAttribute>(type, cfg);
            CreateMap<AutoMapToAttribute>(type, cfg);
            CreateMap<AutoMapAttribute>(type, cfg);
        }

        public static void CreateMap<TAttribute>(Type type, IMapperConfiguration cfg)
            where TAttribute : AutoMapAttribute
        {
            if (!type.IsDefined(typeof(TAttribute)))
            {
                return;
            }

            foreach (var autoMapToAttribute in type.GetCustomAttributes<TAttribute>())
            {
                if (autoMapToAttribute.TargetTypes.IsNullOrEmpty())
                {
                    continue;
                }

                foreach (var targetType in autoMapToAttribute.TargetTypes)
                {
                    if (autoMapToAttribute.Direction.HasFlag(AutoMapDirection.To))
                    {
                        var coreMap = cfg.CreateMap(type, targetType);
                        foreach (var property in type.GetProperties())
                        {
                            var resolveAttributes = property.GetCustomAttributes<AutoMapPropertyResolveAttribute>();
                            foreach (var resolveAttribute in resolveAttributes)
                            {
                                if (resolveAttribute.TargetType != targetType) continue;
                                var srcName = property.Name;
                                var destName = resolveAttribute.PeerMemberName;
                                var resolveActionType = resolveAttribute.ResolveActionType;
                                coreMap = resolveActionType == typeof (IgnoreMapAttribute)
                                    ? coreMap.ForMember(destName, map => map.Ignore())
                                    : coreMap.MappingCore(resolveActionType, destName, srcName);
                            }
                        }
                    }

                    if (autoMapToAttribute.Direction.HasFlag(AutoMapDirection.From))
                    {
                        var coreMap = cfg.CreateMap(targetType, type);
                        foreach (var property in type.GetProperties())
                        {
                            var resolveAttributes = property.GetCustomAttributes<AutoMapPropertyResolveAttribute>();
                            foreach (var resolveAttribute in resolveAttributes)
                            {
                                if (resolveAttribute.TargetType != targetType) continue;
                                var srcName = resolveAttribute.PeerMemberName;
                                var destName = property.Name;
                                var resolveActionType = resolveAttribute.ResolveActionType;
                                coreMap = coreMap.MappingCore(resolveActionType, destName, srcName);
                            }
                        }
                    }
                }
            }
        }

        private static IMappingExpression MappingCore(this IMappingExpression coreMap,
            Type resolveActionType, string destName, string srcName)
        {
            if (resolveActionType == null)
                coreMap = coreMap.ForMember(destName, map => map.MapFrom(srcName));
            else if (string.IsNullOrEmpty(srcName))
                coreMap = coreMap.ForMember(destName,
                    map => map.ResolveUsing(resolveActionType));
            else
                coreMap = coreMap.ForMember(destName,
                    map => map.ResolveUsing(resolveActionType).FromMember(srcName));
            return coreMap;
        }
    }
}