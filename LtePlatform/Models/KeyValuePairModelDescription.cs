using System.Collections.Generic;

namespace LtePlatform.Models
{
    public class KeyValuePairModelDescription : ModelDescription
    {
        public ModelDescription KeyModelDescription { get; set; }

        public ModelDescription ValueModelDescription { get; set; }

        public override IList<ParameterDescription> GetParameterDescriptions()
        {
            return null;
        }
    }
}