using System;
using System.Collections.Generic;

namespace SmartUnitApi.Entities
{
    public partial class SensorLog
    {
        public int LogId { get; set; }
        public int SensorId { get; set; }
        public double Value { get; set; }
        public DateTime Time { get; set; }

        public virtual Sensor Sensor { get; set; }
    }
}
