﻿using System.ComponentModel.DataAnnotations;
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
        public string Photo { get; set; } = string.Empty; 

        [Column("comments")]
        public List<Comment> Comments { get; set; } = new List<Comment>();
        [Column("user_id")] 
        public int? UserId { get; set; }

        [ForeignKey("UserId")]
        public User Author { get; set; }
    }
    [Table("comments")]
    public class Comment
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("text")]
        public string Text { get; set; }
        [Column("user_id")] 
        public int UserId { get; set; }

        [ForeignKey("UserId")] 
        public User Author { get; set; }
    }

}
