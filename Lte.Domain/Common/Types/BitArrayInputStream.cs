using System;
using System.IO;
using System.Text;

namespace Lte.Domain.Common.Types
{
    public sealed class BitArrayInputStream : Stream, IBitArrayReader
    {
        public long BitPosition { get; set; }

        private readonly Stream _byteStream;
        private int _currentBit;
        private int _currentByte;
        private readonly StringBuilder _haveReadBin = new StringBuilder();
        private readonly byte[] _streamArray;

        public BitArrayInputStream(Stream byteStream)
        {
            _streamArray = new byte[byteStream.Length];
            byteStream.Read(_streamArray, 0, _streamArray.Length);
            byteStream.Position = 0L;
            BitPosition = 0L;
            _byteStream = byteStream;
        }

        private string DecimalToBinary(int decimalNum)
        {
            string str = Convert.ToString(decimalNum, 2);
            int num = 8 - str.Length;
            for (int i = 0; i < num; i++)
            {
                str = '0' + str;
            }
            return str;
        }

        public override void Flush()
        {
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (_currentBit == 0)
            {
                return _byteStream.Read(buffer, offset, count);
            }
            int index = 0;
            while (((index < buffer.Length) && (index < _byteStream.Length)) && (index < count))
            {
                buffer[index] = (byte)ReadByte();
                index++;
            }
            return index;
        }

        public int ReadBit()
        {
            if (BitPosition >= bitLength)
            {
                throw new Exception("越界");
            }
            if (_currentBit == 0)
            {
                _currentByte = _byteStream.ReadByte();
            }
            _currentBit++;
            int num = (_currentByte >> (8 - _currentBit)) & 1;
            if (_currentBit > 7)
            {
                _currentBit = 0;
            }
            BitPosition += 1L;
            return num;
        }

        public int ReadBits(int nBits)
        {
            int num = 0;
            for (int i = 0; (i < nBits) && (i <= 0x20); i++)
            {
                num = (num << 1) | ReadBit();
            }
            return num;
        }

        public void ReadBits(byte[] buffer, int offset, int bits)
        {
            int count = bits / 8;
            int nBits = bits % 8;
            Read(buffer, offset, count);
            if (nBits > 0)
            {
                buffer[count] = (byte)(ReadBits(nBits) << (8 - nBits));
            }
        }

        public string ReadBitString(int nBits)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < nBits; i++)
            {
                builder.Append(ReadBit());
            }
            return (builder + "'B");
        }

        public override int ReadByte()
        {
            if (_currentBit == 0)
            {
                return _byteStream.ReadByte();
            }
            int num = _byteStream.ReadByte();
            int num2 = ((_currentByte << _currentBit) | (num >> (8 - _currentBit))) & 0xff;
            _currentByte = num;
            return num2;
        }

        public string readByteString(int nBytes)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < nBytes; i++)
            {
                builder.Append((char)ReadByte());
            }
            return (builder + "'B");
        }

        public string readOctetString(int nBits)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < nBits; i++)
            {
                builder.Append(ReadBits(8).ToString("X2"));
            }
            return (builder + "'H");
        }

        public void Reverse()
        {
            _byteStream.Position = 0L;
            BitPosition = 0L;
            _currentBit = 0;
            _currentByte = 0;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return -1L;
        }

        public override void SetLength(long value)
        {
        }

        public void skipUnreadedBits()
        {
            _currentBit = 0;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
        }

        public Stream BaseStream
        {
            get
            {
                return _byteStream;
            }
        }

        public string BinStr
        {
            get
            {
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < _streamArray.Length; i++)
                {
                    byte decimalNum = _streamArray[i];
                    string str = string.Format("{0}[{1}]:{2}", i, decimalNum.ToString("X2"), DecimalToBinary(decimalNum));
                    builder.AppendLine(str);
                }
                return builder.ToString();
            }
        }

        public long bitLength
        {
            get
            {
                return (_byteStream.Length * 8L);
            }
        }

        public override bool CanRead
        {
            get
            {
                return (BitPosition < bitLength);
            }
        }

        public override bool CanSeek
        {
            get
            {
                return false;
            }
        }

        public override bool CanWrite
        {
            get
            {
                return false;
            }
        }

        public string HaveReadBin
        {
            get
            {
                return _haveReadBin.ToString();
            }
        }

        public string HexStr
        {
            get
            {
                StringBuilder builder = new StringBuilder();
                foreach (byte num in _streamArray)
                {
                    builder.Append(num.ToString("X2"));
                }
                return builder.ToString();
            }
        }

        public override long Length
        {
            get
            {
                return _byteStream.Length;
            }
        }

        public override long Position
        {
            get
            {
                return _byteStream.Position;
            }
            set
            {
                _byteStream.Position = value;
            }
        }
    }
}