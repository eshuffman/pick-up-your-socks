
using server.Models;
using server.DTOs;

namespace server.Services;

public interface IGoalService
{
    Task<List<Goal>> GetAllGoalsAsync();
    Task<Goal?> GetGoalByIdAsync(int id);
    Task<Goal> CreateGoalAsync(CreateGoalRequest request);
    Task<Goal?> UpdateGoalAsync(int id, UpdateGoalRequest request);
    Task<bool> DeleteGoalAsync(int id);
}
