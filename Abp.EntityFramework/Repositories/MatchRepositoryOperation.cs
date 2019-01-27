using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.EntityFramework.AutoMapper;
using AutoMapper;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;

namespace Abp.EntityFramework.Repositories
{
    public static class MatchRepositoryOperation
    {
        public static int Import<TRepository, TEntity, TExcel>(this TRepository repository, IEnumerable<TExcel> stats)
            where TRepository : IRepository<TEntity>, IMatchRepository<TEntity, TExcel>, ISaveChanges
            where TEntity : Entity
        {
            foreach (var stat in stats)
            {
                var info = repository.Match(stat);
                if (info == null)
                {
                    repository.Insert(stat.MapTo<TEntity>());
                }
                else
                {
                    Mapper.Map(stat, info);
                }
            }
            return repository.SaveChanges();
        }

        public static int Import<TRepository, TEntity, TExcel>(this TRepository repository, IEnumerable<TExcel> stats, 
            string key, Action<TEntity, string> setValueAction)
            where TRepository : IRepository<TEntity>, IMatchKeyRepository<TEntity, TExcel>, ISaveChanges
            where TEntity : Entity
        {
            foreach (var stat in stats)
            {
                var info = repository.Match(stat, key);
                if (info == null)
                {
                    var entity = stat.MapTo<TEntity>();
                    setValueAction(entity, key);
                    repository.Insert(entity);
                }
                else
                {
                    Mapper.Map(stat, info);
                }
            }
            return repository.SaveChanges();
        }

        public static int Import<TRepository, TEntity, TExcel, TTown>(this TRepository repository, IEnumerable<TExcel> stats,
            List<TTown> towns, Func<IEnumerable<TTown>, TExcel, int> townIdFunc)
            where TRepository : IRepository<TEntity>, IMatchRepository<TEntity, TExcel>, ISaveChanges
            where TEntity : Entity, ITownId
            where TTown : Entity, ITown
        {
            foreach (var stat in stats)
            {
                var info = repository.Match(stat);
                var townId = townIdFunc(towns, stat);
                if (info == null)
                {
                    info = stat.MapTo<TEntity>();
                    info.TownId = townId;
                    repository.Insert(info);
                }
                else
                {
                    info.TownId = townId;
                    Mapper.Map(stat, info);
                }
            }
            return repository.SaveChanges();
        }

        public static int Import<TRepository, TEntity, TExcel, TTown>(
            this TRepository repository, IEnumerable<TExcel> stats, List<TTown> towns)
            where TRepository : IRepository<TEntity>, IMatchRepository<TEntity, TExcel>, ISaveChanges
            where TEntity : Entity, ITownId
            where TTown : Entity, ITown
            where TExcel : IDistrictTown
        {
            foreach (var stat in stats)
            {
                var info = repository.Match(stat);
                var town = towns.FirstOrDefault(x => x.DistrictName == stat.District && x.TownName == stat.Town);
                var townId = town?.Id ?? 1;
                if (info == null)
                {
                    info = stat.MapTo<TEntity>();
                    info.TownId = townId;
                    repository.Insert(info);
                }
                else
                {
                    info.TownId = townId;
                    Mapper.Map(stat, info);
                }
            }
            return repository.SaveChanges();
        }

        public static int UpdateOnlyMany<TRepository, TEntity, TExcel, TTown>(
            this TRepository repository, IEnumerable<TExcel> stats, List<TTown> towns)
            where TRepository : IRepository<TEntity>, IMatchRepository<TEntity, TExcel>, ISaveChanges
            where TEntity : Entity, ITownId
            where TTown : Entity, ITown
            where TExcel : IDistrictTown
        {
            var count = 0;
            foreach (var stat in stats)
            {
                var info = repository.Match(stat);
                var town = towns.FirstOrDefault(x => x.DistrictName == stat.District && x.TownName == stat.Town);
                var townId = town?.Id ?? -1;
                if (info == null) continue;
                info.TownId = townId;
                Mapper.Map(stat, info);
                count++;
            }
            repository.SaveChanges();
            return count;
        }

