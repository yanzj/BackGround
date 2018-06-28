using Lte.Domain.LinqToCsv.Description;
using Lte.Domain.LinqToCsv.Mapper;
using Lte.Domain.LinqToCsv.StreamDef;
using System.Collections.Generic;
using System.IO;

namespace Lte.Domain.LinqToCsv.Context
{
    public class FileDataAccess
    {
        private StreamReader _stream;

        public CsvFileDescription FileDescription { get; }

        public IDataRow Row { get; set; }

        public CsvStream Cs { get; }

        private AggregatedException Ae { get; set; }

        public FileDataAccess(StreamReader stream, CsvFileDescription fileDescription)
        {
            _stream = stream;
            FileDescription = fileDescription;
            Cs = new CsvStream(stream, null, fileDescription.SeparatorChar,
                fileDescription.IgnoreTrailingSeparatorChar);
        }

        public IEnumerable<T> ReadData<T>(string fileName) where T : class, new()
        {
            var reader = ReadDataPreparation<T>(fileName);
            if (typeof(IDataRow).IsAssignableFrom(typeof(T)))
            {                
                Row = new T() as IDataRow;
                return ReadRawData(reader, fileName);
            }
            Row = new DataRow();
            return ReadFieldData(reader, fileName);
        }

        public RowReader<T> ReadDataPreparation<T>(string fileName) where T : class, new()
        {
            RewindStream(fileName);
            Ae =
                new AggregatedException(typeof(T).ToString(), fileName, FileDescription.MaximumNbrExceptions);
            return new RowReader<T>(FileDescription, Ae);
        }

        public IEnumerable<T> ReadRawData<T>(RowReader<T> reader, string fileName) where T : class, new()
        {
            var firstRow = true;
            try
            {
                while (Cs.ReadRow(Row))
                {
                    if ((Row.Count == 1) && ((Row[0].Value == null) || (string.IsNullOrEmpty(Row[0].Value.Trim()))))
                    {
                        continue;
                    }

                    var readingResult = reader.ReadingOneRawRow(Row, firstRow);

                    if (readingResult) { yield return reader.Obj; }
                    firstRow = false;
                }
            }
            finally
            {
                if (!string.IsNullOrEmpty(fileName))
                {
                    _stream.Close();
                }

                Ae.ThrowIfExceptionsStored();
            }

        }

        public IEnumerable<T> ReadFieldData<T>(RowReader<T> reader, string fileName) where T : class, new()
        {
            var fm = new FieldMapperReading<T>(FileDescription, fileName, false);
            var charLengths = fm.GetCharLengths();
            return ReadFieldDataRows(reader, fileName, fm, charLengths);
        }

        public IEnumerable<T> ReadFieldDataRows<T>(RowReader<T> reader, string fileName,
            FieldMapperReading<T> fm, List<int> charLengths) where T : class, new()
        {
            var firstRow = true;

            try
            {
                while (Cs.ReadRow(Row, charLengths))
                {
                    if ((Row.Count == 1) && ((Row[0].Value == null) || (string.IsNullOrEmpty(Row[0].Value.Trim()))))
                    {
                        continue;
                    }

                    var readingResult = reader.ReadingOneFieldRow(fm, Row, firstRow);

                    if (readingResult) { yield return reader.Obj; }
                    firstRow = false;
                }
            }
            finally
            {
                if (!string.IsNullOrEmpty(fileName))
                {
                    _stream.Close();
                }

                Ae.ThrowIfExceptionsStored();
            }
        }

        private void RewindStream(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                _stream = new StreamReader(fileName, FileDescription.TextEncoding,
                    FileDescription.DetectEncodingFromByteOrderMarks);
            }
            else
            {
                if ((_stream == null) || (!_stream.BaseStream.CanSeek))
                {
                    throw new BadStreamException();
                }

                _stream.BaseStream.Seek(0, SeekOrigin.Begin);
            }
        }
    }
}
