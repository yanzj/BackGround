using System;
using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework.Entities.Test;
using AutoMapper;
using Lte.MySqlFramework.Abstract.Test;
using Lte.MySqlFramework.Entities.Dt;
using Lte.Parameters.Abstract.Dt;
using Lte.Parameters.Entities.Dt;

namespace Lte.Evaluations.DataService.Dt
{
    public class CsvFileInfoService
    {
        private readonly IFileRecordService _service;
        private readonly IDtFileInfoRepository _dtFileInfoRepository;

        public CsvFileInfoService(IFileRecordService service, IDtFileInfoRepository dtFileInfoRepository)
        {
            _service = service;
            _dtFileInfoRepository = dtFileInfoRepository;
        }
        
        public IEnumerable<CsvFilesInfo> QueryFilesInfos(DateTime begin, DateTime end)
        {
            return _dtFileInfoRepository.GetAllList(x => x.TestDate >= begin && x.TestDate < end);
        }

        public CsvFilesInfo QueryCsvFilesInfo(string fileName)
        {
            return _dtFileInfoRepository.FirstOrDefault(x => x.CsvFileName == fileName + ".csv");
        }

        public int UpdateFileDistance(CsvFilesInfo filesInfo)
        {
            var info = _dtFileInfoRepository.FirstOrDefault(x => x.CsvFileName == filesInfo.CsvFileName);
            if (info != null)
            {
                info.Distance = filesInfo.Distance;
                info.Count = filesInfo.Count;
                info.CoverageCount = filesInfo.CoverageCount;
                _dtFileInfoRepository.Update(info);
            }
            return _dtFileInfoRepository.SaveChanges();
        }

        public IEnumerable<FileRecord4G> GetFileRecord4Gs(string fileName)
        {
            return _service.GetFileRecord4Gs(fileName);
        }

        public IEnumerable<FileRecordVolte> GetFileRecordVoltes(string fileName)
        {
            return _service.GetFileRecordVoltes(fileName);
        }

        public IEnumerable<FileRecord4G> GetFileRecord4Gs(string fileName, int rasterNum)
        {
            return _service.GetFileRecord4Gs(fileName, rasterNum);
        }

        public IEnumerable<FileRecordVolte> GetFileRecordVoltes(string fileName, int rasterNum)
        {
            return _service.GetFileRecordVoltes(fileName, rasterNum);
        }

        public IEnumerable<FileRecord3G> GetFileRecord3Gs(string fileName)
        {
            return _service.GetFileRecord3Gs(fileName);
        }

        public IEnumerable<FileRecord3G> GetFileRecord3Gs(string fileName, int rasterNum)
        {
            return _service.GetFileRecord3Gs(fileName, rasterNum);
        }

        public IEnumerable<FileRecord2G> GetFileRecord2Gs(string fileName)
        {
            return _service.GetFileRecord2Gs(fileName);
        }

        public IEnumerable<FileRecord2G> GetFileRecord2Gs(string fileName, int rasterNum)
        {
            return _service.GetFileRecord2Gs(fileName, rasterNum);
        }

        public IEnumerable<FileRecordCoverage2G> GetCoverage2Gs(FileRasterInfoView infoView)
        {
            var query =
                infoView.RasterNums.Select(
                    x =>
                        Mapper.Map<IEnumerable<FileRecord2G>, IEnumerable<FileRecordCoverage2G>>(
                            GetFileRecord2Gs(infoView.CsvFileName, x)));
            return query.Aggregate((x, y) => x.Concat(y));
        }

        public IEnumerable<FileRecord2G> GetFileRecord2Gs(FileRasterInfoView infoView)
        {
            var query =
                infoView.RasterNums.Select(x => GetFileRecord2Gs(infoView.CsvFileName, x));
            return query.Aggregate((x, y) => x.Concat(y));
        }

        public IEnumerable<FileRecordCoverage3G> GetCoverage3Gs(FileRasterInfoView infoView)
        {
            var query =
                infoView.RasterNums.Select(
                    x =>
                        Mapper.Map<IEnumerable<FileRecord3G>, IEnumerable<FileRecordCoverage3G>>(
                            GetFileRecord3Gs(infoView.CsvFileName, x)));
            return query.Aggregate((x, y) => x.Concat(y));
        }

        public IEnumerable<FileRecord3G> GetFileRecord3Gs(FileRasterInfoView infoView)
        {
            var query =
                infoView.RasterNums.Select(x => GetFileRecord3Gs(infoView.CsvFileName, x));
            return query.Aggregate((x, y) => x.Concat(y));
        }

        public IEnumerable<FileRecordCoverage4G> GetCoverage4Gs(FileRasterInfoView infoView)
        {
            var query =
                infoView.RasterNums.Select(
                    x =>
                        Mapper.Map<IEnumerable<FileRecord4G>, IEnumerable<FileRecordCoverage4G>>(
                            GetFileRecord4Gs(infoView.CsvFileName, x)));
            return query.Aggregate((x, y) => x.Concat(y));
        }

        public IEnumerable<FileRecordCoverage4G> GetCoverageVoltes(FileRasterInfoView infoView)
        {
            var query =
                infoView.RasterNums.Select(
                    x =>
                        Mapper.Map<IEnumerable<FileRecordVolte>, IEnumerable<FileRecordCoverage4G>>(
                            GetFileRecordVoltes(infoView.CsvFileName, x)));
            return query.Aggregate((x, y) => x.Concat(y));
        }

        public IEnumerable<FileRecord4G> GetFileRecord4Gs(FileRasterInfoView infoView)
        {
            var query =
                infoView.RasterNums.Select(x => GetFileRecord4Gs(infoView.CsvFileName, x));
            return query.Aggregate((x, y) => x.Concat(y));
        }

    }
}