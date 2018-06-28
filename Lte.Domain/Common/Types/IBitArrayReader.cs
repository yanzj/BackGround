namespace Lte.Domain.Common.Types
{
    public interface IBitArrayReader
    {
        int ReadBit();

        int ReadBits(int nBits);

        string ReadBitString(int nBits);
    }
}