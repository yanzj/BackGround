using System;

namespace LtePlatform.Models
{
    public class CollectionModelProvider : IModelGeneratorProvider
    {
        private readonly Type _elementType;

        public ModelDescription Generate(Type modelType, ModelDescriptionGenerator generator)
        {
            var collectionModelDescription = generator.GetOrCreateModelDescription(_elementType);
            if (collectionModelDescription != null)
            {
                return new CollectionModelDescription
                {
                    Name = ModelNameHelper.GetModelName(modelType),
                    ModelType = modelType,
                    ElementDescription = collectionModelDescription,
                    ParameterDocumentation = collectionModelDescription.ParameterDocumentation
                };
            }

            return null;
        }

        public CollectionModelProvider(Type elementType)
        {
            _elementType = elementType;
        }
    }
}