using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartUnitApi.Entities;

namespace SmartUnitApi.Repositories
{
    public interface IUnitRepository
    {
        Unit Get(int id);
        Unit GetUnitBySerialNumber(string serialNumber);
        RepositoryActionResult<Unit> Create(Unit entity);
        RepositoryActionResult<Unit> Delete(int id);
        RepositoryActionResult<Unit> Update(Unit entity);
        IList<Unit> GetAllUnitsByMunicipality(int id);
        IList<Unit> GetAllUnitsByArea(int id);
        IList<Unit> GetAllUnitsByUser(int id);
        IList<Unit> GetAllUnitsByLocation(float longitude, float latitude);
    }
}
