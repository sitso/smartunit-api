using System;
using System.Collections.Generic;

namespace SmartUnitApi.Entities
{
    public partial class County
    {
        public County()
        {
            Municipality = new HashSet<Municipality>();
        }

        public int CountyId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Municipality> Municipality { get; set; }
    }
}
