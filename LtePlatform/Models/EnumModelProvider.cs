using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace LtePlatform.Models
{
    public class EnumModelProvider : IModelGeneratorProvider
    {
        public ModelDescription Generate(Type modelType, ModelDescriptionGenerator generator)
        {
            var enumDescription = new EnumTypeModelDescription
            {
                Name = ModelNameHelper.GetModelName(modelType),
                ModelType = modelType,
                Documentation = generator.CreateDefaultDocumentation(modelType)
            };
            var hasDataContractAttribute = modelType.GetCustomAttribute<DataContractAttribute>() != null;
            foreach (var field in modelType.GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                if (!ModelDescriptionGenerator.ShouldDisplayMember(field, hasDataContractAttribute)) continue;
                var enumValue = new EnumValueDescription
                {
                    Name = field.Name,
                    Value = field.GetRawConstantValue().ToString()
                };
                if (generator.DocumentationProvider != null)
                {
                    enumValue.Documentation = generator.DocumentationProvider.GetDocumentation(field);
                }
                enumDescription.Values.Add(enumValue);
            }
            generator.GeneratedModels.Add(enumDescription.Name, enumDescription);

            return enumDescription;
        }
    }
}