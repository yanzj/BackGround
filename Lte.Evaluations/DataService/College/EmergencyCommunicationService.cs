using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Complain;
using Abp.EntityFramework.Repositories;
using AutoMapper;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Abstract.Complain;
using Lte.MySqlFramework.Abstract.Region;

namespace Lte.Evaluations.DataService.College
{
    public class EmergencyCommunicationService
    {
        private readonly IEmergencyCommunicationRepository _repository;
        private readonly ITownRepository _townRepository;
        private readonly IEmergencyProcessRepository _processRepository;

        public EmergencyCommunicationService(IEmergencyCommunicationRepository repository, ITownRepository townRepository,
            IEmergencyProcessRepository processRepository)
        {
            _repository = repository;
            _townRepository = townRepository;
            _processRepository = processRepository;
        }

        public int Dump(EmergencyCommunicationDto dto)
        {
            return
                _repository
                    .DumpItem<IEmergencyCommunicationRepository, EmergencyCommunication, EmergencyCommunicationDto>(
                        dto, _townRepository);
        }

        public EmergencyCommunicationDto Query(int id)
        {
            var towns = _townRepository.GetAllList();
            return
                _repository.Query<IEmergencyCommunicationRepository, EmergencyCommunication, EmergencyCommunicationDto>(
                    towns, id);
        }

        public IEnumerable<EmergencyProcessDto> QueryProcess(int id)
        {
            return
                Mapper.Map<List<EmergencyProcess>, IEnumerable<EmergencyProcessDto>>(_processRepository.GetAllList(id));
        }

        public List<EmergencyCommunicationDto> Query(DateTime begin, DateTime end)
        {
            return
                _repository.Query<IEmergencyCommunicationRepository, EmergencyCommunication, EmergencyCommunicationDto>(
                    _townRepository, begin, end);
        }

        public List<EmergencyCommunicationDto> Query(string district, string town, DateTime begin, DateTime end)
        {
            return
                _repository.Query<IEmergencyCommunicationRepository, EmergencyCommunication, EmergencyCommunicationDto>(
                    _townRepository, district, town, begin, end);
        }

        public async Task<EmergencyProcessDto> ConstructProcess(EmergencyCommunicationDto dto, string userName)
        {
            return await 
                _repository
                    .ConstructProcess
                    <IEmergencyCommunicationRepository, IEmergencyProcessRepository, EmergencyCommunication,
                        EmergencyCommunicationDto, EmergencyProcess, EmergencyProcessDto>(_processRepository, dto, userName);
        }

        public async Task<int> UpdateAsync(EmergencyProcessDto dto)
        {
            return
                await
                    _processRepository.UpdateOne<IEmergencyProcessRepository, EmergencyProcess, EmergencyProcessDto>(dto);
        }
    }
}