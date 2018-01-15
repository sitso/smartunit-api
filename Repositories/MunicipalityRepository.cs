using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartUnitApi.Entities;
using System.Collections;

namespace SmartUnitApi.Repositories
{
    public class MunicipalityRepository : IMunicipalityRepository
    {
        SmartUnitDbContext context;

        public MunicipalityRepository(SmartUnitDbContext context)
        {
            this.context = context;
        }

        public RepositoryActionResult<List<UserMunicipality>> CreateMultiple(List<UserMunicipality> userMunicipalities)
        {
            try
            {
                context.UserMunicipality.AddRange(userMunicipalities);
                var result = context.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<List<UserMunicipality>>(userMunicipalities, RepositoryActionStatus.Created);
                }
                else
                    return new RepositoryActionResult<List<UserMunicipality>>(userMunicipalities, RepositoryActionStatus.NothingModified);
            }
            catch (Exception ex)
            {

                return new RepositoryActionResult<List<UserMunicipality>>(userMunicipalities, RepositoryActionStatus.Error, ex);
            }
        }

        public List<Municipality> GetAllByUser(int id)
        {
            var locationIds = GetMunicipalitiesIdsByUser(id);
            var locations = new List<Municipality>();

            foreach (var locationId in locationIds)
            {
                locations.AddRange(context.Municipality
                                            .Where(m => m.MunicipalityId == locationId)
                                            .Select(mu => new Municipality
                                            {
                                                MunicipalityId = mu.MunicipalityId,
                                                Name = mu.Name,
                                                Area = mu.Area,
                                                CountyId = mu.CountyId
                                            })
                                            .ToList());
            }
            return locations;
        }

        public IList<int> GetMunicipalitiesIdsByUser(int id)
        {
            return context.UserMunicipality
                                            .Where(um => um.UserId == id)
                                            .Select(mid => mid.MunicipalityId)
                                            .ToList();
        }
    }
}
