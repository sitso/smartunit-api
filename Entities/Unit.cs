using System;
using System.Collections.Generic;

namespace SmartUnitApi.Entities
{
    public partial class Unit
    {
        public Unit()
        {
            Actuator = new HashSet<Actuator>();
            Alarm = new HashSet<Alarm>();
            Sensor = new HashSet<Sensor>();
        }

        public int UnitId { get; set; }
        public int AreaId { get; set; }
        public string SerialNumber { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public string Name { get; set; }
        public int BatteryCharge { get; set; }
        public int RouterId { get; set; }
        public int Duration { get; set; }
        public int Interval { get; set; }

        public virtual ICollection<Actuator> Actuator { get; set; }
        public virtual ICollection<Alarm> Alarm { get; set; }
        public virtual ICollection<Sensor> Sensor { get; set; }
        public virtual Area Area { get; set; }
        public virtual EdgeRouter Router { get; set; }
    }
}
