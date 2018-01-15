using System;
using System.Collections.Generic;

namespace SmartUnitApi.Entities
{
    public partial class Area
    {
        public Area()
        {
            EdgeRouter = new HashSet<EdgeRouter>();
            Unit = new HashSet<Unit>();
        }

        public int AreaId { get; set; }
        public string Name { get; set; }
        public int MunicipalityId { get; set; }

        public virtual ICollection<EdgeRouter> EdgeRouter { get; set; }
        public virtual ICollection<Unit> Unit { get; set; }
        public virtual Municipality Municipality { get; set; }
    }
}
