using System;
using System.Xml.Serialization;

namespace TraceParser.Zte
{
    public class FileHeader
    {
        public FileSender fileSender { get; set; }

        public TraceCollec traceCollec { get; set; }

        public class FileSender
        {
            [XmlAttribute(AttributeName = "elementDn")]
            public string elementDn { get; set; }

            [XmlAttribute(AttributeName = "elementType")]
            public string elementType { get; set; }
        }

        public class TraceCollec
        {
            [XmlAttribute(AttributeName = "beginTime", DataType = "dateTime")]
            public DateTime beginTime { get; set; }
        }
    }
}
