using System;
using System.Collections.Generic;

namespace SmartUnitApi.Entities
{
    public partial class Alarm
    {
        public Alarm()
        {
            AlarmLog = new HashSet<AlarmLog>();
            SensorAlarm = new HashSet<SensorAlarm>();
        }

        public int AlarmId { get; set; }
        public int TypeId { get; set; }
        public int UnitId { get; set; }

        public virtual ICollection<AlarmLog> AlarmLog { get; set; }
        public virtual ICollection<SensorAlarm> SensorAlarm { get; set; }
        public virtual AlarmType Type { get; set; }
        public virtual Unit Unit { get; set; }
    }
}
