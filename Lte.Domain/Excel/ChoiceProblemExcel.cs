using System;
using Lte.Domain.Regular.Attributes;

namespace Lte.Domain.Excel
{
    public class ChoiceProblemExcel
    {
        [ExcelColumn("题目")]
        public string Body { get; set; }

        [ExcelColumn("选项")]
        public string Selection { get; set; }

        public string[] Fields
            => Selection.Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Split(new[]
                    {
                        "A.", "A）", ";", "A、", "A:",
                        "B.", "B）", "B、", "B:", "；",
                        "C.", "C）", "C、", "C:",
                        "D.", "D）", "D、", "D:",
                        "E.", "E）", "E、", "E:",
                        "F）", "F.", "F、", "F:"
                    },
                    StringSplitOptions.RemoveEmptyEntries);

        public int Choices => Fields.Length;

        public string ChoiceA => Choices > 0 ? Fields[0] : "";

        public string ChoiceB => Choices > 1 ? Fields[1] : "";

        public string ChoiceC => Choices > 2 ? Fields[2] : "";

        public string ChoiceD => Choices > 3 ? Fields[3] : "";

        public string ChoiceE => Choices > 4 ? Fields[4] : "";

        public string ChoiceF => Choices > 5 ? Fields[5] : "";

        [ExcelColumn("参考答案")]
        public string AnswerContent { get; set; }

        public string Answer => AnswerContent.Replace(" ", "");

        [ExcelColumn("类型")]
        public string Type { get; set; }
    }
}