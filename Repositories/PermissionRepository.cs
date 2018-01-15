using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartUnitApi.Entities;

namespace SmartUnitApi.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        SmartUnitDbContext context;

        public PermissionRepository(SmartUnitDbContext context)
        {
            this.context = context; 
        }
        public RepositoryActionResult<List<UserPermissions>> CreateMultiple(List<UserPermissions> userPermissions)
        {
            try
            {
                context.UserPermissions.AddRange(userPermissions);
                var result = context.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<List<UserPermissions>>(userPermissions, RepositoryActionStatus.Created);
                }
                else
                    return new RepositoryActionResult<List<UserPermissions>>(userPermissions, RepositoryActionStatus.NothingModified);
            }
            catch (Exception ex)
            {

                return new RepositoryActionResult<List<UserPermissions>>(userPermissions, RepositoryActionStatus.Error, ex);
            }
        }
        public RepositoryActionResult<UserPermissions> Create(UserPermissions userPermission)
        {
            try
            {
                context.UserPermissions.Add(userPermission);
                var result = context.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<UserPermissions>(userPermission, RepositoryActionStatus.Created);
                }
                else
                    return new RepositoryActionResult<UserPermissions>(userPermission, RepositoryActionStatus.NothingModified);
            }
            catch (Exception ex)
            {

                return new RepositoryActionResult<UserPermissions>(userPermission, RepositoryActionStatus.Error, ex);
            }
        }

        public IList<Permission> GetAllPermissions()
        {
            return context.Permission.ToList();
        }

        public IList<string> GetUserPermissions(int id)
        {
            return (from up in context.UserPermissions
                               join p in context.Permission on up.PermissionId equals p.PermissionId
                               where up.UserId == id
                               select p.Name).ToList(); 
        }

        public IList<int> GetPermissionIdsByUser(int id)
        {
            return (from up in context.UserPermissions
                    join p in context.Permission on up.PermissionId equals p.PermissionId
                    where up.UserId == id
                    select p.PermissionId).ToList();
        }
    }
}
