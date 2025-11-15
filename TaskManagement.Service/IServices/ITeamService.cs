using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Class;

namespace TaskManagement.Service
{
    public interface ITeamService
    {
        Task<Team> GetTeamAsync(int id);
        Task<IEnumerable<Team>> GetTeamsAsync();
        Task<Team> CreateTeamAsync(Team team);
        Task UpdateTeamAsync(Team team);
        Task DeleteTeamAsync(int id);
    }
}
