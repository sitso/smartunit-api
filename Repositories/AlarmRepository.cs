using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartUnitApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace SmartUnitApi.Repositories
{
    public class AlarmRepository : IAlarmRepository
    {
        SmartUnitDbContext context;
        public AlarmRepository(SmartUnitDbContext context)
        {
            this.context = context;
        }

        public Alarm Get(int id)
        {
            return context.Alarm.FirstOrDefault(c => c.AlarmId == id);
        }

        public IList<Alarm> GetAlarmsByUnitId(int id)
        {
            return context.Alarm.Where(c => c.UnitId == id).ToList();
        }

        public IList<AlarmLog> GetAlarmLogByUnitId(int id)
        {
            return context.AlarmLog.Where(c => c.Alarm.UnitId == id).OrderByDescending(c => c.Time).ToList();
        }

        public IList<AlarmLog> GetAlarmLogByAlarmId(int id)
        {
            return context.AlarmLog.Where(c => c.Alarm.AlarmId == id).OrderByDescending(c => c.Time).ToList();
        }

        public IList<AlarmLog> GetAlarmLogByAlarmId(int id, int amount)
        {
            return context.AlarmLog
                    .Where(c => c.AlarmId == id)
                    .Take(amount)
                    .OrderByDescending(c => c.Time)
                    .ToList();
        }

        public IList<Alarm> GetAlarmsByUserId(int id)
        {
            var alarms = from a in context.Alarm
                        join u in context.Unit on a.UnitId equals u.UnitId
                        join m in context.UserMunicipality on u.Area.MunicipalityId equals m.MunicipalityId
                        where m.UserId == id && u.Area.MunicipalityId == m.MunicipalityId
                        select a;
            return alarms.ToList();
        }

        public IList<AlarmType> GetAllAlarmTypes()
        {
            return context.AlarmType.ToList();
        }

        public RepositoryActionResult<AlarmLog> AddAlarmLogValue(AlarmLog alarmvalue)
        {
            try
            {
                context.AlarmLog.Add(alarmvalue);
                context.SaveChanges();
                return new RepositoryActionResult<AlarmLog>(alarmvalue, RepositoryActionStatus.Created);
            }
            catch (Exception)
            {

                return new RepositoryActionResult<AlarmLog>(null, RepositoryActionStatus.Error);
            }
        }

        public RepositoryActionResult<Alarm> Update(Alarm alarm)
        {
            var existingAlarm = context.Alarm.FirstOrDefault(ea => ea.AlarmId == alarm.AlarmId);

            if(existingAlarm == null)
            {
                return new RepositoryActionResult<Alarm>(alarm, RepositoryActionStatus.NotFound);
            }

            context.Entry(existingAlarm).State = EntityState.Detached;
            context.Alarm.Attach(alarm);
            context.Entry(alarm).State = EntityState.Modified;

            var result = context.SaveChanges();

            if(result > 0)
            {
                return new RepositoryActionResult<Alarm>(alarm, RepositoryActionStatus.Updated);
            }
            else
            {
                return new RepositoryActionResult<Alarm>(alarm, RepositoryActionStatus.NothingModified);
            }
        }
    }
}
