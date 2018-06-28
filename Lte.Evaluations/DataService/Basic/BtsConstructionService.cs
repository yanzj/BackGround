using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Entities;

namespace Lte.Evaluations.DataService.Basic
{
    public class BtsConstructionService
    {
        private readonly IConstructionInformationRepository _constructionRepository;
        private readonly IENodebBaseRepository _eNodebBaseRepository;

        public BtsConstructionService(IConstructionInformationRepository constructionRepository, 
            IENodebBaseRepository eNodebBaseRepository)
        {
            _constructionRepository = constructionRepository;
            _eNodebBaseRepository = eNodebBaseRepository;
        }

        public IEnumerable<StationCellView> QueryConstructionInformations(string searchTxt, double west,
            double east, double south, double north, string district, string town)
        {
            var btsList =
                _eNodebBaseRepository.GetAllList(
                    x => x.Longtitute > west && x.Longtitute < east && x.Lattitute > south && x.Lattitute < north);
            return QueryConstructionViews(searchTxt, district, town, btsList);
        }

        public IEnumerable<StationCellView> QueryConstructionInformations(string searchTxt, string district,
            string town)
        {
            return QueryConstructionViews(searchTxt, district, town, _eNodebBaseRepository.GetAllList());
        }

        private IEnumerable<StationCellView> QueryConstructionViews(string searchTxt, string district, string town, List<ENodebBase> btsList)
        {
            if (!string.IsNullOrEmpty(searchTxt))
            {
                btsList = btsList.Where(o => o.ENodebName == searchTxt).ToList();
            }
            if (district != "全部")
            {
                btsList = btsList.Where(o => o.StationDistrict == district).ToList();
            }

            if (town != "全部")
            {
                btsList = btsList.Where(o => o.MarketCenter == town).ToList();
            }
            var conList = _constructionRepository.GetAllList();
            var constructionList = from c in conList
                join b in btsList on c.ENodebId equals b.ENodebId
                select new
                {
                    Bts = b,
                    Con = c
                };
            return constructionList.Select(x =>
            {
                var view = x.Con.MapTo<StationCellView>();
                x.Bts.MapTo(view);
                return view;
            });
        }

    }
}