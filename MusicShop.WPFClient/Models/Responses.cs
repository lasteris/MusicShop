using System;

namespace MusicShop.WPFClient.Models
{
    public class SongResponse
    {
        public int Identity { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Album { get; set; }
        public string Image { get; set; }
        public DateTime DateRelease { get; set; }
        public double Price { get; set; }
    }   
    public class PublisherResponse
    {
        public string Image { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Site { get; set; }
        public int PerformerCount { get; set; }
    }
    public class AuthorResponse
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public int MusicCount { get; set; }
    }

    public class GenreResponse
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class ClientResponse
    {
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int Identity { get; set; }
    }
}
