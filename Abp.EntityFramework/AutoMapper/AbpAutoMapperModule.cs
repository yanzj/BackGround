using Abp.Localization;
using Abp.Modules;
using Abp.Reflection;
using AutoMapper;
using Castle.Core.Logging;
using System.Reflection;
using Lte.Domain.Common.Types;

namespace Abp.EntityFramework.AutoMapper
{
    [DependsOn(typeof (AbpKernelModule))]
    public class AbpAutoMapperModule : AbpModule
    {
        private readonly ITypeFinder _typeFinder;

        private static bool _createdMappingsBefore;
        private static readonly object _syncObj = new object();

        public AbpAutoMapperModule(ITypeFinder typeFinder)
        {
            _typeFinder = typeFinder;
            Logger = NullLogger.Instance;
        }

        public override void PostInitialize()
        {
            Mapper.Initialize(CreateMappings);
            
        }

        private void CreateMappings(IMapperConfiguration cfg)
        {
            lock (_syncObj)
            {
                //We should prevent duplicate mapping in an application, since AutoMapper is static.
                if (_createdMappingsBefore)
                {
                    return;
                }

                FindAndAutoMapTypes(cfg);
                CreateOtherMappings(cfg);

                _createdMappingsBefore = true;
            }
        }

        private void FindAndAutoMapTypes(IMapperConfiguration cfg)
        {
            var types = _typeFinder.Find(type =>
                type.IsDefined(typeof(AutoMapAttribute)) ||
                type.IsDefined(typeof(AutoMapFromAttribute)) ||
                type.IsDefined(typeof(AutoMapToAttribute))
                );

            Logger.DebugFormat("Found {0} classes defines auto mapping attributes", types.Length);
            foreach (var type in types)
            {
                Logger.Debug(type.FullName);
                AutoMapperHelper.CreateMap(type, cfg);
            }
        }

        private void CreateOtherMappings(IMapperConfiguration cfg)
        {
            var types = _typeFinder.Find(type => type.IsDefined(typeof (AutoMapConverterAttribute)));
            if (types.Length > 0)
            {
                var destType = types[0];
                var attribute = destType.GetCustomAttribute<AutoMapConverterAttribute>();
                if (attribute != null)
                {
                    var sourceType = attribute.SourceType;
                    var converterType = attribute.ConverterType;
                    cfg.CreateMap(sourceType,destType).ConvertUsing(converterType);
                }
            }
            if (IocManager == null) return;
            var localizationManager = IocManager.Resolve<ILocalizationManager>();
            cfg.CreateMap<LocalizableString, string>().ConvertUsing(ls => localizationManager.GetString(ls));
        }
    }
}
