using System.ComponentModel.DataAnnotations.Schema;

namespace posts_cs.model
{
    [Table("posts")]
    public class Post
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("description")]
        public string Description { get; set; }
        [Column("photo")]
        public string Photo { get; set; }
        [Column("comments")]
        public List<Comment> Comments { get; set; }
    }
    [Table("comments")]
    public class Comment
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("text")]
        public string Text { get; set; }
    }

}
