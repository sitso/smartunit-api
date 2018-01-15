using System;
using System.Collections.Generic;

namespace SmartUnitApi.Entities
{
    public partial class EdgeRouter
    {
        public EdgeRouter()
        {
            Unit = new HashSet<Unit>();
        }

        public int RouterId { get; set; }
        public string SocketId { get; set; }
        public string Name { get; set; }
        public int? AreaId { get; set; }

        public virtual ICollection<Unit> Unit { get; set; }
        public virtual Area Area { get; set; }
    }
}
