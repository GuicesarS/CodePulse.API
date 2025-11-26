using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.Dtos;
using CodePulse.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryRepository _categoryRepository;
    public CategoriesController(ICategoryRepository categoryRepository) => _categoryRepository = categoryRepository;

    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequestDto request)
    {
        var category = new Category
        {
            Name = request.Name,
            UrlHandle = request.UrlHandle,
        };

        await _categoryRepository.CreateAsync(category);

        var response = new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            UrlHandle = category.UrlHandle,
        };

        return Ok(response);

    }

    [HttpGet]
    public async Task<IActionResult> GetAllCategory()
    {
        var categories = await _categoryRepository.GetAllAsync();

        var response = new List<CategoryDto>();

        foreach (var category in categories)
        {
            response.Add(new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle,
            });
        }

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryById([FromRoute] Guid id)
    {
        var getCategory = await _categoryRepository.GetById(id);

        if (getCategory is null)
            return NotFound();

        var response = new CategoryDto
        {
            Id = getCategory.Id,
            Name = getCategory.Name,
            UrlHandle = getCategory.UrlHandle,
        };

        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory([FromRoute] Guid id, UpdateCategoryRequestDto requestDto)
    {
        var category = new Category
        {
            Id = id,
            Name = requestDto.Name,
            UrlHandle = requestDto.UrlHandle,
        };

        category = await _categoryRepository.UpdateAsync(category);

        if (category == null)
            return NotFound();

        var response = new CategoryDto
        {
            Id = id,
            Name = category.Name,
            UrlHandle = category.UrlHandle,
        };

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
    {
        var category = await _categoryRepository.DeleteAsync(id);

        if (category == null)
            return NotFound();

        var response = new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            UrlHandle = category.UrlHandle,
        };

        return Ok();

    }
}

