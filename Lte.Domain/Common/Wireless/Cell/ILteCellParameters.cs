namespace Lte.Domain.Common.Wireless.Cell
{
    public interface ILteCellParameters<TShort, TInt>
    {
        byte BandClass { get; set; }

        short Pci { get; set; }

        TShort Prach { get; set; }

        TInt Tac { get; set; }

        int Frequency { get; set; }
    }
}