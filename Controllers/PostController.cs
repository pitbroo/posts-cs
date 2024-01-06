using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using posts_cs.model;
using posts_cs.service;
using WebApi.Models;

namespace posts_cs.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class PostController : ControllerBase
{
    private readonly IPostService _postService;

    public PostController(IPostService postService)
    {
        _postService = postService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var posts = await _postService.GetAllPosts();
        return Ok(posts);
    }

    [HttpGet("{postId}")]
    public async Task<IActionResult> Get(int postId)
    {
        var post = await _postService.GetPostById(postId);
        if (post == null)
        {
            return NotFound();
        }
        return Ok(post);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreatePostRequest request)
    { 
        var post = new Post
        {
            Name = request.Name,
            Description = request.Description,
            UserId = request.UserId
        };
        var createdPost = await _postService.CreatePost(post);
        return CreatedAtAction("Get", new { postId = createdPost.Id }, createdPost);
    }

    [HttpPut("{postId}")]
    public async Task<IActionResult> Put(int postId, [FromBody] Post post)
    {
        await _postService.UpdatePost(postId, post);
        return NoContent();
    }
    [HttpPatch("{postId}")]
    public async Task<IActionResult> Patch(int postId, [FromBody] UpdatePostDto updateDto)
    {
        var postToUpdate = await _postService.GetPostById(postId);
        if (postToUpdate == null)
        {
            return NotFound();
        }

        if (updateDto.Name != null)
            postToUpdate.Name = updateDto.Name;

        if (updateDto.Description != null)
            postToUpdate.Description = updateDto.Description;
        
        await _postService.UpdatePost(postId, postToUpdate);
        return NoContent();
    }



    [HttpDelete("{postId}")]
    public async Task<IActionResult> Delete(int postId)
    {
        await _postService.DeletePost(postId);
        return NoContent();
    }
}