        public static async Task<int> UpdateOneInUse<TRepository, TEntity, TDto, TTown>(this TRepository repository,
            TDto stat, List<TTown> towns, Func<TDto, string> queryTownFunc, Action<TEntity, TDto> updateAction,
            Func<TDto, int> searchTownFunc)
            where TRepository : IRepository<TEntity>, IMatchRepository<TEntity, TDto>, ISaveChanges
            where TEntity : Entity, IIsInUse, ITownId, new()
            where TTown : Entity, ITown
            where TDto : IStationDistrictTown
        {
            var info = repository.Match(stat);
            if (info == null)
            {
                info = stat.MapTo<TEntity>();
                updateAction(info, stat);
                info.IsInUse = true;
                var town = towns.FirstOrDefault(x =>
                    x.DistrictName == stat.StationDistrict && x.TownName == queryTownFunc(stat));
                info.TownId = town?.Id ?? searchTownFunc(stat);
                await repository.InsertAsync(info);
            }

            return repository.SaveChanges();
        }

        public static int ImportOne<TRepository, TEntity, TDto>(this TRepository repository, TDto stat)
            where TRepository : IRepository<TEntity>, IMatchRepository<TEntity, TDto>, ISaveChanges
            where TEntity : Entity
        {
            var info = repository.Match(stat);
            if (info == null)
            {
                repository.Insert(stat.MapTo<TEntity>());
            }
            return repository.SaveChanges();
        }

        public static TEntity ImportOne<TRepository, TEntity>(this TRepository repository, TEntity stat)
            where TRepository : IRepository<TEntity>, IMatchRepository<TEntity>, ISaveChanges
            where TEntity : Entity
        {
            var info = repository.Match(stat);
            if (info == null)
            {
                var result = repository.Insert(stat);
                repository.SaveChanges();
                return result;
            }
            return null;
        }

        public static async Task<int> ImportOneAsync<TRepository, TEntity>(this TRepository repository, TEntity stat)
            where TRepository : IRepository<TEntity>, IMatchRepository<TEntity>, ISaveChanges
            where TEntity : Entity
        {
            var info = repository.Match(stat);
            if (info == null)
            {
                await repository.InsertAsync(stat);
                return repository.SaveChanges();
            }
            return 0;
        }

        public static async Task<int> UpdateOne<TRepository, TEntity, TDto>(this TRepository repository, TDto stat)
            where TRepository : IRepository<TEntity>, IMatchRepository<TEntity, TDto>, ISaveChanges
            where TEntity : Entity, new()
        {
            var info = repository.Match(stat);
            if (info == null)
            {
                await repository.InsertAsync(stat.MapTo<TEntity>());
            }
            else
            {
                stat.MapTo(info);
            }
            return repository.SaveChanges();
        }
        
        public static async Task<int> UpdateOneInUse<TRepository, TEntity, TDto>(this TRepository repository, TDto stat)
            where TRepository : IRepository<TEntity>, IMatchRepository<TEntity, TDto>, ISaveChanges
            where TEntity : Entity, IIsInUse, new()
        {
            var info = repository.Match(stat);
            if (info == null)
            {
                info = stat.MapTo<TEntity>();
                info.IsInUse = true;
                await repository.InsertAsync(info);
            }
            else
            {
                stat.MapTo(info);
                info.IsInUse = true;
            }
            return repository.SaveChanges();
        }
        
        public static async Task<int> UpdateOneInUse<TRepository, TEntity, TDto>(this TRepository repository, TDto stat,
            Action<TEntity, TDto> updateAction)
            where TRepository : IRepository<TEntity>, IMatchRepository<TEntity, TDto>, ISaveChanges
            where TEntity : Entity, IIsInUse, new()
        {
            var info = repository.Match(stat);
            if (info == null)
            {
                info = stat.MapTo<TEntity>();
                info.IsInUse = true;
                updateAction(info, stat);
                await repository.InsertAsync(info);
            }
            return repository.SaveChanges();
        }

