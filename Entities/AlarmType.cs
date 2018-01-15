using System;
using System.Collections.Generic;

namespace SmartUnitApi.Entities
{
    public partial class AlarmType
    {
        public AlarmType()
        {
            Alarm = new HashSet<Alarm>();
        }

        public int TypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Alarm> Alarm { get; set; }
    }
}
