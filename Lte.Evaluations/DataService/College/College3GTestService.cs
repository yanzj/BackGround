using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities.Test;
using AutoMapper;
using Lte.MySqlFramework.Abstract.College;
using Lte.MySqlFramework.Abstract.Test;

namespace Lte.Evaluations.DataService.College
{
    public class College3GTestService
    {
        private readonly ICollege3GTestRepository _repository;
        private readonly ICollegeRepository _collegeRepository;

        public College3GTestService(ICollege3GTestRepository repository, ICollegeRepository collegeRepository)
        {
            _repository = repository;
            _collegeRepository = collegeRepository;
        }

        public IEnumerable<College3GTestView> GetViews(DateTime begin, DateTime end, string name)
        {
            var college = _collegeRepository.GetByName(name);
            if (college == null) return new List<College3GTestView>();
            var results =
                _repository.GetAllList(x => x.TestTime >= begin && x.TestTime < end && x.CollegeId == college.Id)
                    .MapTo<List<College3GTestView>>();
            results.ForEach(x => x.CollegeName = name);
            return results;
        }

        public Dictionary<string, double> GetAverageRates(DateTime begin, DateTime end)
        {
            var results = _repository.GetAllList(begin, end);
            var query = from r in results
                join c in _collegeRepository.GetAllList() on r.CollegeId equals c.Id
                select new { c.Name, Rate = r.DownloadRate };
            return query.GroupBy(x => x.Name).ToDictionary(s => s.Key, t => t.Average(x => x.Rate));
        }

        public Dictionary<string, double> GetAverageUsers(DateTime begin, DateTime end)
        {
            var results = _repository.GetAllList(begin, end);
            var query = from r in results
                join c in _collegeRepository.GetAllList() on r.CollegeId equals c.Id
                select new { c.Name, Users = (double)r.AccessUsers };
            return query.GroupBy(x => x.Name).ToDictionary(s => s.Key, t => t.Average(x => x.Users));
        }

        public Dictionary<string, double> GetAverageMinRssi(DateTime begin, DateTime end)
        {
            var results = _repository.GetAllList(begin, end);
            var query = from r in results
                join c in _collegeRepository.GetAllList() on r.CollegeId equals c.Id
                select new { c.Name, Rssi = r.MinRssi };
            return query.GroupBy(x => x.Name).ToDictionary(s => s.Key, t => t.Average(x => x.Rssi));
        }

        public Dictionary<string, double> GetAverageMaxRssi(DateTime begin, DateTime end)
        {
            var results = _repository.GetAllList(begin, end);
            var query = from r in results
                join c in _collegeRepository.GetAllList() on r.CollegeId equals c.Id
                select new { c.Name, Rssi = r.MaxRssi };
            return query.GroupBy(x => x.Name).ToDictionary(s => s.Key, t => t.Average(x => x.Rssi));
        }

        public Dictionary<string, double> GetAverageVswr(DateTime begin, DateTime end)
        {
            var results = _repository.GetAllList(begin, end);
            var query = from r in results
                join c in _collegeRepository.GetAllList() on r.CollegeId equals c.Id
                select new { c.Name, r.Vswr };
            return query.GroupBy(x => x.Name).ToDictionary(s => s.Key, t => t.Average(x => x.Vswr));
        }

        public async Task<int> Post(College3GTestView view)
        {
            var college = _collegeRepository.GetByName(view.CollegeName);
            if (college == null) return 0;
            view.TestTime = DateTime.Today.AddHours(DateTime.Now.Hour);
            var result =
                _repository.FirstOrDefault(
                    x => x.TestTime == view.TestTime && x.CollegeId == college.Id && x.Place == view.Place);
            if (result != null)
                Mapper.Map(view, result);
            else
                result = view.MapTo<College3GTestResults>();
            result.CollegeId = college.Id;
            await _repository.InsertOrUpdateAsync(result);
            return _repository.SaveChanges();
        }
    }
}