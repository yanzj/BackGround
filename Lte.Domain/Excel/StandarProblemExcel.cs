using System;
using Lte.Domain.Regular.Attributes;

namespace Lte.Domain.Excel
{
    public class StandarProblemExcel
    {
        [ExcelColumn("题目")]
        public string Problem { get; set; }

        public string[] Fields
            => Problem.Split(new[] {"A.", "B.", "C.", "D.", "E.", "F."}, StringSplitOptions.RemoveEmptyEntries);

        public int Choices => Fields.Length - 1;

        public string Body => Fields.Length > 0 ? Fields[0] : Problem;

        public string ChoiceA => Choices > 0 ? Fields[1] : "";

        public string ChoiceB => Choices > 1 ? Fields[2] : "";

        public string ChoiceC => Choices > 2 ? Fields[3] : "";

        public string ChoiceD => Choices > 3 ? Fields[4] : "";

        public string ChoiceE => Choices > 4 ? Fields[5] : "";

        public string ChoiceF => Choices > 5 ? Fields[6] : "";

        [ExcelColumn("参考答案")]
        public string AnswerContent { get; set; }

        public string Answer => AnswerContent.Replace(" ", "");

        [ExcelColumn("类型")]
        public string Type { get; set; }
    }
}