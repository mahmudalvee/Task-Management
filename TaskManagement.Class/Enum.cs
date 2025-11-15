using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Class
{
    public enum EnumEmpRole
    {
        Admin = 1,
        Manager = 2,
        Employee= 3
    }

    public enum EnumTaskStatus
    {
        All,
        Todo,
        InProgress,
        Done
    }
}