        public static async Task<int> UpdateOnly<TRepository, TEntity, TDto>(this TRepository repository, TDto stat,
            Action<TDto, TEntity> updateAction)
            where TRepository : IRepository<TEntity>, IMatchRepository<TEntity, TDto>, ISaveChanges
            where TEntity : Entity, new()
        {
            var info = repository.Match(stat);
            if (info != null)
            {
                updateAction(stat, info);

                await repository.UpdateAsync(info);
            }
            return repository.SaveChanges();
        }

        public static async Task<int> UpdateMany<TRepository, TEntity, TDto>(this TRepository repository, IEnumerable<TDto> stats)
            where TRepository : IRepository<TEntity>, IMatchRepository<TEntity, TDto>, ISaveChanges
            where TEntity : Entity, new()
        {
            var count = 0;
            var total = 0;
            foreach (var stat in stats)
            {
                var info = repository.Match(stat);
                if (info == null)
                {
                    await repository.InsertAsync(stat.MapTo<TEntity>());
                }
                else
                {
                    Mapper.Map(stat, info);
                    await repository.UpdateAsync(info);
                }
                if (count++%1000 == 0)
                    total += repository.SaveChanges();
            }
            
            return total + repository.SaveChanges();
        }

        public static async Task<int> UpdateMany<TRepository, TEntity>(this TRepository repository, IEnumerable<TEntity> stats)
            where TRepository : IRepository<TEntity>, IMatchRepository<TEntity>, ISaveChanges
            where TEntity : Entity, new()
        {
            var count = 0;
            var total = 0;
            foreach (var stat in stats)
            {
                var info = repository.Match(stat);
                if (info == null)
                {
                    await repository.InsertAsync(stat.MapTo<TEntity>());
                }
                else
                {
                    Mapper.Map(stat, info);
                    await repository.UpdateAsync(info);
                }
                if (count++ % 1000 == 0)
                    total += repository.SaveChanges();
            }

            return total + repository.SaveChanges();
        }
        
        public static async Task<int> UpdateManyWithFirstCheck<TRepository, TEntity>(this TRepository repository, IEnumerable<TEntity> stats)
            where TRepository : IRepository<TEntity>, IMatchRepository<TEntity>, ISaveChanges
            where TEntity : Entity, new()
        {
            var count = 0;
            var total = 0;
            var firstCheck = false;
            foreach (var stat in stats)
            {
                if (!firstCheck)
                {
                    var info = repository.Match(stat);
                    if (info == null)
                    {
                        await repository.InsertAsync(stat.MapTo<TEntity>());
                        firstCheck = true;
                    }
                }
                else
                {
                    await repository.InsertAsync(stat.MapTo<TEntity>());
                }
                
                if (count++ % 1000 == 0)
                    total += repository.SaveChanges();
            }

            return total + repository.SaveChanges();
        }

        public static async Task<TProcessDto> ConstructProcess
            <TRepository, TProcessRepository, TEntity, TDto, TProcess, TProcessDto>(this TRepository repository,
                TProcessRepository processRepository, TDto dto, string userName)
            where TRepository : IRepository<TEntity>, IMatchRepository<TEntity, TDto>,  ISaveChanges
            where TProcessRepository : IRepository<TProcess>, IMatchRepository<TProcess, TProcessDto>, ISaveChanges
            where TEntity : Entity, new()
            where TDto : IStateChange, IConstructDto<TProcessDto>
            where TProcess : Entity
            where TProcessDto : class
        {
            if (dto.NextStateDescription == null) return null;
            dto.CurrentStateDescription = dto.NextStateDescription;
            var stat = repository.Match(dto);
            if (stat != null)
            {
                Mapper.Map(dto, stat);
                await repository.UpdateAsync(stat);
                repository.SaveChanges();
            }
            var process = dto.Construct(userName);
            processRepository.ImportOne<TProcessRepository, TProcess, TProcessDto>(process);
            return process;
        }
    }
}