using System;
using System.Collections.Generic;

namespace SmartUnitApi.Entities
{
    public partial class AlarmLog
    {
        public int LogId { get; set; }
        public int AlarmId { get; set; }
        public DateTime Time { get; set; }
        public int State { get; set; }

        public virtual Alarm Alarm { get; set; }
    }
}
