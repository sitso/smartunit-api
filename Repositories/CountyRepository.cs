using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartUnitApi.Entities;

namespace SmartUnitApi.Repositories
{
    public class CountyRepository : ICountyRepository
    {
        SmartUnitDbContext context;

        public CountyRepository(SmartUnitDbContext context)
        {
            this.context = context;
        }

        public IList<County> Get(int id)
        {
            var countiesQuery = (from c in context.County
                           join m in context.Municipality on c.CountyId equals m.CountyId
                           join mu in context.UserMunicipality on m.MunicipalityId equals mu.MunicipalityId     
                           where mu.UserId == id
                           orderby c.CountyId ascending 
                           select new County {CountyId = c.CountyId, Name = c.Name });
            var counties = countiesQuery.GroupBy(cq => cq.CountyId)
                                .Select(cq => cq.First())
                                .ToList();
            return counties;
        }

        public IList<County> GetAll()
        {
            return context.County.ToList();
        }
    }
}
