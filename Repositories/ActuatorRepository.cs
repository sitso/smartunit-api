using Microsoft.EntityFrameworkCore;
using SmartUnitApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartUnitApi.Repositories
{
    public class ActuatorRepository : IActuatorRepository
    {
        SmartUnitDbContext context;

        public ActuatorRepository(SmartUnitDbContext context)
        {
            this.context = context;
        }

        public Actuator Get(int id)
        {
            return context.Actuator.FirstOrDefault(c => c.ActuatorId == id);
        }

        public IList<Actuator> GetByUnitId(int id)
        {
            //var newAct = from a in context.Actuator
            //             join l in context.ActuatorLog on a.ActuatorId equals l.ActuatorId
            //             where a.ActuatorId == id
            //             select new { ActuatorId = a.ActuatorId, UnitId = a.UnitId, Name = a.Name, Description = a.Description, Value = l.Value };
            //return newAct.ToList();
            return context.Actuator.Where(c => c.UnitId == id).ToList();
        }

        public IList<ActuatorLog> GetActuatorLogById(int id)
        {
            return context.ActuatorLog.Where(c => c.ActuatorId == id).ToList();
        }

        public IList<ActuatorLog> GetActuatorLogByUnitId(int id)
        {
            return context.ActuatorLog.Where(c => c.Actuator.UnitId == id).ToList();
        }
            
        public RepositoryActionResult<Actuator> Create(Actuator actuator)
        {
            try
            {
                context.Actuator.Add(actuator);
                context.SaveChanges();
                return new RepositoryActionResult<Actuator>(actuator, RepositoryActionStatus.Created);
            }
            catch (Exception ex)
            {

                return new RepositoryActionResult<Actuator>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public RepositoryActionResult<Actuator> Delete(int id)
        {
            try
            {
                var act = context.Actuator.Where(c => c.ActuatorId == id).FirstOrDefault();

                if (act != null)
                {
                    context.Actuator.Remove(act);
                    context.SaveChanges();
                    return new RepositoryActionResult<Actuator>(null, RepositoryActionStatus.Deleted);
                }
                return new RepositoryActionResult<Actuator>(null, RepositoryActionStatus.NotFound);
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<Actuator>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public RepositoryActionResult<Actuator> Update(Actuator actuator)
        {
            try
            {
                var existingAct = context.Actuator.Where(c => c.ActuatorId == actuator.ActuatorId).FirstOrDefault();
             
                if (existingAct != null)
                {
                    context.Entry(existingAct).State = EntityState.Detached;
                    context.Actuator.Attach(actuator);
                    context.Entry(actuator).State = EntityState.Modified;
                    context.SaveChanges();
                    return new RepositoryActionResult<Actuator>(actuator, RepositoryActionStatus.Updated);
                }
                return new RepositoryActionResult<Actuator>(null, RepositoryActionStatus.NothingModified);
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<Actuator>(null, RepositoryActionStatus.Error, ex);
            }
        } 
    }
}
