﻿using Lte.Domain.Common;
using Lte.Domain.Regular;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Xml;
using Lte.Domain.Common.Types;

namespace TraceParser.Huawei
{
    public class HwTraceFileParser
    {
        private bool _isParseAsn = true;
        public readonly List<Msg0Block> Lstmsg = new List<Msg0Block>();
        private uint[] _parTraces;
        private readonly Dictionary<uint, string> _typeswitch = TypeswitchSt;
        private static readonly Dictionary<uint, string> TypeswitchSt;

        static HwTraceFileParser()
        {
            var dictionary = new Dictionary<uint, string>
            {
                {0x2010308, "DL_CCCH_Message"},
                {0x201030a, "DL_CCCH_Message"},
                {0x201030c, "DL_CCCH_Message"},
                {0x201030f, "DL_CCCH_Message"},
                {0x2010301, "DL_DCCH_Message"},
                {0x2010302, "DL_DCCH_Message"},
                {0x2010304, "DL_DCCH_Message"},
                {0x2010306, "DL_DCCH_Message"},
                {0x201030d, "DL_DCCH_Message"},
                {0x2010311, "DL_DCCH_Message"},
                {0x2010314, "DL_DCCH_Message"},
                {0x2010318, "DL_DCCH_Message"},
                {0x2010319, "DL_DCCH_Message"},
                {0x201031a, "DL_DCCH_Message"},
                {0x2010309, "UL_CCCH_Message"},
                {0x201030b, "UL_CCCH_Message"},
                {0x201030e, "UL_CCCH_Message"},
                {0x2010303, "UL_DCCH_Message"},
                {0x2010307, "UL_DCCH_Message"},
                {0x2010310, "UL_DCCH_Message"},
                {0x2010312, "UL_DCCH_Message"},
                {0x2010313, "UL_DCCH_Message"},
                {0x2010315, "UL_DCCH_Message"},
                {0x2010316, "UL_DCCH_Message"},
                {0x2010317, "UL_DCCH_Message"},
                {0x201031b, "UL_DCCH_Message"},
                {0x2010300, "UL_DCCH_Message"},
                {0x2020200, "S1AP_PDU"},
                {0x2020201, "S1AP_PDU"},
                {0x2020202, "S1AP_PDU"},
                {0x2020203, "S1AP_PDU"},
                {0x2020204, "S1AP_PDU"},
                {0x2020205, "S1AP_PDU"},
                {0x2020206, "S1AP_PDU"},
                {0x2020207, "S1AP_PDU"},
                {0x2020208, "S1AP_PDU"},
                {0x2020209, "S1AP_PDU"},
                {0x202020a, "S1AP_PDU"},
                {0x202020b, "S1AP_PDU"},
                {0x202020c, "S1AP_PDU"},
                {0x202020d, "S1AP_PDU"},
                {0x202020e, "S1AP_PDU"},
                {0x202020f, "S1AP_PDU"},
                {0x2020210, "S1AP_PDU"},
                {0x2020211, "S1AP_PDU"},
                {0x2020212, "S1AP_PDU"},
                {0x2020213, "S1AP_PDU"},
                {0x2020214, "S1AP_PDU"},
                {0x2020215, "S1AP_PDU"},
                {0x2020216, "S1AP_PDU"},
                {0x2020217, "S1AP_PDU"},
                {0x2020218, "S1AP_PDU"},
                {0x2020219, "S1AP_PDU"},
                {0x202021a, "S1AP_PDU"},
                {0x202021b, "S1AP_PDU"},
                {0x202021c, "S1AP_PDU"},
                {0x202021d, "S1AP_PDU"},
                {0x202021e, "S1AP_PDU"},
                {0x202021f, "S1AP_PDU"},
                {0x2020220, "S1AP_PDU"},
                {0x2020221, "S1AP_PDU"},
                {0x2020222, "S1AP_PDU"},
                {0x2020223, "S1AP_PDU"},
                {0x2020224, "S1AP_PDU"},
                {0x2020225, "S1AP_PDU"},
                {0x2020226, "S1AP_PDU"},
                {0x2020227, "S1AP_PDU"},
                {0x2020228, "S1AP_PDU"},
                {0x2020229, "S1AP_PDU"},
                {0x202022a, "S1AP_PDU"},
                {0x202022b, "S1AP_PDU"},
                {0x202022c, "S1AP_PDU"},
                {0x202022d, "S1AP_PDU"},
                {0x202022e, "S1AP_PDU"},
                {0x2030400, "X2AP_PDU"},
                {0x2030401, "X2AP_PDU"},
                {0x2030402, "X2AP_PDU"},
                {0x2030403, "X2AP_PDU"},
                {0x2030404, "X2AP_PDU"},
                {0x2030405, "X2AP_PDU"}
            };
            TypeswitchSt = dictionary;
        }

