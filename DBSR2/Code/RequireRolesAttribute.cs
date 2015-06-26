using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBSR2
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class RequireRoleAttribute : Attribute
    {
        public string Role
        {
            get;
            set;
        }

        public RequireRoleAttribute(string role)
        {
            this.Role = role;
        }
    }
}