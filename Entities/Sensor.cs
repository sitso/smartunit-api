using System;
using System.Collections.Generic;

namespace SmartUnitApi.Entities
{
    public partial class Sensor
    {
        public Sensor()
        {
            SensorAlarm = new HashSet<SensorAlarm>();
            SensorLog = new HashSet<SensorLog>();
        }

        public int SensorId { get; set; }
        public int TypeId { get; set; }
        public int UnitId { get; set; }

        public virtual ICollection<SensorAlarm> SensorAlarm { get; set; }
        public virtual ICollection<SensorLog> SensorLog { get; set; }
        public virtual SensorType Type { get; set; }
        public virtual Unit Unit { get; set; }
    }
}
