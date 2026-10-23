
using server.Repositories;
using server.Models;
using server.DTOs;

namespace server.Services;

public class GoalService : IGoalService
{
    private readonly IGoalRepository _goalRepository;

    public GoalService(IGoalRepository goalRepository)
    {
        _goalRepository = goalRepository;
    }

    public async Task<List<Goal>> GetAllGoalsAsync()
    {
        return await _goalRepository.GetAllAsync();
    }

    public async Task<Goal?> GetGoalByIdAsync(int id)
    {
        return await _goalRepository.GetByIdAsync(id);
    }

    public async Task<Goal> CreateGoalAsync(CreateGoalRequest request)
    {
        var goal = new Goal
        {
            Title = request.Title,
            Description = request.Description
        };

        return await _goalRepository.AddAsync(goal);

    }

    public async Task<Goal?> UpdateGoalAsync(
        int id,
        UpdateGoalRequest request)
    {
        var goal = await _goalRepository.GetByIdAsync(id);

        if (goal is null)
        {
            return null;
        }

        var wasCompleted = goal.IsCompleted;

        goal.Title = request.Title;
        goal.Description = request.Description;
        goal.IsCompleted = request.IsCompleted;

        if (!wasCompleted && goal.IsCompleted)
        {
            goal.CompletedAt = DateTime.UtcNow;
        }
        else if (wasCompleted && !goal.IsCompleted)
        {
            goal.CompletedAt = null;
        }

        await _goalRepository.UpdateAsync(goal);

        return goal;
    }

    public async Task<bool> DeleteGoalAsync(int id)
    {
        return await _goalRepository.DeleteAsync(id);
    }

}
