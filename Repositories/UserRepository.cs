using Microsoft.EntityFrameworkCore;
using SmartUnitApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartUnitApi.Repositories
{
    public class UserRepository : IUserRepository
    {

        SmartUnitDbContext context;

        public UserRepository(SmartUnitDbContext context)
        {
            this.context = context;
        }

        public User Get(int id)
        {
            return context.User
                    .Where(usr => usr.UserId == id)
                    .FirstOrDefault(); 
        }
        public RepositoryActionResult<User> Create(User user)
        {
            try
            {
                context.User.Add(user);
                var result = context.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<User>(user, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<User>(user, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<User>(null, RepositoryActionStatus.Error, ex);
            }
        }
        public RepositoryActionResult<List<UserMunicipality>> AddLocationsToUser(List<UserMunicipality> userMunicipality)
        {
            try
            {
                context.UserMunicipality.AddRange(userMunicipality);
                var result = context.SaveChanges();

                if (result > 0)
                {
                    return new RepositoryActionResult<List<UserMunicipality>>(userMunicipality, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<List<UserMunicipality>>(userMunicipality, RepositoryActionStatus.NothingModified);
                }
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<List<UserMunicipality>>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public RepositoryActionResult<User> Delete(int id)
        {
            try
            {
                var user = context.User.Where(usr => usr.UserId == id).FirstOrDefault();
                if (user != null)
                {
                    context.User.Remove(user);
                    context.SaveChanges();
                    return new RepositoryActionResult<User>(null, RepositoryActionStatus.Deleted);
                }
                return new RepositoryActionResult<User>(null, RepositoryActionStatus.NotFound);
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<User>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public IList<int> GetAllUserIdsByUnitId(int id)
        {
            return new List<int> { 1, 2, 3, 4 };    
        }
    }
}
