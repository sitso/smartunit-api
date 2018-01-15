using SmartUnitApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartUnitApi.Repositories
{
    public interface IPermissionRepository
    {
        RepositoryActionResult<UserPermissions> Create(UserPermissions userPermission);
        RepositoryActionResult<List<UserPermissions>> CreateMultiple(List<UserPermissions> userPermissions);
        IList<string> GetUserPermissions(int id);
        IList<Permission> GetAllPermissions();
        IList<int> GetPermissionIdsByUser(int id);
    }
}
