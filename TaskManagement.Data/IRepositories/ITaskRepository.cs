using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Class;

namespace TaskManagement.Data
{
    public interface ITaskRepository
    {
        Task<TTask> GetTaskById(int id);
        Task<IEnumerable<TTask>> GetAssignedTaskById(int id);
       Task<IEnumerable<TTask>> GetTasksByStatus(EnumTaskStatus status, int page, int pageSize);
       Task<IEnumerable<TTask>> GetTasksByAssignedUser(int assignedTo, int page, int pageSize);
       Task<IEnumerable<TTask>> GetTasksByTeam(int teamId, int page, int pageSize);
       Task<IEnumerable<TTask>> GetTasksByDueDate(DateTime dueDate, int page, int pageSize);

        Task<TTask> AddTask(TTask task);
        Task UpdateTask(TTask task);
        Task UpdateTaskStatus(int taskId, EnumTaskStatus status);
        Task DeleteTask(int id);
    }
}
