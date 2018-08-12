using System;
using System.Collections.Generic;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Maintainence;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Abstract.Maintainence;

namespace Lte.Evaluations.DataService.Dump
{
    public class LteProblemService
    {
        private readonly ILteProblemRepository _repository;

        public LteProblemService(ILteProblemRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<LteProblem> QueryProblems(string type)
        {
            return _repository.GetAllList(x => x.Type == type);
        }

        public IEnumerable<LteProblem> QueryRandomProblems(string type, int count)
        {
            var list = _repository.GetAllList(x => x.Type == type
                                                   && x.Keyword1 == null && x.Keyword2 == null);
            return GenerateLteProblems(count, list);
        }

        public IEnumerable<LteProblem> QueryRandomProblems(string type, int count, string keyword1)
        {
            var list = _repository.GetAllList(x => x.Type == type
                                                   && x.Keyword1 == keyword1 && x.Keyword2 == null);
            return GenerateLteProblems(count, list);
        }

        public IEnumerable<LteProblem> QueryRandomProblems(string type, int count, string keyword1, string keyword2)
        {
            var list = _repository.GetAllList(x => x.Type == type
                                                   && x.Keyword1 == keyword1 && x.Keyword2 == keyword2);
            return GenerateLteProblems(count, list);
        }

        public bool UpdateKeywords(int id, string keyword1, string keyword2)
        {
            var item = _repository.FirstOrDefault(x => x.Id == id);
            if (item == null) return false;
            item.Keyword1 = keyword1;
            item.Keyword2 = keyword2;
            _repository.SaveChanges();
            return true;
        }

        private static IEnumerable<LteProblem> GenerateLteProblems(int count, List<LteProblem> list)
        {
            if (count >= list.Count) return list;
            var selected = 0;
            var results = new List<LteProblem>();
            while (selected < count)
            {
                var random = new Random();
                var r = random.Next(list.Count);
                var result = list[r];
                if (results.Contains(result)) continue;
                results.Add(result);
                selected++;
            }
            return results;
        }
    }
}
