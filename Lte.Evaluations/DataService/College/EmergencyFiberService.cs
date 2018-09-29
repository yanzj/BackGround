using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.EntityFramework.Entities.Complain;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Abstract.Complain;

namespace Lte.Evaluations.DataService.College
{
    public class EmergencyFiberService
    {
        private readonly IEmergencyFiberWorkItemRepository _repository;

        public EmergencyFiberService(IEmergencyFiberWorkItemRepository repository)
        {
            _repository = repository;
        }

        public EmergencyFiberWorkItem Create(EmergencyFiberWorkItem item)
        {
            item.BeginDate = DateTime.Now;
            return _repository.ImportOne(item);
        }

        public async Task<int> Finish(EmergencyFiberWorkItem item)
        {
            var info =
                _repository.FirstOrDefault(
                    x => x.EmergencyId == item.EmergencyId && x.WorkItemNumber == item.WorkItemNumber);
            info.FinishDate = DateTime.Now;
            await _repository.UpdateAsync(info);
            return _repository.SaveChanges();
        }

        public List<EmergencyFiberWorkItem> Query(int id)
        {
            return _repository.GetAllList(id);
        }
    }
}