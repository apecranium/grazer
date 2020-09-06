using System;

namespace Grazer.Models
{
    public class Post
    {
        public int Id { get; set; }
        public User Author { get; set; }
        public string Title { get; set; }
        public DateTime DatePosted { get; set; }
        public string BodyContent { get; set; }
    }
}
