namespace WebApi.Models;

public class CreatePostRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int UserId { get; set; }
}