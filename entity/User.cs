using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace posts_cs.model;

[Table("users")]
public class User
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    [Column("first_name")]
    public string FirstName { get; set; }
    [Column("last_name")]

    public string LastName { get; set; }
    [Column("username")]

    public string Username { get; set; }
    [JsonIgnore]
    [Column("password")]

    public string Password { get; set; }
    [JsonIgnore]
    public virtual ICollection<Post> Posts { get; set; }
    [JsonIgnore]
    public virtual ICollection<Comment> Comments { get; set; }
}

