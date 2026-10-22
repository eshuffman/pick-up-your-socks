
using Microsoft.AspNetCore.Mvc;
using server.DTOs;
using server.Models;
using server.Services;

namespace server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GoalsController : ControllerBase
{
    private readonly IGoalService _goalService;

    public GoalsController(
        IGoalService goalService)
    {
        _goalService = goalService;
    }
    
    // GET: api/goals
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Goal>>> GetAllGoals()
    {
        var goals = await _goalService.GetAllGoalsAsync();
        return Ok(goals);
    }

    // GET: api/goals/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Goal>> GetGoal(int id)
    {
        var goal = await _goalService.GetGoalByIdAsync(id);

        if (goal is null)
        {
            return NotFound();
        }

        return Ok(goal);
    }

    // POST: api/goals
    [HttpPost]
    public async Task<ActionResult<Goal>> CreateGoal(
        CreateGoalRequest request)
    {
        var goal = await _goalService.CreateGoalAsync(request);

        return CreatedAtAction(
            nameof(GetGoal),
            new { id = goal.Id },
            goal);
    }

    // PUT: api/goals/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateGoal(
        int id,
        UpdateGoalRequest request)
    {
        var goal = await _goalService.UpdateGoalAsync(id, request);

        if (goal is null)
        {
            return NotFound();
        }

        return NoContent();
    }

    // DELETE: api/goals/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteGoal(int id)
    {
        var deleted = await _goalService.DeleteGoalAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }


}
