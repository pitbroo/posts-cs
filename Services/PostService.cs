using posts_cs.model;
using posts_cs.repository;
using posts_cs.Services;

namespace posts_cs.service;


public interface IPostService
{
    Task<IEnumerable<Post>> GetAllPosts();
    Task<Post> GetPostById(int postId);
    Task<Post> CreatePost(Post post);
    Task UpdatePost(int postId, Post post);
    Task DeletePost(int postId);
}
public class PostService : IPostService
{
    private readonly IPostRepository _postRepository;
    private readonly IUserService _userService;

    public PostService(IPostRepository postRepository, IUserService userService)
    {
        _postRepository = postRepository;
        _userService = userService;
    }

    public async Task<IEnumerable<Post>> GetAllPosts()
    {
        var posts = await _postRepository.GetAllPosts();

        foreach (var post in posts)
        {
            foreach (var comment in post.Comments)
            {
                var author = _userService.GetById(comment.UserId);
                comment.Author = author;
            }
        }

        return posts;
    }

    public async Task<Post> GetPostById(int postId)
    {
        return await _postRepository.GetPostById(postId);
    }

    public async Task<Post> CreatePost(Post post)
    {
        return await _postRepository.CreatePost(post);
    }

    public async Task UpdatePost(int postId, Post post)
    {
        await _postRepository.UpdatePost(postId, post);
    }

    public async Task DeletePost(int postId)
    {
        await _postRepository.DeletePost(postId);
    }
}
