using System;
using System.Collections.Generic;

namespace SmartUnitApi.Entities
{
    public partial class Permission
    {
        public Permission()
        {
            UserPermissions = new HashSet<UserPermissions>();
        }

        public int PermissionId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<UserPermissions> UserPermissions { get; set; }
    }
}
