using Lte.Parameters.Entities.Basic;

namespace Lte.Evaluations.DataService.Basic
{
    public interface ICellPowerService
    {
        CellPower Query(int eNodebId, byte sectorId);
    }
}