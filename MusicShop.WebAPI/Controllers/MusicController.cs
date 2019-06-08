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
        public IActionResult GetAllMusic(bool isNew= false, bool isTop = false, int count= 10, string author= "all", string publisher = "all", string genre = "all")
        {

            var response = new List<SongResponse>();

            var songs = _context.Songs
                .Include(s => s.Al)
                .Include(s => s.Au).AsQueryable();

                #region Фильтр по НОВОЙ музыке
                if (isNew)
                {
                    songs = songs
                        .OrderByDescending(s => s.Al.AlDateRelease)
                        .Take(count);
                }
                #endregion

                #region Фильтр по ТОП-музыке
                if (isTop)
                {
                    var groups = _context.Includes.GroupBy(i => i.SongId)
                                              .Select(g => new { SongId = g.Key, Total = g.Count() })
                                              .OrderByDescending(g => g.Total)
                                              .Take(count);

                    foreach (var g in groups)
                    {

                        var song = songs.Where(s => s.SongId == g.SongId)
                                        .Select(s => new SongResponse
                                        {
                                            Name = s.SongName,
                                            Author = s.Au.AuName,
                                            Album = s.Al.AlName,
                                            Image = s.Al.AlImage,
                                            DateRelease = s.Al.AlDateRelease
                                        }).Single();

                        response.Add(song);

                    }
                    return Ok(response);
                }
                #endregion

                #region Фильтр по Исполнителям
                if (author != "all")
                {
                    songs = songs.Where(s => s.Au.AuName == author);
                }
                #endregion

                #region Фильтр по Лейблам
                if (publisher != "all")
                {
                    songs = songs.Include(s => s.Au)
                        .ThenInclude(au => au.Pub)
                        .Where(s => s.Au.Pub.PubName == publisher);
                }
            #endregion

            #region Фильтр по жанрам
                if(genre != "all")
                {
                songs = songs.Include(s => s.Genre).Where(g => g.Genre.GenreName == genre);
                }
            #endregion

            #region формирование результирующей выборки
            response = songs.Select(s => new SongResponse
            {
                Name = s.SongName,
                Author = s.Au.AuName,
                Album = s.Al.AlName,
                Image = s.Al.AlImage,
                DateRelease = s.Al.AlDateRelease
            }).ToList();
            #endregion

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
            var response = _context.Publishers
                .Include(p => p.Author)
                .Select(pub => new PublisherResponse()
                {

                    Name = pub.PubName,
                    Image = pub.PubImage,
                    Site = pub.PubWebsite,
                    PerformerCount = pub.Author.Count()
                }).ToList();

            return Ok(response);
        }

        //// Get api/v1/music/top
        //[HttpGet("top")]
        //public IActionResult GetTopMusic(int count = 10)
        //{
        //    List<SongResponse> response = new List<SongResponse>();
        //    var groups = _context.Includes.GroupBy(i => i.SongId)
        //                                  .Select(g => new { SongId = g.Key, Total = g.Count() })
        //                                  .OrderByDescending(g => g.Total)
        //                                  .Take(count);

        //    if (groups is null)
        //        return NotFound();

        //    foreach (var g in groups)
        //    {
        //        var song = _context.Songs
        //            .Where(s => s.SongId == g.SongId)
        //            .Include(s => s.Al)
        //            .Include(s => s.Au)
        //            .Select(s => new SongResponse
        //            {
        //                Name = s.SongName,
        //                Author = s.Au.AuName,
        //                Album = s.Al.AlName,
        //                Image = s.Al.AlImage,
        //                DateRelease = s.Al.AlDateRelease
        //            }).First();

        //        response.Add(song);
        //    }
        //    return Ok(response);
        //}

    

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

            return new JsonResult(response);
        }


        [HttpGet("authors")]
        public IActionResult GetAuthors(string publisher = "all")
        {
            var authors = _context.Authors.Include(au => au.Song).AsQueryable();

            #region Фильтр по Лейблам
            if(publisher != "all")
            {
                authors = authors.Include(au => au.Pub)
                                 .Where(au => au.Pub.PubName == publisher);
            }
            #endregion

            var response = authors.Select(ar => new AuthorResponse()
            {
                Name = ar.AuName,
                Image = ar.AuImage,
                MusicCount = ar.Song.Count
            }).ToList();

            return Ok(response);
        }

    }
}
