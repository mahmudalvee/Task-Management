using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Class;
using TaskManagement.Data;
using TaskManagement.Data.Contexts;

namespace TaskManagement.Data.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskManagementDBContext _context;

        public TaskRepository(TaskManagementDBContext context)
        {
            _context = context;
        }

        public async Task<TTask> GetTaskById(int id)
        {
            return await _context.TTasks.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<TTask>> GetAssignedTaskById(int id)
        {
            return await _context.TTasks.Where(t => t.AssignedToUserId == id).ToListAsync();
        }

        public async Task<IEnumerable<TTask>> GetTasksByStatus(EnumTaskStatus status, int page,int pageSize)
        {
            IQueryable<TTask> query = _context.TTasks;

            if (status != EnumTaskStatus.All)
                query = query.Where(t => t.Status == status);

            return await query.Skip((page - 1) * pageSize)
                              .Take(pageSize)
                              .ToListAsync();
        }

        public async Task<IEnumerable<TTask>> GetTasksByAssignedUser( int assignedTo,int page, int pageSize)
        {
            IQueryable<TTask> query = _context.TTasks;

            if (assignedTo > 0)
            {
                query = query.Where(t => t.AssignedToUserId == assignedTo);
            }

            return await query.Skip((page - 1) * pageSize)
                              .Take(pageSize)
                              .ToListAsync();
        }

        public async Task<IEnumerable<TTask>> GetTasksByTeam(int teamId, int page,    int pageSize)
        {
            IQueryable<TTask> query = _context.TTasks.Include(t => t.Team);     //get team
            query = query.Where(t => t.Team.Id == teamId);

            return await query.Skip((page - 1) * pageSize)
                              .Take(pageSize)
                              .ToListAsync();
        }

        public async Task<IEnumerable<TTask>> GetTasksByDueDate( DateTime dueDate, int page, int pageSize)
        {
            IQueryable<TTask> query = _context.TTasks;
            query = query.Where(t => t.DueDate.HasValue && t.DueDate <= dueDate);  //within due dates

            return await query.Skip((page - 1) * pageSize)
                              .Take(pageSize)
                              .ToListAsync();
        }

        public async Task<TTask> AddTask(TTask task)
        {
            _context.TTasks.Add(task);

            await _context.SaveChangesAsync();
            return task;
        }

        public async Task UpdateTask(TTask task)
        {
            _context.TTasks.Update(task);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTaskStatus(int taskId, EnumTaskStatus status)
        {
            var taskToUpdate = await _context.TTasks.FirstOrDefaultAsync(t => t.Id == taskId);
            if (taskToUpdate == null)
                throw new KeyNotFoundException($"Task ID  not exis");

            taskToUpdate.Status = status;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTask(int id)
        {
            var task = await _context.TTasks.FindAsync(id);
            if (task != null)
            {
                _context.TTasks.Remove(task);
                await _context.SaveChangesAsync();
            }
        }
    }
}
