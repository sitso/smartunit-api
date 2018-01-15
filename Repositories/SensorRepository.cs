using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartUnitApi.Entities;
namespace SmartUnitApi.Repositories
{
    public class SensorRepository : ISensorRepository
    {
        SmartUnitDbContext context;

        public SensorRepository(SmartUnitDbContext context)
        {
            this.context = context;
        }

        public Sensor Get(int id)
        {
            return context.Sensor.FirstOrDefault(c => c.SensorId == id);
        }

        public IList<Sensor> GetByUnitId(int id)
        {
            return context.Sensor.Where(c => c.UnitId == id).ToList();
        }

        public IList<SensorLog> GetSensorLogBySensorId(int id)
        {
            return context.SensorLog.Where(c => c.Sensor.SensorId == id).OrderByDescending(c => c.Time).ToList();
        } 
        public IList<SensorLog> GetSensorLogBySensorId(int id, int amount)
        {
            return context.SensorLog
                    .Where(c => c.Sensor.SensorId == id)
                    .Take(amount)
                    .OrderByDescending(c => c.Time)
                    .ToList();
        }
        public IList<SensorLog> GetSensorLogByUnitId(int id)
        {
            return context.SensorLog.Where(c => c.Sensor.UnitId == id).OrderByDescending(c => c.Time).ToList();
        }

        public RepositoryActionResult<Sensor> Delete(int id)
        {
            try
            {
                var sensor = context.Sensor.Where(c => c.SensorId == id).FirstOrDefault();
                if (sensor != null)
                {
                    context.Sensor.Remove(sensor);
                    context.SaveChanges();
                    return new RepositoryActionResult<Sensor>(null, RepositoryActionStatus.Deleted);
                }
                else
                    return new RepositoryActionResult<Sensor>(null, RepositoryActionStatus.NotFound);
            }
            catch (Exception ex)
            {

                return new RepositoryActionResult<Sensor>(null, RepositoryActionStatus.Error, ex);
            }

        }

        public RepositoryActionResult<Sensor> Create(Sensor sensor)
        {
            try
            {
                context.Sensor.Add(sensor);
                context.SaveChanges();
                return new RepositoryActionResult<Sensor>(sensor, RepositoryActionStatus.Created);
            }
            catch (Exception ex)
            {

                return new RepositoryActionResult<Sensor>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public RepositoryActionResult<SensorLog> AddSensorValue(SensorLog sensorlog)
        {
            try
            {
                context.SensorLog.Add(sensorlog);
                context.SaveChanges();
                return new RepositoryActionResult<SensorLog>(sensorlog, RepositoryActionStatus.Created);
            }
            catch (Exception ex)
            {

                return new RepositoryActionResult<SensorLog>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public IList<SensorType> GetSensorTypes()
        {
            return context.SensorType.ToList();
        }
    }
}
