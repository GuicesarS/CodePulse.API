using CodePulse.API.Models.Domain;
using CodePulse.API.Models.Dtos.BlogPost;
using CodePulse.API.Models.Dtos.Category;
using CodePulse.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {
        private readonly IBlogPostRepository _blogpostRepository;
        private readonly ICategoryRepository _categoryRepository;

        public BlogPostController(
            IBlogPostRepository blogpostRepository,
            ICategoryRepository categoryRepository)
        { 
            _blogpostRepository = blogpostRepository;
            _categoryRepository = categoryRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody]CreateBlogPostRequestDto requestDto)
        {
            var blogPost = new BlogPost
            {
                Title = requestDto.Title,
                ShortDescription = requestDto.ShortDescription,
                Content = requestDto.Content,
                FeaturedImageUrl = requestDto.FeaturedImageUrl,
                UrlHandle = requestDto.UrlHandle,
                PublishedDate = requestDto.PublishedDate,
                Author = requestDto.Author,
                IsVisible = requestDto.IsVisible,
                Categories = new List<Category>()
            };


            foreach(var categoryGuid in requestDto.Categories)
            {
                var existingCategory = await _categoryRepository.GetById(categoryGuid);

                if (existingCategory is not null)
                {
                    blogPost.Categories!.Add(existingCategory);
                }
            }

            blogPost = await _blogpostRepository.CreateAsync(blogPost);

            var response = new BlogPostDto
            {
                Title = blogPost.Title,
                ShortDescription = blogPost.ShortDescription,
                Content = blogPost.Content,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                UrlHandle = blogPost.UrlHandle,
                PublishedDate = blogPost.PublishedDate,
                Author = blogPost.Author,
                IsVisible = blogPost.IsVisible,
                Categories = blogPost.Categories!.Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    UrlHandle = c.UrlHandle
                }).ToList()

            };

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBlogPosts()
        {
            var blogPosts = await _blogpostRepository.GetAllAsync();

            var response = new List<BlogPostDto>();
            foreach (var blogPost in blogPosts)
            {
                response.Add(new BlogPostDto
                {
                    Id = blogPost.Id,
                    Title = blogPost.Title,
                    ShortDescription = blogPost.ShortDescription,
                    Content = blogPost.Content,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    UrlHandle = blogPost.UrlHandle,
                    PublishedDate = blogPost.PublishedDate,
                    Author = blogPost.Author,
                    IsVisible = blogPost.IsVisible,
                    Categories = blogPost.Categories!.Select(c => new CategoryDto
                    {
                        Id = c.Id,
                        Name = c.Name,
                        UrlHandle = c.UrlHandle
                    }).ToList()
                });

            }

            return Ok(response);

        }
    }
}
