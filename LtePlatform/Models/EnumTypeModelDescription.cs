using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace LtePlatform.Models
{
    public class EnumTypeModelDescription : ModelDescription
    {
        public EnumTypeModelDescription()
        {
            Values = new Collection<EnumValueDescription>();
        }

        public Collection<EnumValueDescription> Values { get; private set; }

        public override IList<ParameterDescription> GetParameterDescriptions()
        {
            return null;
        }
    }
}