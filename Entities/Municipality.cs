using System;
using System.Collections.Generic;

namespace SmartUnitApi.Entities
{
    public partial class Municipality
    {
        public Municipality()
        {
            Area = new HashSet<Area>();
            UserMunicipality = new HashSet<UserMunicipality>();
        }

        public int MunicipalityId { get; set; }
        public string Name { get; set; }
        public int CountyId { get; set; }

        public virtual ICollection<Area> Area { get; set; }
        public virtual ICollection<UserMunicipality> UserMunicipality { get; set; }
        public virtual County County { get; set; }
    }
}
