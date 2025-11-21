using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public CategoriesController(ApplicationDbContext dbContext) => _context = dbContext;

    [HttpPost]
    public async Task<IActionResult> CreateCategory(CreateCategoryRequestDto request)
    {
        var category = new Category
        {
            Name = request.Name,
            UrlHandle = request.UrlHandle,
        };

        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();

        var response = new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            UrlHandle = category.UrlHandle,
        };

        return Ok(response);

    }
}
