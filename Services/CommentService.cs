using posts_cs.model;
using posts_cs.repository;

namespace posts_cs.Services;

public interface ICommentService
{
    Task<IEnumerable<Comment>> GetAllComments();
    Task<Comment> GetCommentById(int commentId);
    Task<Comment> CreateComment(int postId, Comment comment);
    Task UpdateComment(int commentId, Comment comment);
    Task DeleteComment(int commentId);
}
public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly IPostRepository _postRepository;
    private readonly IUserService _userService;

    public CommentService(ICommentRepository commentRepository, IPostRepository postRepository, IUserService userService)
    {
        _commentRepository = commentRepository;
        _postRepository = postRepository;
        _userService = userService;
    }

    public async Task<IEnumerable<Comment>> GetAllComments()
    {
        return await _commentRepository.GetAllComments();
    }

    public async Task<Comment> GetCommentById(int commentId)
    {
        return await _commentRepository.GetCommentById(commentId);
    }

    public async Task<Comment> CreateComment(int postId, Comment comment)
    {
        var post = await _postRepository.GetPostById(postId);
        if (post == null)
        {
            throw new ArgumentException("Post with given ID does not exist");
        }

        var author = _userService.GetById(comment.UserId);
        if (author == null)
        {
            comment.Author = null;
            throw new ArgumentException("User with given ID does not exist");
        }
        comment.Author = author;
        

        post.Comments.Add(comment);
        await _postRepository.UpdatePost(post.Id, post); 

        return comment;
    }


    public async Task UpdateComment(int commentId, Comment comment)
    {
        var existingComment = await _commentRepository.GetCommentById(commentId);
        if (existingComment == null)
        {
            throw new ArgumentException("Comment with given ID does not exist");
        }

        // Aktualizacja tylko wymaganych pól
        existingComment.Text = comment.Text;
        // Aktualizacja innych pól w razie potrzeby

        await _commentRepository.UpdateComment(commentId, existingComment);
    }

    public async Task DeleteComment(int commentId)
    {
        var comment = await _commentRepository.GetCommentById(commentId);
        if (comment == null)
        {
            throw new ArgumentException("Comment with given ID does not exist");
        }

        await _commentRepository.DeleteComment(commentId);
    }
}
