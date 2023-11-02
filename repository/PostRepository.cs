using Microsoft.EntityFrameworkCore;
using posts_cs.model;


namespace posts_cs.repository
{


    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PostRepository(ApplicationDbContext dbContext)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseNpgsql("Host=localhost;Database=posts;Username=hexagonal;Password=hexagonal")
                .Options;

            _dbContext = new ApplicationDbContext(options);
        }

        public async Task<IEnumerable<Post>> GetAllPosts()
        {
            return await _dbContext.Posts.Include(p => p.Comments).ToListAsync();
        }

        public async Task<Post> GetPostById(int postId)
        {
            return await _dbContext.Posts.Include(p => p.Comments)
                .FirstOrDefaultAsync(p => p.Id == postId);
        }

        public async Task<Post> CreatePost(Post post)
        {
            _dbContext.Posts.Add(post);
            await _dbContext.SaveChangesAsync();
            return post;
        }

        public async Task UpdatePost(int postId, Post post)
        {
            var existingPost = await GetPostById(postId);
            if (existingPost != null)
            {
                existingPost.Name = post.Name;
                existingPost.Description = post.Description;
                existingPost.Photo = post.Photo;
                // Update other properties as needed
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DeletePost(int postId)
        {
            var post = await GetPostById(postId);
            if (post != null)
            {
                _dbContext.Posts.Remove(post);
                await _dbContext.SaveChangesAsync();
            }
        }
    }

}
