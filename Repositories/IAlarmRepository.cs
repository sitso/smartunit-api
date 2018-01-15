using System.Collections.Generic;
using SmartUnitApi.Entities;

namespace SmartUnitApi.Repositories
{
    public interface IAlarmRepository
    {
        Alarm Get(int id);
        IList<AlarmLog> GetAlarmLogByUnitId(int id);
        IList<AlarmLog> GetAlarmLogByAlarmId(int id);
        IList<AlarmLog> GetAlarmLogByAlarmId(int id, int amount);
        IList<Alarm> GetAlarmsByUnitId(int id);
        IList<Alarm> GetAlarmsByUserId(int id);
        IList<AlarmType> GetAllAlarmTypes();
        RepositoryActionResult<AlarmLog> AddAlarmLogValue(AlarmLog alarmvalue);
        RepositoryActionResult<Alarm> Update(Alarm alarm);
    }
}