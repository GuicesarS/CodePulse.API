using Azure.Core;
using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.Dtos;
using CodePulse.API.Repositories.Interfaces;

namespace CodePulse.API.Repositories.Implementations;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _context;
    public CategoryRepository(ApplicationDbContext dbContext) => _context = dbContext;

    public async Task<Category> CreateAsync(Category category)
    {
      
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();

        return category;
    }
}
