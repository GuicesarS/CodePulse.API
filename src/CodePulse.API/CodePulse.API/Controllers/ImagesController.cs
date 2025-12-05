using CodePulse.API.Models.Domain;
using CodePulse.API.Models.Dtos.BlogImage;
using CodePulse.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ImagesController : ControllerBase
{
    private readonly IBlogImageRepository _imageRepository;

    public ImagesController(IBlogImageRepository imageRepository)
    {
        _imageRepository = imageRepository;
    }

    [HttpPost]
    public async Task<IActionResult> UploadImage([FromForm] UploadImageRequest request)
    {
        ValidateForm(request.File);

        if (ModelState.IsValid)
        {
            var blogImage = new BlogImage
            {
                FileExtension = Path.GetExtension(request.File.FileName).ToLower(),
                FileName = request.FileName,
                Title = request.Title,
                DateCreated = DateTime.UtcNow
            };

            blogImage = await _imageRepository.Upload(request.File, blogImage);

            var response = new BlogImageDto
            {
                Id = blogImage.Id,
                FileExtension = blogImage.FileExtension,
                FileName = blogImage.FileName,
                Title = blogImage.Title,
                Url = blogImage.Url,
                DateCreated = blogImage.DateCreated
            };

            return Ok(response);
        }

        return BadRequest(ModelState);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllImages()
    {
        var blogImages = await _imageRepository.GetAllAsync();

        var response = blogImages.Select(blogImage => new BlogImageDto
        {
            Id = blogImage.Id,
            FileExtension = blogImage.FileExtension,
            FileName = blogImage.FileName,
            Title = blogImage.Title,
            Url = blogImage.Url,
            DateCreated = blogImage.DateCreated
        });

        return Ok(response);
    }

    private void ValidateForm(IFormFile file)
    {
        var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

        if (!allowedExtensions.Contains(Path.GetExtension(file.FileName).ToLower()))
            ModelState.AddModelError("file", "Invalid file type. Only .jpg, .jpeg, and .png are allowed.");

        if (file.Length > 10485760)
            ModelState.AddModelError("file", "File size cannot be more than 10MB");
    }
}