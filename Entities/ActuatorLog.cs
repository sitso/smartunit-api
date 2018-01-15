using System;
using System.Collections.Generic;

namespace SmartUnitApi.Entities
{
    public partial class ActuatorLog
    {
        public int LogId { get; set; }
        public int ActuatorId { get; set; }
        public DateTime Time { get; set; }
        public double Value { get; set; }

        public virtual Actuator Actuator { get; set; }
    }
}
