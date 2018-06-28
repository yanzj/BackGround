using Abp.Reflection;
using System.Collections.Generic;
using System.Reflection;

namespace Lte.Evaluations.Policy
{
    public class MyAssemblyFinder : IAssemblyFinder
    {
        public List<Assembly> GetAllAssemblies()
        {
            return new List<Assembly>
            {
                Assembly.Load("Lte.Parameters"),
                Assembly.Load("Lte.Evaluations"),
                Assembly.Load("Abp.EntityFramework.Customize"),
                Assembly.Load("Lte.MySqlFramework"),
                Assembly.Load("Lte.Domain")
            };
        }
    }
}
