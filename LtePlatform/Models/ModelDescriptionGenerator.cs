using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Runtime.Serialization;
using System.Web.Http;
using System.Web.Http.Description;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace LtePlatform.Models
{
    public class ModelDescriptionGenerator
    {
        public ModelDescriptionGenerator(HttpConfiguration config)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            DocumentationProvider = config.Services.GetDocumentationProvider() as IModelDocumentationProvider ?? new DocProvider();
        }

        public Dictionary<string, ModelDescription> GeneratedModels { get; } = new Dictionary<string, ModelDescription>(StringComparer.OrdinalIgnoreCase);

        public IModelDocumentationProvider DocumentationProvider { get; private set; }

        public ModelDescription GetOrCreateModelDescription(Type modelType)
        {
            if (modelType == null)
            {
                throw new ArgumentNullException(nameof(modelType));
            }

            var underlyingType = Nullable.GetUnderlyingType(modelType);
            if (underlyingType != null)
            {
                modelType = underlyingType;
            }

            ModelDescription modelDescription;
            var modelName = ModelNameHelper.GetModelName(modelType);
            if (GeneratedModels.TryGetValue(modelName, out modelDescription))
            {
                if (modelType != modelDescription.ModelType)
                {
                    throw new InvalidOperationException(
                        string.Format(
                            CultureInfo.CurrentCulture,
                            "A model description could not be created. Duplicate model name '{0}' was found for types '{1}' and '{2}'. " +
                            "Use the [ModelName] attribute to change the model name for at least one of the types so that it has a unique name.",
                            modelName,
                            modelDescription.ModelType.FullName,
                            modelType.FullName));
                }

                return modelDescription;
            }

            var provider = ModelProviderFactory.GetProvider(modelType);
            return provider.Generate(modelType, this);
        }

        public static bool ShouldDisplayMember(MemberInfo member, bool hasDataContractAttribute)
        {
            var jsonIgnore = member.GetCustomAttribute<JsonIgnoreAttribute>();
            var xmlIgnore = member.GetCustomAttribute<XmlIgnoreAttribute>();
            var ignoreDataMember = member.GetCustomAttribute<IgnoreDataMemberAttribute>();
            var nonSerialized = member.GetCustomAttribute<NonSerializedAttribute>();
            var apiExplorerSetting = member.GetCustomAttribute<ApiExplorerSettingsAttribute>();

            var hasMemberAttribute = member.DeclaringType != null && (member.DeclaringType.IsEnum ?
                member.GetCustomAttribute<EnumMemberAttribute>() != null :
                member.GetCustomAttribute<DataMemberAttribute>() != null);

            // Display member only if all the followings are true:
            // no JsonIgnoreAttribute
            // no XmlIgnoreAttribute
            // no IgnoreDataMemberAttribute
            // no NonSerializedAttribute
            // no ApiExplorerSettingsAttribute with IgnoreApi set to true
            // no DataContractAttribute without DataMemberAttribute or EnumMemberAttribute
            return jsonIgnore == null &&
                xmlIgnore == null &&
                ignoreDataMember == null &&
                nonSerialized == null &&
                (apiExplorerSetting == null || !apiExplorerSetting.IgnoreApi) &&
                (!hasDataContractAttribute || hasMemberAttribute);
        }

        public string CreateDefaultDocumentation(Type type)
        {
            string documentation;
            if (ModelProviderFactory.DefaultTypeDocumentation.TryGetValue(type, out documentation))
            {
                return documentation;
            }
            if (DocumentationProvider != null)
            {
                documentation = DocumentationProvider.GetDocumentation(type);
            }

            return documentation;
        }

        
    }
}