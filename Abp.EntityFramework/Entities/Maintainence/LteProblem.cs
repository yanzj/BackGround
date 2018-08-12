using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Excel;

namespace Abp.EntityFramework.Entities.Maintainence
{
    [AutoMapFrom(typeof(StandarProblemExcel), typeof(ChoiceProblemExcel))]
    public class LteProblem : Entity
    {
        public string Type { get; set; }

        public string Body { get; set; }

        public string Keyword1 { get; set; }

        public string Keyword2 { get; set; }

        public int Choices { get; set; }

        public string ChoiceA { get; set; }

        public string ChoiceB { get; set; }

        public string ChoiceC { get; set; }

        public string ChoiceD { get; set; }

        public string ChoiceE { get; set; }

        public string ChoiceF { get; set; }

        public string Answer { get; set; }
    }
}
