using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Entities;

namespace Lte.Evaluations.DataService.Basic
{
    public class ENodebBaseService
    {
        private readonly IENodebBaseRepository _repository;

        public ENodebBaseService(IENodebBaseRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<ENodebBaseView> QueryENodebBases(string searchText)
        {
            return string.IsNullOrEmpty(searchText)
                ? new List<ENodebBaseView>()
                : _repository.GetAllList(
                        x => x.ENodebName.Contains(searchText)
                             || x.ENodebSerial.Contains(searchText)
                             || x.ProjectSerial.Contains(searchText)
                             || x.StationNum.Contains(searchText)
                             || x.StationName.Contains(searchText))
                    .MapTo<IEnumerable<ENodebBaseView>>();
        }

        public IEnumerable<ENodebBaseView> QueryAll()
        {
            return _repository.GetAllList().MapTo<IEnumerable<ENodebBaseView>>();
        }

        public IEnumerable<ENodebBaseView> QueryByStationNum(string stationNum)
        {
            return _repository.GetAllList(x => x.StationNum == stationNum).MapTo<IEnumerable<ENodebBaseView>>();
        }

        public ENodebBaseView QueryByENodebName(string eNodebName)
        {
            return _repository.FirstOrDefault(x => x.ENodebFormalName == eNodebName)?.MapTo<ENodebBaseView>();
        }

        public IEnumerable<ENodebBaseView> QueryRange(double west, double east, double south, double north)
        {
            return
                _repository.GetAllList(
                        x => x.Longtitute >= west && x.Longtitute < east && x.Lattitute >= south && x.Lattitute < north)
                    .MapTo<IEnumerable<ENodebBaseView>>();
        }

        public bool UpdateENodebBasicInfo(int eNodebId, string eNodebFactoryDescription, string duplexingDescription,
            int totalCells, string omcStateDescription, string eNodebTypeDescription)
        {
            var item = _repository.FirstOrDefault(x => x.ENodebId == eNodebId);
            if (item == null) return false;
            item.ENodebFactory = eNodebFactoryDescription.GetEnumType<ENodebFactory>();
            item.Duplexing = duplexingDescription.GetEnumType<Duplexing>();
            item.TotalCells = totalCells;
            item.OmcState = omcStateDescription.GetEnumType<OmcState>();
            item.ENodebType = eNodebTypeDescription.GetEnumType<ENodebType>();
            _repository.SaveChanges();
            return true;
        }

        public bool UpdateENodebPositionInfo(int eNodebId, double longtitute, double lattitute, string eNodebShared,
            string omcIp)
        {
            var item = _repository.FirstOrDefault(x => x.ENodebId == eNodebId);
            if (item == null) return false;
            item.Longtitute = longtitute;
            item.Lattitute = lattitute;
            item.IsENodebShared = eNodebShared == "是";
            item.OmcIp = omcIp;
            _repository.SaveChanges();
            return true;
        }
    }
}
