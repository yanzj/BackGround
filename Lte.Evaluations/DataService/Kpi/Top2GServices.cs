﻿using AutoMapper;
using Lte.MySqlFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework.Entities.Cdma;
using Lte.Domain.Common.Wireless.Kpi;
using Lte.Evaluations.MapperSerive;
using Lte.MySqlFramework.Abstract.Cdma;
using Lte.MySqlFramework.Abstract.Infrastructure;
using Lte.MySqlFramework.Entities.Cdma;
using Lte.MySqlFramework.Support.Container;

namespace Lte.Evaluations.DataService.Kpi
{
    public class TopDrop2GService
    {
        private readonly ITopDrop2GCellRepository _repository;
        private readonly IBtsRepository _btsRepository;
        private readonly IENodebRepository _eNodebRepository;

        public TopDrop2GService(ITopDrop2GCellRepository repository, IBtsRepository btsRepository,
            IENodebRepository eNodebRepository)
        {
            _repository = repository;
            _btsRepository = btsRepository;
            _eNodebRepository = eNodebRepository;
        }

        public TopDrop2GDateView GetDateView(DateTime statDate, string city)
        {
            var begin = statDate.AddDays(-100);
            var end = statDate.AddDays(1);
            var query = _repository.GetAllList(city, begin, end);
            begin = query.Select(x => x.StatTime).Max().Date;
            end = end.AddDays(1);
            var statContainers = GetStatContainers(city, begin, end);
            var viewContainers =
                Mapper.Map<List<TopCellContainer<TopDrop2GCell>>, IEnumerable<TopDrop2GCellViewContainer>>(statContainers);
            var views = viewContainers.Select(x =>
            {
                var view = x.TopDrop2GCellView;
                view.LteName = x.LteName;
                view.CdmaName = x.CdmaName;
                return view;
            });
            return new TopDrop2GDateView
            {
                StatDate = begin,
                StatViews = views
            };
        }

        private List<TopCellContainer<TopDrop2GCell>> GetStatContainers(string city, DateTime begin, DateTime end)
        {
            return _repository.GetAllList(city, begin, end)
                .QueryContainers(_btsRepository, _eNodebRepository)
                .ToList();
        }

        public IEnumerable<TopDrop2GTrendView> GetTrendViews(DateTime begin, DateTime end, string city)
        {
            var statContainers = GetTrendContainers(city, begin, end);
            var viewContainers = 
                Mapper.Map<List<TopCellContainer<TopDrop2GTrend>>, IEnumerable<TopDrop2GTrendViewContainer>>(statContainers);
            return viewContainers.Select(x =>
            {
                var view = x.TopDrop2GTrendView;
                view.CellName = x.CellName;
                view.ENodebName = x.ENodebName;
                return view;
            });
        } 

        private List<TopCellContainer<TopDrop2GTrend>> GetTrendContainers(string city, DateTime begin, DateTime end)
        {
            return
                _repository.GetAllList(city, begin, end)
                    .QueryTrends()
                    .ToList()
                    .QueryContainers(_btsRepository, _eNodebRepository)
                    .ToList();
        } 
    }

    public class TopConnection3GService
    {
        private readonly ITopConnection3GRepository _repository;
        private readonly IBtsRepository _btsRepository;
        private readonly IENodebRepository _eNodebRepository;

        public TopConnection3GService(ITopConnection3GRepository repository, IBtsRepository btsRepository,
            IENodebRepository eNodebRepository)
        {
            _repository = repository;
            _btsRepository = btsRepository;
            _eNodebRepository = eNodebRepository;
        }

        public TopConnection3GDateView GetDateView(DateTime statDate, string city)
        {
            var begin = statDate.AddDays(-100);
            var end = statDate.AddDays(1);
            var query = _repository.GetAllList(city, begin, end);
            begin = query.Select(x => x.StatTime).Max().Date;
            end = end.AddDays(1);
            var statContainers = GetStatContainers(city, begin, end);
            var viewContainers =
                Mapper.Map<List<TopCellContainer<TopConnection3GCell>>, IEnumerable<TopConnection3GCellViewContainer>>(statContainers);
            var views = viewContainers.Select(x =>
            {
                var view = x.TopConnection3GCellView;
                view.LteName = x.LteName;
                view.CdmaName = x.CdmaName;
                return view;
            });
            return new TopConnection3GDateView
            {
                StatDate = begin,
                StatViews = views
            };
        }

        private List<TopCellContainer<TopConnection3GCell>> GetStatContainers(string city, DateTime begin, DateTime end)
        {
            return _repository.GetAllList(city, begin, end)
                .QueryContainers(_btsRepository, _eNodebRepository)
                .ToList();
        }

        public IEnumerable<TopConnection3GTrendView> GetTrendViews(DateTime begin, DateTime end, string city)
        {
            var statContainers = GetTrendContainers(city, begin, end);
            var viewContainers =
                Mapper.Map<List<TopCellContainer<TopConnection3GTrend>>, IEnumerable<TopConnection3GTrendViewContainer>>(statContainers);
            return viewContainers.Select(x =>
            {
                var view = x.TopConnection3GTrendView;
                view.CellName = x.CellName;
                view.ENodebName = x.ENodebName;
                return view;
            });
        }

        private List<TopCellContainer<TopConnection3GTrend>> GetTrendContainers(string city, DateTime begin, DateTime end)
        {
            return
                _repository.GetAllList(city, begin, end)
                    .QueryTrends()
                    .ToList()
                    .QueryContainers(_btsRepository, _eNodebRepository)
                    .ToList();
        }
    }
}
