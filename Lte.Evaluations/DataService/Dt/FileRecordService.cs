using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Abstract.Test;
using Lte.Parameters.Abstract.Dt;
using Lte.Parameters.Entities.Dt;

namespace Lte.Parameters.Concrete.Dt
{
    public class FileRecordService : IFileRecordService
    {
        private readonly IFileRecord2GRepository _fileRecord2GRepository;
        private readonly IFileRecord3GRepository _fileRecord3GRepository;
        private readonly IFileRecord4GRepository _fileRecord4GRepository;
        private readonly IFileRecordVolteRepository _fileRecordVolteRepository;

        private readonly IDtFileInfoRepository _dtFileInfoRepository;

        public FileRecordService(IFileRecord2GRepository fileRecord2GRepository,
            IFileRecord3GRepository fileRecord3GRepository, IFileRecord4GRepository fileRecord4GRepository,
            IFileRecordVolteRepository fileRecordVolteRepository, IDtFileInfoRepository dtFileInfoRepository)
        {
            _fileRecord2GRepository = fileRecord2GRepository;
            _fileRecord3GRepository = fileRecord3GRepository;
            _fileRecord4GRepository = fileRecord4GRepository;
            _fileRecordVolteRepository = fileRecordVolteRepository;
            _dtFileInfoRepository = dtFileInfoRepository;
        }
        
        public IEnumerable<FileRecord4G> GetFileRecord4Gs(string fileName)
        {
            var file = _dtFileInfoRepository.FirstOrDefault(x => x.CsvFileName == fileName + ".csv");
            return file == null
                ? new List<FileRecord4G>()
                : _fileRecord4GRepository.GetAllList(x => x.FileId == file.Id);
        }

        public IEnumerable<FileRecordVolte> GetFileRecordVoltes(string fileName)
        {
            var file = _dtFileInfoRepository.FirstOrDefault(x => x.CsvFileName == fileName + ".csv");
            return file == null
                ? new List<FileRecordVolte>()
                : _fileRecordVolteRepository.GetAllList(x => x.FileId == file.Id);
        }

        public IEnumerable<FileRecord4G> GetFileRecord4Gs(string fileName, int rasterNum)
        {
            var file = _dtFileInfoRepository.FirstOrDefault(x => x.CsvFileName == fileName + ".csv");
            return file == null
                ? new List<FileRecord4G>()
                : _fileRecord4GRepository.GetAllList(x => x.FileId == file.Id && x.RasterNum == rasterNum);
        }

        public IEnumerable<FileRecordVolte> GetFileRecordVoltes(string fileName, int rasterNum)
        {
            var file = _dtFileInfoRepository.FirstOrDefault(x => x.CsvFileName == fileName + ".csv");
            return file == null
                ? new List<FileRecordVolte>()
                : _fileRecordVolteRepository.GetAllList(x => x.FileId == file.Id && x.RasterNum == rasterNum);
        }

        public IEnumerable<FileRecord3G> GetFileRecord3Gs(string fileName)
        {
            var file = _dtFileInfoRepository.FirstOrDefault(x => x.CsvFileName == fileName + ".csv");
            return file == null
                ? new List<FileRecord3G>()
                : _fileRecord3GRepository.GetAllList(x => x.FileId == file.Id);
        }

        public IEnumerable<FileRecord3G> GetFileRecord3Gs(string fileName, int rasterNum)
        {
            var file = _dtFileInfoRepository.FirstOrDefault(x => x.CsvFileName == fileName + ".csv");
            return file == null
                ? new List<FileRecord3G>()
                : _fileRecord3GRepository.GetAllList(x => x.FileId == file.Id && x.RasterNum == rasterNum);
        }

        public IEnumerable<FileRecord2G> GetFileRecord2Gs(string fileName)
        {
            var file = _dtFileInfoRepository.FirstOrDefault(x => x.CsvFileName == fileName + ".csv");
            return file == null
                ? new List<FileRecord2G>()
                : _fileRecord2GRepository.GetAllList(x => x.FileId == file.Id);
        }

        public IEnumerable<FileRecord2G> GetFileRecord2Gs(string fileName, int rasterNum)
        {
            var file = _dtFileInfoRepository.FirstOrDefault(x => x.CsvFileName == fileName + ".csv");
            return file == null
                ? new List<FileRecord2G>()
                : _fileRecord2GRepository.GetAllList(x => x.FileId == file.Id && x.RasterNum == rasterNum);
        }
        
        public async Task<int> InsertFileRecord4Gs(IEnumerable<FileRecord4G> stats, string tableName)
        {
            var file = _dtFileInfoRepository.FirstOrDefault(x =>
                x.CsvFileName == tableName + ".csv" || x.CsvFileName == tableName + ".CSV");
            if (file == null) return 0;
            var statList = stats.ToList();
            statList.ForEach(stat => { stat.FileId = file.Id;});
            return await _fileRecord4GRepository.UpdateMany(statList);
        }

        public async Task<int> InsertFileRecord2Gs(IEnumerable<FileRecord2G> stats, string tableName)
        {
            var file = _dtFileInfoRepository.FirstOrDefault(x =>
                x.CsvFileName == tableName + ".csv" || x.CsvFileName == tableName + ".CSV");
            if (file == null) return 0;
            var statList = stats.ToList();
            statList.ForEach(stat => { stat.FileId = file.Id;});
            return await _fileRecord2GRepository.UpdateMany(statList);
        }

        public async Task<int> InsertFileRecord3Gs(IEnumerable<FileRecord3G> stats, string tableName)
        {
            var file = _dtFileInfoRepository.FirstOrDefault(x =>
                x.CsvFileName == tableName + ".csv" || x.CsvFileName == tableName + ".CSV");
            if (file == null) return 0;
            var statList = stats.ToList();
            statList.ForEach(stat => { stat.FileId = file.Id;});
            return await _fileRecord3GRepository.UpdateMany(statList);
        }

        public async Task<int> InsertFileRecordVoltes(IEnumerable<FileRecordVolte> stats, string tableName)
        {
            var file = _dtFileInfoRepository.FirstOrDefault(x =>
                x.CsvFileName == tableName + ".csv" || x.CsvFileName == tableName + ".CSV");
            if (file == null) return 0;
            var statList = stats.ToList();
            statList.ForEach(stat => { stat.FileId = file.Id;});
            return await _fileRecordVolteRepository.UpdateManyWithFirstCheck(statList);
        }
    }
}
