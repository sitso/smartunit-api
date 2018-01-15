using SmartUnitApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartUnitApi.Repositories
{
    public interface IUserRepository
    { 
        RepositoryActionResult<User> Delete(int id);
        RepositoryActionResult<User> Create(User user);
        RepositoryActionResult<List<UserMunicipality>> AddLocationsToUser(List<UserMunicipality> userLocations);
        IList<int> GetAllUserIdsByUnitId(int id);
        User Get(int id);
    }
}
