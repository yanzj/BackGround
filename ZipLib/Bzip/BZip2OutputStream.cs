using System;
using System.IO;
using System.Runtime.InteropServices;
using ZipLib.CheckSums;

namespace ZipLib.Bzip
{
    public class BZip2OutputStream : Stream
    {
        private int _allowableBlockSize;
        private Stream _baseStream;
        private byte[] _block;
        private uint _blockCrc;
        private bool _blockRandomised;
        private readonly int _blockSize100K;
        private int _bsBuff;
        private int _bsLive;
        private uint _combinedCrc;
        private int _currentChar;
        private bool _disposed;
        private bool _firstAttempt;
        private int[] _ftab;
        private readonly int[] _increments;
        private readonly bool[] _inUse;
        private int _last;
        private readonly IChecksum _mCrc;
        private readonly int[] _mtfFreq;
        private int _nBlocksRandomised;
        private int _nInUse;
        private int _nMtf;
        private int _origPtr;
        private int[] _quadrant;
        private int _runLength;
        private readonly char[] _selector;
        private readonly char[] _selectorMtf;
        private readonly char[] _seqToUnseq;
        private short[] _szptr;
        private readonly char[] _unseqToSeq;
        private int _workDone;
        private readonly int _workFactor;
        private int _workLimit;
        private int[] _zptr;

        public BZip2OutputStream(Stream stream)
            : this(stream, 9)
        {
        }

        public BZip2OutputStream(Stream stream, int blockSize)
        {
            _increments 
                = new[] { 1, 4, 13, 40, 0x79, 0x16c, 0x445, 0xcd0, 0x2671, 0x7354, 0x159fd, 0x40df8, 0xc29e9, 0x247dbc };
            IsStreamOwner = true;
            _mCrc = new StrangeCRC();
            _inUse = new bool[0x100];
            _seqToUnseq = new char[0x100];
            _unseqToSeq = new char[0x100];
            _selector = new char[0x4652];
            _selectorMtf = new char[0x4652];
            _mtfFreq = new int[0x102];
            _currentChar = -1;
            BsSetStream(stream);
            _workFactor = 50;
            if (blockSize > 9)
            {
                blockSize = 9;
            }
            if (blockSize < 1)
            {
                blockSize = 1;
            }
            _blockSize100K = blockSize;
            AllocateCompressStructures();
            Initialize();
            InitBlock();
        }

        private void AllocateCompressStructures()
        {
            var num = BZip2Constants.BaseBlockSize * _blockSize100K;
            _block = new byte[(num + 1) + 20];
            _quadrant = new int[num + 20];
            _zptr = new int[num];
            _ftab = new int[0x10001];
            _szptr = new short[2 * num];
        }

        private void BsFinishedWithStream()
        {
            while (_bsLive > 0)
            {
                var num = _bsBuff >> 0x18;
                _baseStream.WriteByte((byte)num);
                _bsBuff = _bsBuff << 8;
                _bsLive -= 8;
                BytesWritten++;
            }
        }

        private void BsPutint(int u)
        {
            BsW(8, (u >> 0x18) & 0xff);
            BsW(8, (u >> 0x10) & 0xff);
            BsW(8, (u >> 8) & 0xff);
            BsW(8, u & 0xff);
        }

        private void BsPutIntVs(int numBits, int c)
        {
            BsW(numBits, c);
        }

        private void BsPutUChar(int c)
        {
            BsW(8, c);
        }

        private void BsSetStream(Stream stream)
        {
            _baseStream = stream;
            _bsLive = 0;
            _bsBuff = 0;
            BytesWritten = 0;
        }

        private void BsW(int n, int v)
        {
            while (_bsLive >= 8)
            {
                var num = _bsBuff >> 0x18;
                _baseStream.WriteByte((byte)num);
                _bsBuff = _bsBuff << 8;
                _bsLive -= 8;
                BytesWritten++;
            }
            _bsBuff |= v << ((0x20 - _bsLive) - n);
            _bsLive += n;
        }

