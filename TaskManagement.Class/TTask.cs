using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Class
{
    public class TTask : BaseClass
    {
        public string Description { get; set; }
        public EnumTaskStatus Status { get; set; }
        public int AssignedToUserId { get; set; }
        public int TeamId { get; set; }
        public DateTime? DueDate { get; set; }
        public Team? Team { get; set; }
        public User? AssignedToUser { get; set; }
    }
}
