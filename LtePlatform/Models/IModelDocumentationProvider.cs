using System;
using System.Reflection;

namespace LtePlatform.Models
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}