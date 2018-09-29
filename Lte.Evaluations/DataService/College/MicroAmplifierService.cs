using System.Collections.Generic;
using Abp.EntityFramework.AutoMapper;
using Lte.MySqlFramework.Abstract.Infrastructure;
using Lte.MySqlFramework.Entities.Maintainence;

namespace Lte.Evaluations.DataService.College
{
    public class MicroAmplifierService
    {
        private readonly IMicroItemRepository _repository;
        private readonly IMicroAddressRepository _addressRepository;

        public MicroAmplifierService(IMicroItemRepository repository, IMicroAddressRepository addressRepository)
        {
            _repository = repository;
            _addressRepository = addressRepository;
        }

        public IEnumerable<MicroAmplifierView> QueryMicroAmplifierViews()
        {
            var list =
                _addressRepository.GetAllList(
                        x => x.Longtitute > 112 && x.Longtitute < 114 && x.Lattitute > 22 && x.Lattitute < 24)
                    .MapTo<List<MicroAmplifierView>>();
            list.ForEach(item =>
            {
                item.MicroItems = _repository.GetAllList(x => x.AddressNumber == item.AddressNumber);
            });
            return list;
        }
    }
}