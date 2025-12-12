using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Data;

public class AuthDbContext: IdentityDbContext
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
    {
    }
    override protected void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        var readerRoleId = "8932bf5e-ee24-40e5-9d59-7393f86bb46c";
        var writerRoleId = "98a8379d-4e3b-4612-b4b6-a4297bc432ed";

        var roles = new List<IdentityRole>
        {
            new IdentityRole()
            {
                 Id = readerRoleId,
                 Name = "Reader",
                 NormalizedName = "reader".ToUpper(),
                 ConcurrencyStamp = readerRoleId
            },
            new IdentityRole()
            {
                 Id = writerRoleId,
                 Name = "Writer",
                 NormalizedName = "writer".ToUpper(),
                 ConcurrencyStamp = writerRoleId
            }
        };

        
        builder.Entity<IdentityRole>().HasData(roles);

        var adminUserId = "328aa706-ffa1-4c57-a73c-d9407fdf1ebf";
        var admin = new IdentityUser()
        {
            Id = adminUserId,                        
            UserName = "admin",                     
            Email = "admin@codepulse.com",          
            NormalizedEmail = "admin@codepulse.com".ToUpper(), 
            NormalizedUserName = "admin".ToUpper()   
        };

        
        admin.PasswordHash = new PasswordHasher<IdentityUser>()
            .HashPassword(admin, "Admin@123");

        builder.Entity<IdentityUser>().HasData(admin);

        var adminRoles = new List<IdentityUserRole<string>>()
        {
            
            new()
            {
                UserId = adminUserId,    
                RoleId = readerRoleId    
            },
            new()
            {
                UserId = adminUserId,   
                RoleId = writerRoleId  
            }
        };

        builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);

    }
}
