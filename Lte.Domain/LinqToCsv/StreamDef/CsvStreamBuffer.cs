namespace Lte.Domain.LinqToCsv.StreamDef
{
    public class CsvStreamBuffer
    {
        private readonly char[] _buffer = new char[4096];
        private int _pos;
        private int _length;

        private readonly CsvStream _stream;
        private CsvStreamLine _line;

        public CsvStreamLine Line
        {
            set { _line = value; }
        }

        public CsvStreamBuffer(CsvStream stream)
        {
            _stream = stream;
        }

        public char GetNextChar(bool eat)
        {
            if (_pos < _length) return eat ? _buffer[_pos++] : _buffer[_pos];
            _length = _stream.InStream.ReadBlock(_buffer, 0, _buffer.Length);
            if (_length == 0)
            {
                _line.Eos = true;
                return '\0';
            }
            _pos = 0;
            return eat ? _buffer[_pos++] : _buffer[_pos];
        }
    }
}
