using System.Text;

namespace Lte.Domain.LinqToCsv.StreamDef
{
    public class CsvStreamLine
    {
        private readonly CsvStream _stream;

        private readonly CsvStreamBuffer _buffer;

        public CsvStreamLine(CsvStream stream, CsvStreamBuffer buffer)
        {
            _stream = stream;
            _buffer = buffer;
        }

        public bool Eos { get; set; }

        public bool Eol { get; private set; }

        private bool _previousWasCr;

        public bool GetNextItem(ref string itemString, int? itemLength = null)
        {
            itemString = null;
            if (Eol)
            {
                // previous item was last in line, start new line
                Eol = false;
                return false;
            }

            var itemFound = false;
            var quoted = false;
            var predata = true;
            var postdata = false;
            var item = new StringBuilder();

            var cnt = 0;
            while (true)
            {
                if (itemLength !=null && cnt >= itemLength.Value)
                {
                    itemString = item.ToString();
                    return true;
                }

                var c = _buffer.GetNextChar(true);
                cnt++;

                if (Eos)
                {
                    if (itemFound) { itemString = item.ToString(); }
                    return itemFound;
                }

                // Keep track of line number. 
                // Note that line breaks can happen within a quoted field, not just at the
                // end of a record.
                // Don't count 0D0A as two line breaks.
                if ((!_previousWasCr) && (c == '\x0A'))
                {
                    _stream.LineNbr++;
                }


                if (c == '\x0D') //'\r'
                {
                    _stream.LineNbr++;
                    _previousWasCr = true;
                }
                else
                {
                    _previousWasCr = false;
                }

                if ((postdata || !quoted) && (itemLength == null && c == _stream.SeparatorChar))
                {
                    if (_stream.IgnoreTrailingSeparatorChar)
                    {
                        var nC = _buffer.GetNextChar(false);
                        if ((nC == '\x0A' || nC == '\x0D'))
                            continue;
                    }
                    // end of item, return
                    if (itemFound) { itemString = item.ToString(); }
                    return true;
                }


                if ((predata || postdata || !quoted) && (c == '\x0A' || c == '\x0D'))
                {
                    // we are at the end of the line, eat newline characters and exit
                    Eol = true;
                    if (c == '\x0D' && _buffer.GetNextChar(false) == '\x0A')
                    {
                        // new line sequence is 0D0A
                        _buffer.GetNextChar(true);
                    }


                    if (itemFound) { itemString = item.ToString(); }
                    return true;
                }


                if (predata && c == ' ')
                    // whitespace preceeding data, discard
                    continue;


                if (predata && c == '"')
                {
                    // quoted data is starting
                    quoted = true;
                    predata = false;
                    itemFound = true;
                    continue;
                }


                if (predata)
                {
                    // data is starting without quotes
                    predata = false;
                    item.Append(c);
                    itemFound = true;
                    continue;
                }


                if (c == '"' && quoted)
                {
                    if (_buffer.GetNextChar(false) == '"')
                    {
                        // double quotes within quoted string means add a quote       
                        item.Append(_buffer.GetNextChar(true));
                    }
                    else
                    {
                        // end-quote reached
                        postdata = true;
                    }

                    continue;
                }


                // all cases covered, character must be data
                item.Append(c);
            }
        }

    }
}
