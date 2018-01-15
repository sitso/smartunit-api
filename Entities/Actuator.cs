using System;
using System.Collections.Generic;

namespace SmartUnitApi.Entities
{
    public partial class Actuator
    {
        public Actuator()
        {
            ActuatorLog = new HashSet<ActuatorLog>();
        }

        public int ActuatorId { get; set; }
        public int UnitId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsAnalog { get; set; }
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
        public string MeasureUnit { get; set; }

        public virtual ICollection<ActuatorLog> ActuatorLog { get; set; }
        public virtual Unit Unit { get; set; }
    }
}
