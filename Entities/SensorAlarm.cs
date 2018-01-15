using System;
using System.Collections.Generic;

namespace SmartUnitApi.Entities
{
    public partial class SensorAlarm
    {
        public int SensorId { get; set; }
        public int AlarmId { get; set; }
        public bool IsHigh { get; set; }
        public double Limit { get; set; }

        public virtual Alarm Alarm { get; set; }
        public virtual Sensor Sensor { get; set; }
    }
}
