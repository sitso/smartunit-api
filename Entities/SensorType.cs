using System;
using System.Collections.Generic;

namespace SmartUnitApi.Entities
{
    public partial class SensorType
    {
        public SensorType()
        {
            Sensor = new HashSet<Sensor>();
        }

        public int TypeId { get; set; }
        public string Name { get; set; }
        public string MeasureUnit { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Sensor> Sensor { get; set; }
    }
}
