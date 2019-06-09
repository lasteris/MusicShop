using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicShop.WebAPI.Extensions;
using MusicShop.WebAPI.Model;

namespace MusicShop.WebAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private MusicShopContext _context;

        public ClientController(MusicShopContext context)
        {
            _context = context;
        }


        // GET api/v1/client/login
        [HttpGet("{login}")]
        public IActionResult Login(string login, string key)
        {
            var query = _context.Clients.Where(c => c.ClLogin == login).AsQueryable();
            
            if(query != null)
            {
                var response = query.Where(c => c.ClPassword == key)
                                    .Select( c => new ClientResponse
                                    {
                                        Name = c.ClName,
                                        Password = c.ClPassword,
                                        Email = c.ClEmail,
                                        Login = c.ClLogin,
                                        Phone = c.ClPhone,
                                        Identity = c.ClId
                                    }).SingleOrDefault();

                if(response != null)
                {
                    return Ok(response);
                }
                return BadRequest();
            }
            return NotFound();
        }

        [HttpPut("{login}/Profile")]
        public async Task<IActionResult> EditClientProfile(string login, [FromBody]ClientResponse client)
        {
            var request = _context.Clients.Where(c => c.ClId == client.Identity).SingleOrDefault();

            if (request == null)
            {
                return BadRequest();
            }

            request.ClLogin = client.Login;
            request.ClPassword = client.Password;
            request.ClPhone = client.Phone;
            request.ClEmail = client.Email;

            _context.Clients.Update(request);
            int result = await _context.SaveChangesAsync();           

            return Ok(client);
        }

        // POST api/v1/client/new
        [HttpPost("new")]
        public async Task<IActionResult> CreateClientProfile([FromBody]ClientResponse client)
        {
            var existed = _context.Clients.Where(c => c.ClLogin == client.Login && c.ClPassword == client.Password)
                                           .SingleOrDefault();

            if(existed != null)
            {
                return Conflict();
            }

            _context.Clients.Add(new Client {
                ClName = client.Name,
                ClPassword = client.Password,
                ClEmail = client.Email,
                ClPhone = client.Phone,
                ClLogin = client.Login
            });

            int result = await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("{id}/Order")]
        public async Task<IActionResult> CreateOrder(int id, [FromBody]List<SongResponse> songs)
        {

            Client client = _context.Clients.Where(c => c.ClId == id).SingleOrDefault();

            if (client == null)
            {
                return BadRequest();
            }

            #region несовместимость разных жанров
            foreach (var outer in songs)
            {
                var validate = _context.Songs.Include(s => s.Genre).Where(s => s.SongId == outer.Identity).Single();

                foreach(var inner in songs)
                {
                    var innerSong = _context.Songs.Include(s => s.Genre).Where(s => s.SongId == inner.Identity).Single();
                    var innerGenre = _context.Genres.Where(g => g.GenreId == validate.GenreId).Single();

                    if(innerGenre.GenreId != validate.GenreId)
                    {
                        return BadRequest();
                    }
                }
            }
            #endregion


            var purchase = new Purchase();
            purchase.ClId = client.ClId;
            purchase.PurDate = DateTime.Now;
            purchase.TotalQty = songs.Count;

            #region каждая десятая бесплатна
            purchase.TotalSum = 0.0;

            for (int i = 0; i < songs.Count; ++i)
            {
                if ( i != 0 && i % 9 == 0)
                    continue;

                purchase.TotalSum += songs[i].Price;
            }
            #endregion

            _context.Purchases.Add(purchase);

            int result = await _context.SaveChangesAsync();

            foreach (var song in songs)
            {
                _context.Includes.Add(new Includes
                {
                    ClId = client.ClId,
                    SongId = song.Identity,
                    PurId = purchase.PurId
                });
            }
            result = await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("{id}/History")]
        public async Task<IActionResult> GetHistory(int id)
        {

            var client = await _context.Clients
                .Where(c => c.ClId == id).SingleOrDefaultAsync();

            if(client == null)
            {
                return NotFound();
            }


            var purchases = _context.Includes.Where(i => i.ClId == id).ToList();

            var response = new List<SongResponse>();

            foreach(var p in purchases)
            {
                var song = _context.Songs.Include(s => s.Au).Include(s => s.Al).Where(s => s.SongId == p.SongId).Single();

                response.Add(new SongResponse
                {
                    Name = song.SongName,
                    Album = song.Al.AlName,
                    Author = song.Au.AuName,
                    DateRelease = song.Al.AlDateRelease,
                    Image = song.Al.AlImage
                });
            }

            return Ok(response);
        }

    }
}