
using server.Models;

namespace server.Repositories;

public interface IGoalRepository
{
    Task<List<Goal>> GetAllAsync();
    Task<Goal?> GetByIdAsync(int id);
    Task<Goal> AddAsync(Goal goal);
    Task UpdateAsync(Goal goal);
    Task<bool> DeleteAsync(int id);
}
