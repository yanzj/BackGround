using System;

namespace LtePlatform.Models
{
    public interface IModelGeneratorProvider
    {
        ModelDescription Generate(Type modelType, ModelDescriptionGenerator generator);
    }
}