using Microsoft.EntityFrameworkCore;
using posts_cs.model;

namespace posts_cs.repository;

public interface ICommentRepository
{
    Task<IEnumerable<Comment>> GetAllComments();
    Task<Comment> GetCommentById(int commentId);
    Task<Comment> CreateComment(Comment comment);
    Task UpdateComment(int commentId, Comment comment);
    Task DeleteComment(int commentId);
}
public class CommentRepository : ICommentRepository
{
    private readonly ApplicationDbContext _dbContext;

    public CommentRepository(ApplicationDbContext dbContext)
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseNpgsql("Host=localhost;Database=posts;Username=hexagonal;Password=hexagonal")
            .Options;

        _dbContext = new ApplicationDbContext(options);
    }

    public async Task<IEnumerable<Comment>> GetAllComments()
    {
        return await _dbContext.Comments.ToListAsync();
    }

    public async Task<Comment> GetCommentById(int commentId)
    {
        return await _dbContext.Comments.FindAsync(commentId);
    }

    public async Task<Comment> CreateComment(Comment comment)
    {
        _dbContext.Comments.Add(comment);
         await _dbContext.SaveChangesAsync();
        return comment;
    }

    public async Task UpdateComment(int commentId, Comment comment)
    {
        var existingComment = await _dbContext.Comments.FindAsync(commentId);
        if (existingComment != null)
        {
            existingComment.Text = comment.Text;
            // Aktualizacja innych pól w razie potrzeby
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task DeleteComment(int commentId)
    {
        var comment = await _dbContext.Comments.FindAsync(commentId);
        if (comment != null)
        {
            _dbContext.Comments.Remove(comment);
            await _dbContext.SaveChangesAsync();
        }
    }
}
