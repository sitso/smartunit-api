using System;
using System.Collections.Generic;

namespace SmartUnitApi.Entities
{
    public partial class UserMunicipality
    {
        public int UserId { get; set; }
        public int MunicipalityId { get; set; }

        public virtual Municipality Municipality { get; set; }
        public virtual User User { get; set; }
    }
}
