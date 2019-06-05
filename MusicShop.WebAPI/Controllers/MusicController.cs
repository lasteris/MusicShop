using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicShop.WebAPI.Extensions;
using MusicShop.WebAPI.Model;

namespace MusicShop.WebAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MusicController : ControllerBase
    {
        private  readonly MusicShopContext _context;

        public MusicController(MusicShopContext context)
        {
            _context = context;
        }
        // GET api/v1/music
        [HttpGet]
        public IActionResult GetAllMusic()
        {
            var songs = _context.Songs
                .Include(s => s.Au)
                .Include(s => s.Al).ToList();

            List<SongResponse> response = new List<SongResponse>();

            foreach (var song in songs)
            {
                response.Add(new SongResponse
                {
                    Name = song.SongName,
                    Author = song.Au.AuName,
                    Album = song.Al.AlName,
                    Image = song.Al.AlImage,
                    DateRelease = song.Al.AlDateRelease
                });
            }

            return Ok(response);
        }

        [HttpGet("download")]
        public IActionResult Download()
        {
            return PhysicalFile(@"d:\INNA.mp4", "application/octet-stream");
        }

        // Get api/v1/music/publishers
        [HttpGet("publishers")]
        public IActionResult GetPublishers()
        {
            var publishers = _context.Publishers.Include(p => p.Author).ToList();

            List<PublisherResponse> response = new List<PublisherResponse>();

            foreach (var pub in publishers)
            {
                response.Add(new PublisherResponse
                {
                    Name = pub.PubName,
                    Image = pub.PubImage,
                    PerformerCount = pub.Author.Count()
                });
            }

            return Ok(response);
        }

        // Get api/v1/music/top
        [HttpGet("top")]
        public IActionResult GetTopMusic(int count = 10)
        {
            List<SongResponse> response = new List<SongResponse>();
            var groups = _context.Includes.GroupBy(i => i.SongId)
                                          .Select(g => new { SongId = g.Key, Total = g.Count() })
                                          .OrderByDescending(g => g.Total)
                                          .Take(count);

            if (groups is null)
                return NotFound();

            foreach (var g in groups)
            {
                var song = _context.Songs
                    .Where(s => s.SongId == g.SongId)
                    .Include(s => s.Al)
                    .Include(s => s.Au)
                    .Select(s => new SongResponse
                    {
                        Name = s.SongName,
                        Author = s.Au.AuName,
                        Album = s.Al.AlName,
                        Image = s.Al.AlImage,
                        DateRelease = s.Al.AlDateRelease
                    }).First();

                response.Add(song);
            }
            return Ok(response);
        }

    

        [HttpGet("genres")]
        public IActionResult GetGenres()
        {
            var response = _context.Genres.Select(g => new GenreResponse
            {
                Name = g.GenreName,
                Description = g.GenreDesc
            }).ToList();

            if (response == null)
                return NotFound();

            return Ok(response);
        }


        [HttpGet("authors")]
        public IActionResult GetAuthors()
        {
            var authors = _context.Authors.Include(p => p.Song).ToList();

           var response = new List<AuthorResponse>();

            foreach (var author in authors)
            {
                response.Add(new AuthorResponse
                {
                  Name = author.AuName,
                  Image = author.AuImage,
                  MusicCount = author.Song.Count()
                });
            }

            return Ok(response);
        }

    }
}
