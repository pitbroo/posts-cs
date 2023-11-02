using posts_cs.model;

namespace posts_cs.repository;

public interface IPostRepository
{
    Task<IEnumerable<Post>> GetAllPosts();
    Task<Post> GetPostById(int postId);
    Task<Post> CreatePost(Post post);
    Task UpdatePost(int postId, Post post);
    Task DeletePost(int postId);
}