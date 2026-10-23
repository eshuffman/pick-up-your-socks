
using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Models;

namespace server.Repositories;

public class GoalRepository : IGoalRepository
{
    private readonly AppDbContext _context;

    public GoalRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Goal>> GetAllAsync()
    {
        return await _context.Goals.ToListAsync();
    }

    public async Task<Goal?> GetByIdAsync(int id)
    {
        return await _context.Goals.FindAsync(id);
    }

    public async Task<Goal> AddAsync(Goal goal)
    {
        _context.Goals.Add(goal);
        await _context.SaveChangesAsync();

        return goal;
    }

    public async Task UpdateAsync(Goal goal)
    {
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var goal = await _context.Goals.FindAsync(id);

        if (goal is null)
        {
            return false;
        }

        _context.Goals.Remove(goal);
        await _context.SaveChangesAsync();

        return true;
    }
}
