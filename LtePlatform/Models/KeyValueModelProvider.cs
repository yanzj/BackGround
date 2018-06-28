using System;

namespace LtePlatform.Models
{
    public class KeyValueModelProvider : IModelGeneratorProvider
    {
        private readonly Type _keyType;
        private readonly Type _valueType;

        public KeyValueModelProvider(Type keyType, Type valueType)
        {
            _keyType = keyType;
            _valueType = valueType;
        }

        public ModelDescription Generate(Type modelType, ModelDescriptionGenerator generator)
        {
            var keyModelDescription = generator.GetOrCreateModelDescription(_keyType);
            var valueModelDescription = generator.GetOrCreateModelDescription(_valueType);

            return new KeyValuePairModelDescription
            {
                Name = ModelNameHelper.GetModelName(modelType),
                ModelType = modelType,
                KeyModelDescription = keyModelDescription,
                ValueModelDescription = valueModelDescription
            };
        }
    }
}