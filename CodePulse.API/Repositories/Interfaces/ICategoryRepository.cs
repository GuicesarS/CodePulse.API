using CodePulse.API.Models.Domain;

namespace CodePulse.API.Repositories.Interfaces;

public interface ICategoryRepository
{
    Task<Category> CreateAsync(Category category);
}
