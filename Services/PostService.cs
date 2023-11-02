using posts_cs.model;
using posts_cs.repository;

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

    public PostService(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<IEnumerable<Post>> GetAllPosts()
    {
        return await _postRepository.GetAllPosts();
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
