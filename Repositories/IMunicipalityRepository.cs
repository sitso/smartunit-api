using SmartUnitApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartUnitApi.Repositories
{
    public interface IMunicipalityRepository
    {
        RepositoryActionResult<List<UserMunicipality>> CreateMultiple(List<UserMunicipality> userMunicipalities);
        List<Municipality> GetAllByUser(int id);
        IList<int> GetMunicipalitiesIdsByUser(int id);
    }
}
