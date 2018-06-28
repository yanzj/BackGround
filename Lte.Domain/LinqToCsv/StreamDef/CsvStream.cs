using Lte.Domain.LinqToCsv.Description;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lte.Domain.LinqToCsv.StreamDef
{
    /// <summary>
    /// Based on code found at
    /// http://knab.ws/blog/index.php?/archives/3-CSV-file-parser-and-writer-in-C-Part-1.html
    /// and
    /// http://knab.ws/blog/index.php?/archives/10-CSV-file-parser-and-writer-in-C-Part-2.html
    /// </summary>
    public class CsvStream
    {
        public TextReader InStream { get; }

        private readonly TextWriter _outStream;

        public char SeparatorChar { get; }

        private readonly char[] _specialChars;

        public bool IgnoreTrailingSeparatorChar { get; }

        // Current line number in the file. Only used when reading a file, not when writing a file.

        public int LineNbr { get; set; }

        private readonly CsvStreamLine _line;
        private readonly CsvStreamBuffer _buffer;

        public CsvStream(TextReader inStream, TextWriter outStream, char separatorChar, bool ignoreTrailingSeparatorChar)
        {
            InStream = inStream;
            _outStream = outStream;
            SeparatorChar = separatorChar;
            IgnoreTrailingSeparatorChar = ignoreTrailingSeparatorChar;
            _specialChars = ("\"\x0A\x0D" + separatorChar).ToCharArray();
            LineNbr = 1;

            _buffer = new CsvStreamBuffer(this);
            _line = new CsvStreamLine(this, _buffer);
            _buffer.Line = _line;
        }

        public void WriteRow(List<string> row, bool quoteAllFields)
        {
            var firstItem = true;
            foreach (var item in row)
            {
                if (!firstItem) { _outStream.Write(SeparatorChar); }

                // If the item is null, don't write anything.
                if (item != null)
                {
                    // If user always wants quoting, or if the item has special chars
                    // (such as "), or if item is the empty string or consists solely of
                    // white space, surround the item with quotes.
                    if ((quoteAllFields ||
                        (item.IndexOfAny(_specialChars) > -1) ||
                        (item.Trim() == "")))
                    {
                        _outStream.Write("\"" + item.Replace("\"", "\"\"") + "\"");
                    }
                    else
                    {
                        _outStream.Write(item);
                    }
                }

                firstItem = false;
            }

            _outStream.WriteLine("");
        }

        /// <param name="row">
        /// Contains the values in the current row, in the order in which they 
        /// appear in the file.
        /// </param>
        /// <returns>
        /// True if a row was returned in parameter "row".
        /// False if no row returned. In that case, you're at the end of the file.
        /// </returns>
        public bool ReadRow(IDataRow row, List<int> charactersLength = null)
        {
            row.Clear();

            var i = 0;
            while (true)
            {
                // Number of the line where the item starts. Note that an item
                // can span multiple lines.
                var startingLineNbr = LineNbr;
                string item = null;
                var itemLength = charactersLength == null ? (int?)null : charactersLength.Skip(i).First();

                var moreAvailable = _line.GetNextItem(ref item, itemLength);
                if (charactersLength != null)
                {
                    if (!(charactersLength.Count() > i + 1))
                    {
                        if (moreAvailable)
                        {
                            row.Add(new DataRowItem(item, startingLineNbr));
                        }


                        if (!_line.Eol)
                        {
                            //line.AdvanceToEndOfLine();
                            moreAvailable = false;
                        }
                    }
                }

                if (!moreAvailable)
                {
                    return (row.Count > 0);
                }


                row.Add(new DataRowItem(item, startingLineNbr));


                i++;
            }
        }
        
    }

}
