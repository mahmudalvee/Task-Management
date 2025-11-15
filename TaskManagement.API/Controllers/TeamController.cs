using Microsoft.AspNetCore.Mvc;
using TaskManagement.Service;
using TaskManagement.Class;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class TeamController : ControllerBase
{
    private readonly ITeamService _teamService;

    public TeamController(ITeamService teamService)
    {
        _teamService = teamService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTeams()
    {
        var teams = await _teamService.GetTeamsAsync();
        return Ok(teams);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTeam(int id)
    {
        var team = await _teamService.GetTeamAsync(id);
        if (team == null)
        {
            return NotFound();
        }
        return Ok(team);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTeam([FromBody] Team team)
    {
        if (team == null)
            return BadRequest("Team data cant be null");

        var createdTeam = await _teamService.CreateTeamAsync(team);
        return Ok(createdTeam.Name);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTeam(int id, [FromBody] Team team)
    {
        if (id != team.Id)
            return BadRequest();

        await _teamService.UpdateTeamAsync(team);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTeam(int id)
    {
        if (id <= 0)
            return BadRequest();
        await _teamService.DeleteTeamAsync(id);
        return Ok();
    }
}
