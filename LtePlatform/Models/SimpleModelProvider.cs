using System;

namespace LtePlatform.Models
{
    public class SimpleModelProvider : IModelGeneratorProvider
    {
        public ModelDescription Generate(Type modelType, ModelDescriptionGenerator generator)
        {
            var simpleModelDescription = new SimpleTypeModelDescription
            {
                Name = ModelNameHelper.GetModelName(modelType),
                ModelType = modelType,
                Documentation = generator.CreateDefaultDocumentation(modelType)
            };
            generator.GeneratedModels.Add(simpleModelDescription.Name, simpleModelDescription);

            return simpleModelDescription;
        }
    }
}