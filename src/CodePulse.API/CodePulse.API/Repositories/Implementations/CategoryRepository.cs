using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.Dtos;
using CodePulse.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Implementations;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _dbContext;
    public CategoryRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

    public async Task<Category> CreateAsync(Category category)
    {
      
        await _dbContext.Categories.AddAsync(category);
        await _dbContext.SaveChangesAsync();

        return category;
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _dbContext.Categories.ToListAsync();
    }

    public async Task<Category?> GetById(Guid id)
    {
        return await _dbContext.Categories.FirstOrDefaultAsync(category => category.Id == id);
    }

    public async Task<Category?> UpdateAsync(Category category)
    {
        var getCategory = await _dbContext.Categories.FirstOrDefaultAsync(cat => cat.Id == category.Id);
        
        if (getCategory != null)
        {
            _dbContext.Entry(getCategory).CurrentValues.SetValues(category);

            await _dbContext.SaveChangesAsync();

            return category;
        }

        return null;
    }
}
