using SmartUnitApi.Entities;
using System.Collections.Generic;

namespace SmartUnitApi.Repositories
{
    public interface IActuatorRepository
    {
        RepositoryActionResult<Actuator> Create(Actuator actuator);
        RepositoryActionResult<Actuator> Delete(int id);
        RepositoryActionResult<Actuator> Update(Actuator actuator); 
        Actuator Get(int id);
        IList<Actuator> GetByUnitId(int id);
        IList<ActuatorLog> GetActuatorLogById(int id);
        IList<ActuatorLog> GetActuatorLogByUnitId(int id);
    }
}