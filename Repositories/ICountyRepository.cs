using SmartUnitApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SmartUnitApi.Repositories
{
    public interface ICountyRepository
    {
        IList<County> GetAll();
        IList<County> Get(int id);
    }
}
