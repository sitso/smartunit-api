using System;
using System.Collections.Generic;

namespace SmartUnitApi.Entities
{
    public partial class User
    {
        public User()
        {
            UserMunicipality = new HashSet<UserMunicipality>();
            UserPermissions = new HashSet<UserPermissions>();
        }

        public int UserId { get; set; }
        public string Email { get; set; }
        public string FcmToken { get; set; }

        public virtual ICollection<UserMunicipality> UserMunicipality { get; set; }
        public virtual ICollection<UserPermissions> UserPermissions { get; set; }
    }
}
