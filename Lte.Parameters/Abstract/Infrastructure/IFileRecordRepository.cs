using System.Collections.Generic;
using Lte.Parameters.Entities.Dt;

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
}
