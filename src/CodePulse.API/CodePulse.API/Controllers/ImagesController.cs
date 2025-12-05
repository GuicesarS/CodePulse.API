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

    public IBlogImageRepository ImageRepository { get; }

    [HttpPost]
    public async Task<IActionResult> UploadImage(
        [FromForm] IFormFile file, 
        [FromForm] string fileName,
        [FromForm] string title)
    {
        ValidateForm(file);

        if(ModelState.IsValid)
        {
            var blogImage = new BlogImage
            {
                FileExtension = Path.GetExtension(file.FileName).ToLower(),
                FileName = fileName,
                Title = title,
                DateCreated = DateTime.UtcNow
            };

            blogImage = await _imageRepository.Upload(file, blogImage);

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
    private void ValidateForm(IFormFile file)
    {
        var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

        if(!allowedExtensions.Contains(Path.GetExtension(file.FileName).ToLower()))
            ModelState.AddModelError("file", "Invalid file type. Only .jpg, .jpeg, and .png are allowed.");

        if (file.Length > 10485760)
            ModelState.AddModelError("file", "File size cannot be more than 10MB");
        
    }
}
