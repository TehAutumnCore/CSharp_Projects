using Microsoft.EntityFrameworkCore;
using Blog.Models;

namespace Blog.Data
{
    public class PortfolioDbContext  : DbContext
    {
        public DbSet<BlogPost> Blogs { get; set; }
        public DbSet<AppUser> Users{ get; set; }

        public PortfolioDbContext (DbContextOptions<PortfolioDbContext > options) : base(options)
        {

        }
        
    }
}