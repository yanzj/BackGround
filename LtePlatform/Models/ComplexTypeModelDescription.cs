using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace LtePlatform.Models
{
    public class ComplexTypeModelDescription : ModelDescription
    {
        public ComplexTypeModelDescription()
        {
            Properties = new Collection<ParameterDescription>();
        }

        public Collection<ParameterDescription> Properties { get; private set; }

        public override IList<ParameterDescription> GetParameterDescriptions()
        {
            return Properties;
        }
    }
}