        public override void Close()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                base.Dispose(disposing);
                if (!_disposed)
                {
                    _disposed = true;
                    if (_runLength > 0)
                    {
                        WriteRun();
                    }
                    _currentChar = -1;
                    EndBlock();
                    EndCompression();
                    Flush();
                }
            }
            finally
            {
                if (disposing && IsStreamOwner)
                {
                    _baseStream.Close();
                }
            }
        }

        private void DoReversibleTransformation()
        {
            _workLimit = _workFactor * _last;
            _workDone = 0;
            _blockRandomised = false;
            _firstAttempt = true;
            MainSort();
            if ((_workDone > _workLimit) && _firstAttempt)
            {
                RandomiseBlock();
                _workLimit = _workDone = 0;
                _blockRandomised = true;
                _firstAttempt = false;
                MainSort();
            }
            _origPtr = -1;
            for (var i = 0; i <= _last; i++)
            {
                if (_zptr[i] == 0)
                {
                    _origPtr = i;
                    break;
                }
            }
            if (_origPtr == -1)
            {
                Panic();
            }
        }

        private void EndBlock()
        {
            if (_last >= 0)
            {
                _blockCrc = (uint)_mCrc.Value;
                _combinedCrc = (_combinedCrc << 1) | (_combinedCrc >> 0x1f);
                _combinedCrc ^= _blockCrc;
                DoReversibleTransformation();
                BsPutUChar(0x31);
                BsPutUChar(0x41);
                BsPutUChar(0x59);
                BsPutUChar(0x26);
                BsPutUChar(0x53);
                BsPutUChar(0x59);
                BsPutint((int)_blockCrc);
                if (_blockRandomised)
                {
                    BsW(1, 1);
                    _nBlocksRandomised++;
                }
                else
                {
                    BsW(1, 0);
                }
                MoveToFrontCodeAndSend();
            }
        }

        private void EndCompression()
        {
            BsPutUChar(0x17);
            BsPutUChar(0x72);
            BsPutUChar(0x45);
            BsPutUChar(0x38);
            BsPutUChar(80);
            BsPutUChar(0x90);
            BsPutint((int)_combinedCrc);
            BsFinishedWithStream();
        }

        ~BZip2OutputStream()
        {
            Dispose(false);
        }

        public override void Flush()
        {
            _baseStream.Flush();
        }

        private bool FullGtU(int i1, int i2)
        {
            var num2 = _block[i1 + 1];
            var num3 = _block[i2 + 1];
            if (num2 != num3)
            {
                return (num2 > num3);
            }
            i1++;
            i2++;
            num2 = _block[i1 + 1];
            num3 = _block[i2 + 1];
            if (num2 != num3)
            {
                return (num2 > num3);
            }
            i1++;
            i2++;
            num2 = _block[i1 + 1];
            num3 = _block[i2 + 1];
            if (num2 != num3)
            {
                return (num2 > num3);
            }
            i1++;
            i2++;
            num2 = _block[i1 + 1];
            num3 = _block[i2 + 1];
            if (num2 != num3)
            {
                return (num2 > num3);
            }
            i1++;
            i2++;
            num2 = _block[i1 + 1];
            num3 = _block[i2 + 1];
            if (num2 != num3)
            {
                return (num2 > num3);
            }
            i1++;
            i2++;
            num2 = _block[i1 + 1];
            num3 = _block[i2 + 1];
            if (num2 != num3)
            {
                return (num2 > num3);
            }
            i1++;
            i2++;
            var num = _last + 1;
            do
            {
                num2 = _block[i1 + 1];
                num3 = _block[i2 + 1];
                if (num2 != num3)
                {
                    return (num2 > num3);
                }
                var num4 = _quadrant[i1];
                var num5 = _quadrant[i2];
                if (num4 != num5)
                {
                    return (num4 > num5);
                }
                i1++;
                i2++;
                num2 = _block[i1 + 1];
                num3 = _block[i2 + 1];
                if (num2 != num3)
                {
                    return (num2 > num3);
                }
                num4 = _quadrant[i1];
                num5 = _quadrant[i2];
                if (num4 != num5)
                {
                    return (num4 > num5);
                }
                i1++;
                i2++;
                num2 = _block[i1 + 1];
                num3 = _block[i2 + 1];
                if (num2 != num3)
                {
                    return (num2 > num3);
                }
                num4 = _quadrant[i1];
                num5 = _quadrant[i2];
                if (num4 != num5)
                {
                    return (num4 > num5);
                }
                i1++;
                i2++;
                num2 = _block[i1 + 1];
                num3 = _block[i2 + 1];
                if (num2 != num3)
                {
                    return (num2 > num3);
                }
                num4 = _quadrant[i1];
                num5 = _quadrant[i2];
                if (num4 != num5)
                {
                    return (num4 > num5);
                }
                i1++;
                i2++;
                if (i1 > _last)
                {
                    i1 -= _last;
                    i1--;
                }
                if (i2 > _last)
                {
                    i2 -= _last;
                    i2--;
                }
                num -= 4;
                _workDone++;
            }
            while (num >= 0);
            return false;
        }

        private void GenerateMtfValues()
        {
            int num;
            var chArray = new char[0x100];
            MakeMaps();
            var index = _nInUse + 1;
            for (num = 0; num <= index; num++)
            {
                _mtfFreq[num] = 0;
            }
            var num4 = 0;
            var num3 = 0;
            for (num = 0; num < _nInUse; num++)
            {
                chArray[num] = (char)num;
            }
            for (num = 0; num <= _last; num++)
            {
                var num2 = 0;
                var ch = chArray[num2];
                while (_unseqToSeq[_block[_zptr[num]]] != ch)
                {
                    num2++;
                    var ch2 = ch;
                    ch = chArray[num2];
                    chArray[num2] = ch2;
                }
                chArray[0] = ch;
                if (num2 == 0)
                {
                    num3++;
                    continue;
                }
                if (num3 <= 0)
                {
                    goto Label_0126;
                }
                num3--;
            Label_00A9:
                switch ((num3 % 2))
                {
                    case 0:
                        _szptr[num4] = 0;
                        num4++;
                        _mtfFreq[0]++;
                        break;

                    case 1:
                        _szptr[num4] = 1;
                        num4++;
                        _mtfFreq[1]++;
                        break;
                }
                if (num3 >= 2)
                {
                    num3 = (num3 - 2) / 2;
                    goto Label_00A9;
                }
                num3 = 0;
            Label_0126:
                _szptr[num4] = (short)(num2 + 1);
                num4++;
                _mtfFreq[num2 + 1]++;
            }
            if (num3 <= 0)
            {
                goto Label_01EC;
            }
            num3--;
        Label_0172:
            switch ((num3 % 2))
            {
                case 0:
                    _szptr[num4] = 0;
                    num4++;
                    _mtfFreq[0]++;
                    break;

                case 1:
                    _szptr[num4] = 1;
                    num4++;
                    _mtfFreq[1]++;
                    break;
            }
            if (num3 >= 2)
            {
                num3 = (num3 - 2) / 2;
                goto Label_0172;
            }
        Label_01EC:
            _szptr[num4] = (short)index;
            num4++;
            _mtfFreq[index]++;
            _nMtf = num4;
        }

        private static void HbAssignCodes(int[] code, char[] length, int minLen, int maxLen, int alphaSize)
        {
            var num = 0;
            for (var i = minLen; i <= maxLen; i++)
            {
                for (var j = 0; j < alphaSize; j++)
                {
                    if (length[j] == i)
                    {
                        code[j] = num;
                        num++;
                    }
                }
                num = num << 1;
            }
        }

        private static void HbMakeCodeLengths(char[] len, int[] freq, int alphaSize, int maxLen)
        {
            var numArray = new int[260];
            var numArray2 = new int[0x204];
            var numArray3 = new int[0x204];
            for (var i = 0; i < alphaSize; i++)
            {
                numArray2[i + 1] = ((freq[i] == 0) ? 1 : freq[i]) << 8;
            }
            while (true)
            {
                int num5;
                var index = alphaSize;
                var num2 = 0;
                numArray[0] = 0;
                numArray2[0] = 0;
                numArray3[0] = -2;
                for (var j = 1; j <= alphaSize; j++)
                {
                    numArray3[j] = -1;
                    num2++;
                    numArray[num2] = j;
                    var num9 = num2;
                    var num10 = numArray[num9];
                    while (numArray2[num10] < numArray2[numArray[num9 >> 1]])
                    {
                        numArray[num9] = numArray[num9 >> 1];
                        num9 = num9 >> 1;
                    }
                    numArray[num9] = num10;
                }
                if (num2 >= 260)
                {
                    Panic();
                }
                while (num2 > 1)
                {
                    var num3 = numArray[1];
                    numArray[1] = numArray[num2];
                    num2--;
                    var num11 = 1;
                    var num13 = numArray[num11];
                Label_00E7:
                    var num12 = num11 << 1;
                    if (num12 <= num2)
                    {
                        if ((num12 < num2) && (numArray2[numArray[num12 + 1]] < numArray2[numArray[num12]]))
                        {
                            num12++;
                        }
                        if (numArray2[num13] >= numArray2[numArray[num12]])
                        {
                            numArray[num11] = numArray[num12];
                            num11 = num12;
                            goto Label_00E7;
                        }
                    }
                    numArray[num11] = num13;
                    var num4 = numArray[1];
                    numArray[1] = numArray[num2];
                    num2--;
                    num11 = 1;
                    num13 = numArray[num11];
                Label_0155:
                    num12 = num11 << 1;
                    if (num12 <= num2)
                    {
                        if ((num12 < num2) && (numArray2[numArray[num12 + 1]] < numArray2[numArray[num12]]))
                        {
                            num12++;
                        }
                        if (numArray2[num13] >= numArray2[numArray[num12]])
                        {
                            numArray[num11] = numArray[num12];
                            num11 = num12;
                            goto Label_0155;
                        }
                    }
                    numArray[num11] = num13;
                    index++;
                    numArray3[num3] = numArray3[num4] = index;
                    numArray2[index] = ((int)((numArray2[num3] & 0xffffff00L) + (numArray2[num4] & 0xffffff00L))) | (1 + (((numArray2[num3] & 0xff) > (numArray2[num4] & 0xff)) ? (numArray2[num3] & 0xff) : (numArray2[num4] & 0xff)));
                    numArray3[index] = -1;
                    num2++;
                    numArray[num2] = index;
                    num11 = num2;
                    num13 = numArray[num11];
                    while (numArray2[num13] < numArray2[numArray[num11 >> 1]])
                    {
                        numArray[num11] = numArray[num11 >> 1];
                        num11 = num11 >> 1;
                    }
                    numArray[num11] = num13;
                }
                if (index >= 0x204)
                {
                    Panic();
                }
                var flag = false;
                for (var k = 1; k <= alphaSize; k++)
                {
                    num5 = 0;
                    var num6 = k;
                    while (numArray3[num6] >= 0)
                    {
                        num6 = numArray3[num6];
                        num5++;
                    }
                    len[k - 1] = (char)num5;
                    if (num5 > maxLen)
                    {
                        flag = true;
                    }
                }
                if (!flag)
                {
                    return;
                }
                for (var m = 1; m < alphaSize; m++)
                {
                    num5 = numArray2[m] >> 8;
                    num5 = 1 + (num5 / 2);
                    numArray2[m] = num5 << 8;
                }
            }
        }

        private void InitBlock()
        {
            _mCrc.Reset();
            _last = -1;
            for (var i = 0; i < 0x100; i++)
            {
                _inUse[i] = false;
            }
            _allowableBlockSize = (BZip2Constants.BaseBlockSize * _blockSize100K) - 20;
        }

        private void Initialize()
        {
            BytesWritten = 0;
            _nBlocksRandomised = 0;
            BsPutUChar(0x42);
            BsPutUChar(90);
            BsPutUChar(0x68);
            BsPutUChar(0x30 + _blockSize100K);
            _combinedCrc = 0;
        }

        private void MainSort()
        {
            int num;
            var numArray = new int[0x100];
            var numArray2 = new int[0x100];
            var flagArray = new bool[0x100];
            for (num = 0; num < 20; num++)
            {
                _block[(_last + num) + 2] = _block[(num % (_last + 1)) + 1];
            }
            for (num = 0; num <= (_last + 20); num++)
            {
                _quadrant[num] = 0;
            }
            _block[0] = _block[_last + 1];
            if (_last < 0xfa0)
            {
                for (num = 0; num <= _last; num++)
                {
                    _zptr[num] = num;
                }
                _firstAttempt = false;
                _workDone = _workLimit = 0;
                SimpleSort(0, _last, 0);
            }
            else
            {
                int num2;
                int num6;
                for (num = 0; num <= 0xff; num++)
                {
                    flagArray[num] = false;
                }
                for (num = 0; num <= 0x10000; num++)
                {
                    _ftab[num] = 0;
                }
                int index = _block[0];
                for (num = 0; num <= _last; num++)
                {
                    num6 = _block[num + 1];
                    _ftab[(index << 8) + num6]++;
                    index = num6;
                }
                for (num = 1; num <= 0x10000; num++)
                {
                    _ftab[num] += _ftab[num - 1];
                }
                index = _block[1];
                for (num = 0; num < _last; num++)
                {
                    num6 = _block[num + 2];
                    num2 = (index << 8) + num6;
                    index = num6;
                    _ftab[num2]--;
                    _zptr[_ftab[num2]] = num;
                }
                num2 = (_block[_last + 1] << 8) + _block[1];
                _ftab[num2]--;
                _zptr[_ftab[num2]] = _last;
                num = 0;
                while (num <= 0xff)
                {
                    numArray[num] = num;
                    num++;
                }
                var num9 = 1;
                do
                {
                    num9 = (3 * num9) + 1;
                }
                while (num9 <= 0x100);
                do
                {
                    num9 /= 3;
                    num = num9;
                    while (num <= 0xff)
                    {
                        var num8 = numArray[num];
                        num2 = num;
                        while ((_ftab[(numArray[num2 - num9] + 1) << 8] - _ftab[numArray[num2 - num9] << 8]) > (_ftab[(num8 + 1) << 8] - _ftab[num8 << 8]))
                        {
                            numArray[num2] = numArray[num2 - num9];
                            num2 -= num9;
                            if (num2 <= (num9 - 1))
                            {
                                break;
                            }
                        }
                        numArray[num2] = num8;
                        num++;
                    }
                }
                while (num9 != 1);
                for (num = 0; num <= 0xff; num++)
                {
                    var num3 = numArray[num];
                    num2 = 0;
                    while (num2 <= 0xff)
                    {
                        var num4 = (num3 << 8) + num2;
                        if ((_ftab[num4] & 0x200000) != 0x200000)
                        {
                            var loSt = _ftab[num4] & -2097153;
                            var hiSt = (_ftab[num4 + 1] & -2097153) - 1;
                            if (hiSt > loSt)
                            {
                                QSort3(loSt, hiSt, 2);
                                if ((_workDone > _workLimit) && _firstAttempt)
                                {
                                    return;
                                }
                            }
                            _ftab[num4] |= 0x200000;
                        }
                        num2++;
                    }
                    flagArray[num3] = true;
                    if (num < 0xff)
                    {
                        var num12 = _ftab[num3 << 8] & -2097153;
                        var num13 = (_ftab[(num3 + 1) << 8] & -2097153) - num12;
                        var num14 = 0;
                        while ((num13 >> num14) > 0xfffe)
                        {
                            num14++;
                        }
                        num2 = 0;
                        while (num2 < num13)
                        {
                            var num15 = _zptr[num12 + num2];
                            var num16 = num2 >> num14;
                            _quadrant[num15] = num16;
                            if (num15 < 20)
                            {
                                _quadrant[(num15 + _last) + 1] = num16;
                            }
                            num2++;
                        }
                        if (((num13 - 1) >> num14) > 0xffff)
                        {
                            Panic();
                        }
                    }
                    num2 = 0;
                    while (num2 <= 0xff)
                    {
                        numArray2[num2] = _ftab[(num2 << 8) + num3] & -2097153;
                        num2++;
                    }
                    num2 = _ftab[num3 << 8] & -2097153;
                    while (num2 < (_ftab[(num3 + 1) << 8] & -2097153))
                    {
                        index = _block[_zptr[num2]];
                        if (!flagArray[index])
                        {
                            _zptr[numArray2[index]] = (_zptr[num2] == 0) ? _last : (_zptr[num2] - 1);
                            numArray2[index]++;
                        }
                        num2++;
                    }
                    for (num2 = 0; num2 <= 0xff; num2++)
                    {
                        _ftab[(num2 << 8) + num3] |= 0x200000;
                    }
                }
            }
        }

        private void MakeMaps()
        {
            _nInUse = 0;
            for (var i = 0; i < 0x100; i++)
            {
                if (_inUse[i])
                {
                    _seqToUnseq[_nInUse] = (char)i;
                    _unseqToSeq[i] = (char)_nInUse;
                    _nInUse++;
                }
            }
        }

        private static byte Med3(byte a, byte b, byte c)
        {
            byte num;
            if (a > b)
            {
                num = a;
                a = b;
                b = num;
            }
            if (b > c)
            {
                b = c;
            }
            if (a > b)
            {
                b = a;
            }
            return b;
        }

        private void MoveToFrontCodeAndSend()
        {
            BsPutIntVs(0x18, _origPtr);
            GenerateMtfValues();
            SendMtfValues();
        }

        private static void Panic()
        {
            throw new BZip2Exception("BZip2 output stream panic");
        }

        private void QSort3(int loSt, int hiSt, int dSt)
        {
            var elementArray = new StackElement[0x3e8];
            var index = 0;
            elementArray[index].ll = loSt;
            elementArray[index].hh = hiSt;
            elementArray[index].dd = dSt;
            index++;
            while (index > 0)
            {
                int num3;
                int num4;
                int num6;
                if (index >= 0x3e8)
                {
                    Panic();
                }
                index--;
                var ll = elementArray[index].ll;
                var hh = elementArray[index].hh;
                var dd = elementArray[index].dd;
                if (((hh - ll) < 20) || (dd > 10))
                {
                    SimpleSort(ll, hh, dd);
                    if ((_workDone > _workLimit) && _firstAttempt)
                    {
                        return;
                    }
                    continue;
                }
                int num5 = Med3(_block[(_zptr[ll] + dd) + 1], _block[(_zptr[hh] + dd) + 1], _block[(_zptr[(ll + hh) >> 1] + dd) + 1]);
                var num = num3 = ll;
                var num2 = num4 = hh;
            Label_0118:
                if (num <= num2)
                {
                    num6 = _block[(_zptr[num] + dd) + 1] - num5;
                    if (num6 == 0)
                    {
                        var num12 = _zptr[num];
                        _zptr[num] = _zptr[num3];
                        _zptr[num3] = num12;
                        num3++;
                        num++;
                        goto Label_0118;
                    }
                    if (num6 <= 0)
                    {
                        num++;
                        goto Label_0118;
                    }
                }
            Label_0172:
                if (num <= num2)
                {
                    num6 = _block[(_zptr[num2] + dd) + 1] - num5;
                    if (num6 == 0)
                    {
                        var num13 = _zptr[num2];
                        _zptr[num2] = _zptr[num4];
                        _zptr[num4] = num13;
                        num4--;
                        num2--;
                        goto Label_0172;
                    }
                    if (num6 >= 0)
                    {
                        num2--;
                        goto Label_0172;
                    }
                }
                if (num <= num2)
                {
                    var num14 = _zptr[num];
                    _zptr[num] = _zptr[num2];
                    _zptr[num2] = num14;
                    num++;
                    num2--;
                    goto Label_0118;
                }
                if (num4 < num3)
                {
                    elementArray[index].ll = ll;
                    elementArray[index].hh = hh;
                    elementArray[index].dd = dd + 1;
                    index++;
                }
                else
                {
                    num6 = ((num3 - ll) < (num - num3)) ? (num3 - ll) : (num - num3);
                    Vswap(ll, num - num6, num6);
                    var n = ((hh - num4) < (num4 - num2)) ? (hh - num4) : (num4 - num2);
                    Vswap(num, (hh - n) + 1, n);
                    num6 = ((ll + num) - num3) - 1;
                    n = (hh - (num4 - num2)) + 1;
                    elementArray[index].ll = ll;
                    elementArray[index].hh = num6;
                    elementArray[index].dd = dd;
                    index++;
                    elementArray[index].ll = num6 + 1;
                    elementArray[index].hh = n - 1;
                    elementArray[index].dd = dd + 1;
                    index++;
                    elementArray[index].ll = n;
                    elementArray[index].hh = hh;
                    elementArray[index].dd = dd;
                    index++;
                }
            }
        }

        private void RandomiseBlock()
        {
            int num;
            var num2 = 0;
            var index = 0;
            for (num = 0; num < 0x100; num++)
            {
                _inUse[num] = false;
            }
            for (num = 0; num <= _last; num++)
            {
                if (num2 == 0)
                {
                    num2 = BZip2Constants.RandomNumbers[index];
                    index++;
                    if (index == 0x200)
                    {
                        index = 0;
                    }
                }
                num2--;
                _block[num + 1] = (byte)(_block[num + 1] ^ ((num2 == 1) ? 1 : 0));
                _block[num + 1] = (byte)(_block[num + 1] & 0xff);
                _inUse[_block[num + 1]] = true;
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException("BZip2OutputStream Read not supported");
        }

        public override int ReadByte()
        {
            throw new NotSupportedException("BZip2OutputStream ReadByte not supported");
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException("BZip2OutputStream Seek not supported");
        }

        private void SendMtfValues()
        {
            int num3;
            int num13;
            var chArray = new char[6][];
            for (var i = 0; i < 6; i++)
            {
                chArray[i] = new char[0x102];
            }
            var index = 0;
            var alphaSize = _nInUse + 2;
            for (var j = 0; j < 6; j++)
            {
                for (var num15 = 0; num15 < alphaSize; num15++)
                {
                    chArray[j][num15] = '\x000f';
                }
            }
            if (_nMtf <= 0)
            {
                Panic();
            }
            if (_nMtf < 200)
            {
                num13 = 2;
            }
            else if (_nMtf < 600)
            {
                num13 = 3;
            }
            else if (_nMtf < 0x4b0)
            {
                num13 = 4;
            }
            else if (_nMtf < 0x960)
            {
                num13 = 5;
            }
            else
            {
                num13 = 6;
            }
            var num16 = num13;
            var num2 = 0;
            while (num16 > 0)
            {
                var num18 = _nMtf / num16;
                var num19 = 0;
                num3 = num2 - 1;
                while ((num19 < num18) && (num3 < (alphaSize - 1)))
                {
                    num3++;
                    num19 += _mtfFreq[num3];
                }
                if (((num3 > num2) && (num16 != num13)) && ((num16 != 1) && (((num13 - num16) % 2) == 1)))
                {
                    num19 -= _mtfFreq[num3];
                    num3--;
                }
                for (var num20 = 0; num20 < alphaSize; num20++)
                {
                    if ((num20 >= num2) && (num20 <= num3))
                    {
                        chArray[num16 - 1][num20] = '\0';
                    }
                    else
                    {
                        chArray[num16 - 1][num20] = '\x000f';
                    }
                }
                num16--;
                num2 = num3 + 1;
                _nMtf -= num19;
            }
            var numArray = new int[6][];
            for (var k = 0; k < 6; k++)
            {
                numArray[k] = new int[0x102];
            }
            var numArray2 = new int[6];
            var numArray3 = new short[6];
            for (var m = 0; m < 4; m++)
            {
                for (var num22 = 0; num22 < num13; num22++)
                {
                    numArray2[num22] = 0;
                }
                for (var num23 = 0; num23 < num13; num23++)
                {
                    for (var num24 = 0; num24 < alphaSize; num24++)
                    {
                        numArray[num23][num24] = 0;
                    }
                }
                index = 0;
                num2 = 0;
                while (true)
                {
                    if (num2 >= _nMtf)
                    {
                        break;
                    }
                    num3 = (num2 + 50) - 1;
                    if (num3 >= _nMtf)
                    {
                        num3 = _nMtf - 1;
                    }
                    for (var num25 = 0; num25 < num13; num25++)
                    {
                        numArray3[num25] = 0;
                    }
                    if (num13 == 6)
                    {
                        short num27;
                        short num28;
                        short num29;
                        short num30;
                        short num31;
                        var num26 = num27 = num28 = num29 = num30 = num31 = 0;
                        for (var num32 = num2; num32 <= num3; num32++)
                        {
                            var num33 = _szptr[num32];
                            num26 = (short)(num26 + ((short)chArray[0][num33]));
                            num27 = (short)(num27 + ((short)chArray[1][num33]));
                            num28 = (short)(num28 + ((short)chArray[2][num33]));
                            num29 = (short)(num29 + ((short)chArray[3][num33]));
                            num30 = (short)(num30 + ((short)chArray[4][num33]));
                            num31 = (short)(num31 + ((short)chArray[5][num33]));
                        }
                        numArray3[0] = num26;
                        numArray3[1] = num27;
                        numArray3[2] = num28;
                        numArray3[3] = num29;
                        numArray3[4] = num30;
                        numArray3[5] = num31;
                    }
                    else
                    {
                        for (var num34 = num2; num34 <= num3; num34++)
                        {
                            var num35 = _szptr[num34];
                            for (var num36 = 0; num36 < num13; num36++)
                            {
                                numArray3[num36] = (short)(numArray3[num36] + ((short)chArray[num36][num35]));
                            }
                        }
                    }
                    var num6 = 0x3b9ac9ff;
                    var num5 = -1;
                    for (var num37 = 0; num37 < num13; num37++)
                    {
                        if (numArray3[num37] < num6)
                        {
                            num6 = numArray3[num37];
                            num5 = num37;
                        }
                    }
                    numArray2[num5]++;
                    _selector[index] = (char)num5;
                    index++;
                    for (var num38 = num2; num38 <= num3; num38++)
                    {
                        numArray[num5][_szptr[num38]]++;
                    }
                    num2 = num3 + 1;
                }
                for (var num39 = 0; num39 < num13; num39++)
                {
                    HbMakeCodeLengths(chArray[num39], numArray[num39], alphaSize, 20);
                }
            }
            if (num13 >= 8)
            {
                Panic();
            }
            if ((index >= 0x8000) || (index > 0x4652))
            {
                Panic();
            }
            var chArray2 = new char[6];
            for (var n = 0; n < num13; n++)
            {
                chArray2[n] = (char)n;
            }
            for (var num41 = 0; num41 < index; num41++)
            {
                var num42 = 0;
                var ch3 = chArray2[num42];
                while (_selector[num41] != ch3)
                {
                    num42++;
                    var ch2 = ch3;
                    ch3 = chArray2[num42];
                    chArray2[num42] = ch2;
                }
                chArray2[0] = ch3;
                _selectorMtf[num41] = (char)num42;
            }
            var numArray4 = new int[6][];
            for (var num43 = 0; num43 < 6; num43++)
            {
                numArray4[num43] = new int[0x102];
            }
            for (var num44 = 0; num44 < num13; num44++)
            {
                var minLen = 0x20;
                var maxLen = 0;
                for (var num45 = 0; num45 < alphaSize; num45++)
                {
                    if (chArray[num44][num45] > maxLen)
                    {
                        maxLen = chArray[num44][num45];
                    }
                    if (chArray[num44][num45] < minLen)
                    {
                        minLen = chArray[num44][num45];
                    }
                }
                if (maxLen > 20)
                {
                    Panic();
                }
                if (minLen < 1)
                {
                    Panic();
                }
                HbAssignCodes(numArray4[num44], chArray[num44], minLen, maxLen, alphaSize);
            }
            var flagArray = new bool[0x10];
            for (var num46 = 0; num46 < 0x10; num46++)
            {
                flagArray[num46] = false;
                for (var num47 = 0; num47 < 0x10; num47++)
                {
                    if (_inUse[(num46 * 0x10) + num47])
                    {
                        flagArray[num46] = true;
                    }
                }
            }
            for (var num48 = 0; num48 < 0x10; num48++)
            {
                if (flagArray[num48])
                {
                    BsW(1, 1);
                }
                else
                {
                    BsW(1, 0);
                }
            }
            for (var num49 = 0; num49 < 0x10; num49++)
            {
                if (flagArray[num49])
                {
                    for (var num50 = 0; num50 < 0x10; num50++)
                    {
                        if (_inUse[(num49 * 0x10) + num50])
                        {
                            BsW(1, 1);
                        }
                        else
                        {
                            BsW(1, 0);
                        }
                    }
                }
            }
            BsW(3, num13);
            BsW(15, index);
            for (var num51 = 0; num51 < index; num51++)
            {
                for (var num52 = 0; num52 < _selectorMtf[num51]; num52++)
                {
                    BsW(1, 1);
                }
                BsW(1, 0);
            }
            for (var num53 = 0; num53 < num13; num53++)
            {
                int v = chArray[num53][0];
                BsW(5, v);
                var num55 = 0;
                goto Label_0691;
            Label_064F:
                BsW(2, 2);
                v++;
            Label_065D:
                if (v < chArray[num53][num55])
                {
                    goto Label_064F;
                }
                while (v > chArray[num53][num55])
                {
                    BsW(2, 3);
                    v--;
                }
                BsW(1, 0);
                num55++;
            Label_0691:
                if (num55 < alphaSize)
                {
                    goto Label_065D;
                }
            }
            var num12 = 0;
            num2 = 0;
            while (true)
            {
                if (num2 >= _nMtf)
                {
                    break;
                }
                num3 = (num2 + 50) - 1;
                if (num3 >= _nMtf)
                {
                    num3 = _nMtf - 1;
                }
                for (var num56 = num2; num56 <= num3; num56++)
                {
                    BsW(chArray[_selector[num12]][_szptr[num56]], numArray4[_selector[num12]][_szptr[num56]]);
                }
                num2 = num3 + 1;
                num12++;
            }
            if (num12 != index)
            {
                Panic();
            }
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException("BZip2OutputStream SetLength not supported");
        }

        private void SimpleSort(int lo, int hi, int d)
        {
            var num4 = (hi - lo) + 1;
            if (num4 >= 2)
            {
                var index = 0;
                while (_increments[index] < num4)
                {
                    index++;
                }
                index--;
                while (index >= 0)
                {
                    var num3 = _increments[index];
                    var num = lo + num3;
                    do
                    {
                        if (num > hi)
                        {
                            goto Label_0160;
                        }
                        var num6 = _zptr[num];
                        var num2 = num;
                        while (FullGtU(_zptr[num2 - num3] + d, num6 + d))
                        {
                            _zptr[num2] = _zptr[num2 - num3];
                            num2 -= num3;
                            if (num2 <= ((lo + num3) - 1))
                            {
                                break;
                            }
                        }
                        _zptr[num2] = num6;
                        num++;
                        if (num > hi)
                        {
                            goto Label_0160;
                        }
                        num6 = _zptr[num];
                        num2 = num;
                        while (FullGtU(_zptr[num2 - num3] + d, num6 + d))
                        {
                            _zptr[num2] = _zptr[num2 - num3];
                            num2 -= num3;
                            if (num2 <= ((lo + num3) - 1))
                            {
                                break;
                            }
                        }
                        _zptr[num2] = num6;
                        num++;
                        if (num > hi)
                        {
                            goto Label_0160;
                        }
                        num6 = _zptr[num];
                        num2 = num;
                        while (FullGtU(_zptr[num2 - num3] + d, num6 + d))
                        {
                            _zptr[num2] = _zptr[num2 - num3];
                            num2 -= num3;
                            if (num2 <= ((lo + num3) - 1))
                            {
                                break;
                            }
                        }
                        _zptr[num2] = num6;
                        num++;
                    }
                    while ((_workDone <= _workLimit) || !_firstAttempt);
                    return;
                Label_0160:
                    index--;
                }
            }
        }

        private void Vswap(int p1, int p2, int n)
        {
            while (n > 0)
            {
                var num = _zptr[p1];
                _zptr[p1] = _zptr[p2];
                _zptr[p2] = num;
                p1++;
                p2++;
                n--;
            }
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer");
            }
            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException("offset");
            }
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException("count");
            }
            if ((buffer.Length - offset) < count)
            {
                throw new ArgumentException("Offset/count out of range");
            }
            for (var i = 0; i < count; i++)
            {
                WriteByte(buffer[offset + i]);
            }
        }

        public override void WriteByte(byte value)
        {
            var num = (0x100 + value) % 0x100;
            if (_currentChar != -1)
            {
                if (_currentChar != num)
                {
                    WriteRun();
                    _runLength = 1;
                    _currentChar = num;
                }
                else
                {
                    _runLength++;
                    if (_runLength > 0xfe)
                    {
                        WriteRun();
                        _currentChar = -1;
                        _runLength = 0;
                    }
                }
            }
            else
            {
                _currentChar = num;
                _runLength++;
            }
        }

        private void WriteRun()
        {
            if (_last < _allowableBlockSize)
            {
                _inUse[_currentChar] = true;
                for (var i = 0; i < _runLength; i++)
                {
                    _mCrc.Update(_currentChar);
                }
                switch (_runLength)
                {
                    case 1:
                        _last++;
                        _block[_last + 1] = (byte)_currentChar;
                        return;

                    case 2:
                        _last++;
                        _block[_last + 1] = (byte)_currentChar;
                        _last++;
                        _block[_last + 1] = (byte)_currentChar;
                        return;

                    case 3:
                        _last++;
                        _block[_last + 1] = (byte)_currentChar;
                        _last++;
                        _block[_last + 1] = (byte)_currentChar;
                        _last++;
                        _block[_last + 1] = (byte)_currentChar;
                        return;
                }
                _inUse[_runLength - 4] = true;
                _last++;
                _block[_last + 1] = (byte)_currentChar;
                _last++;
                _block[_last + 1] = (byte)_currentChar;
                _last++;
                _block[_last + 1] = (byte)_currentChar;
                _last++;
                _block[_last + 1] = (byte)_currentChar;
                _last++;
                _block[_last + 1] = (byte)(_runLength - 4);
            }
            else
            {
                EndBlock();
                InitBlock();
                WriteRun();
            }
        }

        public int BytesWritten { get; private set; }

        public override bool CanRead => false;

        public override bool CanSeek => false;

        public override bool CanWrite => _baseStream.CanWrite;

        public bool IsStreamOwner { get; set; }

        public override long Length => _baseStream.Length;

        public override long Position
        {
            get
            {
                return _baseStream.Position;
            }
            set
            {
                throw new NotSupportedException("BZip2OutputStream position cannot be set");
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct StackElement
        {
            public int ll;
            public int hh;
            public int dd;
        }
    }
}