        private static uint BlockType(string type)
        {
            return BigEndianBitConverter.ToUInt32(Encoding.ASCII.GetBytes(type), 0);
        }

        public string FormatXml(XmlDocument xmldoc)
        {
            var sb = new StringBuilder();
            var w = new StringWriter(sb);
            XmlTextWriter writer2 = null;
            try
            {
                writer2 = new XmlTextWriter(w)
                {
                    Formatting = Formatting.Indented,
                    Indentation = 1,
                    IndentChar = '\t'
                };
                xmldoc.WriteTo(writer2);
            }
            finally
            {
                if (writer2 != null)
                {
                    writer2.Close();
                }
            }
            return sb.ToString();
        }

        private static void OnFileHeader()
        {
        }

        public void Parse(Stream stream)
        {
            Parse(stream, true);
        }

        public void Parse(string binfile)
        {
            using (var stream = UnzipToMemoryStream(binfile))
            {
                Parse(stream, true);
            }
        }

        private void Parse(Stream stream, bool isParseAsn, uint[] parTraces = null)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }
            var reader = new BigEndianBinaryReader(stream);
            var header = new HuaweiHeader
            {
                file_flag_ui4 = reader.ReadUInt32()
            };
            reader.ReadString(4);
            reader.ReadUInt32();
            header.fver_ui2 = reader.ReadUInt16();
            reader.ReadString(4);
            reader.ReadUInt32();
            header.ttyp_ui2 = reader.ReadUInt16();
            reader.ReadString(4);
            reader.ReadUInt32();
            header.fno_ui4 = reader.ReadUInt32();
            reader.ReadString(4);
            reader.ReadUInt32();
            header.ntyp_i1 = reader.ReadByte();
            reader.ReadString(4);
            reader.ReadUInt32();
            header.nver_s40 = reader.ReadString(40);
            reader.ReadString(4);
            reader.ReadUInt32();
            header.ndep_ui1 = reader.ReadSByte();
            OnFileHeader();

            _parTraces = parTraces;
            _isParseAsn = isParseAsn;
            var num = BlockType("msg0");
            while (!reader.Eof())
            {
                var num2 = reader.ReadUInt32();
                if (num2 == num)
                {
                    ParseMsg0(reader);
                }
                else
                {
                    Console.WriteLine("unknown block type as {0:X}, expect {1:X}, but {2:X}",
                        reader.BaseStream.Position, num, num2);
                    return;
                }
            }
        }

        private void ParseMsg0(BinaryReader reader)
        {
            string str;
            var item = new Msg0Block
            {
                typeid_ui4 = reader.ReadUInt32(),
                head_length_ui2 = reader.ReadUInt16(),
                callid_ui4 = reader.ReadUInt32(),
                cell_id_ui4 = reader.ReadUInt32(),
                year_ui2 = reader.ReadUInt16(),
                month_ui1 = reader.ReadSByte(),
                day_ui1 = reader.ReadSByte(),
                hour_ui1 = reader.ReadSByte(),
                minute_ui1 = reader.ReadSByte(),
                second_ui1 = reader.ReadSByte(),
                localcell_id_ui1 = reader.ReadSByte(),
                micro_second_ui4 = reader.ReadUInt32(),
                direction_ui1 = reader.ReadSByte(),
                reserved_ui1 = reader.ReadSByte()
            };
            reader.BaseStream.Seek(item.head_length_ui2 - 0x16, SeekOrigin.Current);
            item.body_length_ui2 = reader.ReadUInt16();
            var buffer = new byte[item.body_length_ui2];
            reader.Read(buffer, 0, item.body_length_ui2);
            item.setBody_bytes(buffer);
            _typeswitch.TryGetValue(item.typeid_ui4, out str);
            item.AsnParseClass = str;
            if ((_parTraces != null) && _parTraces.Contains(item.typeid_ui4))
            {
                if (_isParseAsn)
                {
                    item.ParseAsn();
                }
                Lstmsg.Add(item);
            }
            else if (_parTraces == null)
            {
                if (_isParseAsn)
                {
                    item.ParseAsn();
                }
                Lstmsg.Add(item);
            }
        }

        public static MemoryStream UnzipToMemoryStream(string binfile)
        {
            var destination = new MemoryStream();
            var stream = new FileStream(binfile, FileMode.Open, FileAccess.Read, FileShare.Read);
            using (var stream2 = new GZipStream(stream, CompressionMode.Decompress))
            {
                stream2.CopyTo(destination);
            }
            return destination;
        }
    }
}
