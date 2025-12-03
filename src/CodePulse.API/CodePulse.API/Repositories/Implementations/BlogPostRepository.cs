using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Implementations;

public class BlogPostRepository : IBlogPostRepository
{
    private readonly ApplicationDbContext _dbContext;
    public BlogPostRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<BlogPost> CreateAsync(BlogPost blogPost)
    {
       await _dbContext.BlogPost.AddAsync(blogPost);
       await _dbContext.SaveChangesAsync();

       return blogPost;
    }

    public async Task<BlogPost?> DeleteAsync(Guid id)
    {
       var blogpost = await _dbContext.BlogPost.FirstOrDefaultAsync(x => x.Id == id);

        if(blogpost is not null)
        {
            _dbContext.BlogPost.Remove(blogpost);

            await _dbContext.SaveChangesAsync();

            return blogpost;
        }
         return null;
    }

    public async Task<IEnumerable<BlogPost>> GetAllAsync()
    {
        return await _dbContext.BlogPost
            .Include(blogPost => blogPost.Categories)
            .ToListAsync();
    }

    public async Task<BlogPost?> GetByIdAsync(Guid id)
    {
        return await _dbContext.BlogPost
             .Include(blogPost => blogPost.Categories)
             .FirstOrDefaultAsync(blogPost => blogPost.Id == id);
    }

    public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
    {
        var getBlogPost = await _dbContext.BlogPost
            .Include(c => c.Categories)
            .FirstOrDefaultAsync(x => x.Id == blogPost.Id);

        if (getBlogPost is null)
            return null;

        _dbContext.Entry(getBlogPost).CurrentValues.SetValues(blogPost);

        getBlogPost.Categories = blogPost.Categories;

        await _dbContext.SaveChangesAsync();

        return blogPost; 
    }
}
