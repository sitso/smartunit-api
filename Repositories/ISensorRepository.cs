using SmartUnitApi.Entities;
using System.Collections.Generic;

namespace SmartUnitApi.Repositories
{
    public interface ISensorRepository
    {
        RepositoryActionResult<Sensor> Create(Sensor sensor);
        RepositoryActionResult<Sensor> Delete(int id);
        Sensor Get(int id);
        IList<Sensor> GetByUnitId(int id);
        IList<SensorLog> GetSensorLogBySensorId(int id);
        IList<SensorLog> GetSensorLogBySensorId(int id, int amount);
        IList<SensorLog> GetSensorLogByUnitId(int id);
        IList<SensorType> GetSensorTypes();
        RepositoryActionResult<SensorLog> AddSensorValue(SensorLog sensorlog);
    }
}