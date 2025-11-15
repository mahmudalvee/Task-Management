using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Class
{
    public class User : BaseClass
    {
        public string Email { get; set; }
        public EnumEmpRole Role { get; set; }
        public string Password { get; set; }        //need Encrypt/Hash

    }
}
