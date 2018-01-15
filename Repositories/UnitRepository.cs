using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartUnitApi.Entities;
using Microsoft.EntityFrameworkCore;
using SmartUnitApi.Repositories;
using SmartUnitApi.Models;

namespace SmartUnitApi.Repositories
{
    public class UnitRepository : IUnitRepository
    {
        private SmartUnitDbContext context;

        public UnitRepository(SmartUnitDbContext context)
        {
            this.context = context;
        }

        public RepositoryActionResult<Unit> Create(Unit entity)
        {
            try
            {
                context.Unit.Add(entity);
                var result = context.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<Unit>(entity, RepositoryActionStatus.Created);
                }
                else
                    return new RepositoryActionResult<Unit>(null, RepositoryActionStatus.NothingModified);
            }
            catch (Exception ex)
            {

                return new RepositoryActionResult<Unit>(entity, RepositoryActionStatus.Error, ex);
            }
        }

        public RepositoryActionResult<Unit> Delete(int id)
        {
            try
            {
                var unit = context.Unit.Where(c => c.UnitId == id).FirstOrDefault();
                if (unit != null)
                {
                    context.Unit.Remove(unit);
                    context.SaveChanges();
                    return new RepositoryActionResult<Unit>(null, RepositoryActionStatus.Deleted);
                }
                else
                    return new RepositoryActionResult<Unit>(null, RepositoryActionStatus.NotFound);

            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<Unit>(null, RepositoryActionStatus.Error, ex);                
            }
        }

        public Unit Get(int id)
        {
            return context.Unit.FirstOrDefault(c => c.UnitId == id);
        }

        public IList<Unit> GetAllUnitsByMunicipality(int id)
        {
            return context.Unit.Where(c => c.Area.MunicipalityId == id).ToList();
        }

        public IList<Unit> GetAllUnitsByArea(int id)
        {
            return context.Unit.Where(c => c.AreaId == id).ToList();
        }

        public IList<Unit> GetAllUnitsByUser(int id)
        {
            var tempUnit = from u in context.Unit
                           join m in context.UserMunicipality on u.Area.MunicipalityId equals m.MunicipalityId
                           where m.UserId == id && u.Area.MunicipalityId == m.MunicipalityId
                           select u;
            return tempUnit.ToList();
        }
        public IList<Unit> GetAllUnitsByLocation(float longitude, float latitude)
        {
            return context.Unit.Where(c => c.Longitude == longitude && c.Latitude == latitude).ToList();
        }

        public RepositoryActionResult<Unit> Update(Unit entity)
        {
            try
            {
                var existingUnit = context.Unit.Where(c => c.UnitId == entity.UnitId).FirstOrDefault();
                if (existingUnit != null)
                {
                    context.Entry(existingUnit).State = EntityState.Detached;
                    context.Unit.Attach(entity);
                    context.Entry(entity).State = EntityState.Modified;
                    context.SaveChanges();
                    return new RepositoryActionResult<Unit>(entity, RepositoryActionStatus.Updated);
                }
                else
                    return new RepositoryActionResult<Unit>(entity, RepositoryActionStatus.NotFound);
            }
            catch (Exception ex)
            {

                return new RepositoryActionResult<Unit>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public Unit GetUnitBySerialNumber(string serialNumber)
        {
            return context.Unit.Where(u => u.SerialNumber == serialNumber).FirstOrDefault();
        }
    }
}
