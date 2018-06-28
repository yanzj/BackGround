using System;
using Abp.Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Common.Wireless;
using Lte.Parameters.Entities;
using Lte.Parameters.Entities.Dt;
using Lte.Parameters.Entities.Kpi;
using Lte.Parameters.Entities.Neighbor;
using MongoDB.Bson;

namespace Lte.Parameters.Abstract.Infrastructure
{
    public interface IFileRecordRepository
    {
        IEnumerable<FileRecord4G> GetFileRecord4Gs(string fileName);

        IEnumerable<FileRecordVolte> GetFileRecordVoltes(string fileName);

        IEnumerable<FileRecord4G> GetFileRecord4Gs(string fileName, int rasterNum);

        IEnumerable<FileRecordVolte> GetFileRecordVoltes(string fileName, int rasterNum);

        IEnumerable<FileRecord3G> GetFileRecord3Gs(string fileName);

        IEnumerable<FileRecord3G> GetFileRecord3Gs(string fileName, int rasterNum);

        IEnumerable<FileRecord2G> GetFileRecord2Gs(string fileName);

        IEnumerable<FileRecord2G> GetFileRecord2Gs(string fileName, int rasterNum);

        int InsertFileRecord2Gs(IEnumerable<FileRecord2G> stats, string tableName);

        int InsertFileRecord3Gs(IEnumerable<FileRecord3G> stats, string tableName);

        int InsertFileRecord4Gs(IEnumerable<FileRecord4G> stats, string tableName);

        int InsertFileRecordVoltes(IEnumerable<FileRecordVolte> stats, string tableName);

        IEnumerable<string> GetTables();
    }

    public interface IInterferenceMatrixRepository : IRepository<InterferenceMatrixStat>
    {
        int SaveChanges();

        Task<int> UpdateItemsAsync(int eNodebId, byte sectorId, short destPci, int destENodebId, byte destSectorId);

        List<InterferenceMatrixStat> GetAllList(DateTime begin, DateTime end, int cellId, byte sectorId);

        List<InterferenceMatrixStat> GetAllVictims(DateTime begin, DateTime end, int cellId, byte sectorId);
    }

    public interface IEUtranRelationZteRepository : IRepository<EUtranRelationZte, ObjectId>
    {
        List<EUtranRelationZte> GetRecentList(int eNodebId, byte sectorId);

        List<EUtranRelationZte> GetRecentList(int eNodebId);

        EUtranRelationZte GetRecent(int eNodebId, int externalId);
    }

    public interface IEutranIntraFreqNCellRepository : IRepository<EutranIntraFreqNCell, ObjectId>
    {
        List<EutranIntraFreqNCell> GetRecentList(int eNodebId, byte localCellId);

        List<EutranIntraFreqNCell> GetReverseList(int destENodebId, byte destSectorId);

        List<EutranIntraFreqNCell> GetAllReverseList(int destENodebId, byte destSectorId);
    }

    public interface IEutranInterFreqNCellRepository : IRepository<EutranInterFreqNCell, ObjectId>
    {
        List<EutranInterFreqNCell> GetRecentList(int eNodebId, byte localCellId);

        List<EutranInterFreqNCell> GetReverseList(int destENodebId, byte destSectorId);

        List<EutranInterFreqNCell> GetAllReverseList(int destENodebId, byte destSectorId);
    }

    public interface IEutranInterNFreqRepository : IRepository<EutranInterNFreq, ObjectId>
    {
        List<EutranInterNFreq> GetRecentList(int eNodebId);

        List<EutranInterNFreq> GetRecentList(int eNodebId, int localCellId);
    }
}
