using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartUnitApi.Repositories
{
    public enum RepositoryActionStatus
    {
        Ok,
        Created,
        Updated,
        NotFound,
        NothingModified,
        Deleted,
        Error
    }
}
