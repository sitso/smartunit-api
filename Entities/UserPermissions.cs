﻿using System;
using System.Collections.Generic;

namespace SmartUnitApi.Entities
{
    public partial class UserPermissions
    {
        public int UserId { get; set; }
        public int PermissionId { get; set; }

        public virtual Permission Permission { get; set; }
        public virtual User User { get; set; }
    }
}
