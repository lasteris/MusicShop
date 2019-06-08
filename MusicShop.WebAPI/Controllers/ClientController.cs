using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
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

        //Получить данные профиля

        // GET api/v1/client
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
                                        Phone = c.ClPhone
                                    }).SingleOrDefault();

                if(response != null)
                {
                    return Ok(response);
                }
                return BadRequest();
            }
            return NotFound();
        }

        [HttpGet("Profile")]
        public IActionResult GetClientProfile(string login, string password)
        {
            var response = _context.Clients.Where(c => c.ClLogin == login && c.ClPassword == password);

            if (response is Client)
            {
                return Ok(new JsonResult(response));
            }

            return NotFound();
        }

        [HttpPut("Profile")]
        public IActionResult EditClientProfile(Client client)
        {
            Client response = _context.Clients.Where(c => c.ClId == client.ClId).FirstOrDefault();

            if (response != null)
            {
                response = client;

                _context.SaveChanges();

                return Ok();
            }

            return BadRequest();
        }

        // POST api/v1/client
        [HttpPost]
        public IActionResult CreateClientProfile(Client client)
        {
            if (_context.Clients.Find(client) != null)
            {
                return Conflict();
            }
            _context.Clients.Add(client);

            _context.SaveChanges();

            return Ok();
        }

        //[HttpGet("Orders")]
        [HttpPost("Order")]
        public IActionResult CreateOrder(string login, string pass, List<Song> songs)
        {

            Client client = _context.Clients.Where(c => c.ClLogin == login && c.ClPassword == pass).FirstOrDefault();

            if (client != null)
            {
                var purchase = new Purchase
                {
                    ClId = client.ClId,
                    PurDate = DateTime.Now,
                    TotalQty = songs.Count(),
                    TotalSum = songs.Sum(s => s.Price)
                };
                _context.Purchases.Add(purchase);

                _context.SaveChanges();

                foreach (var song in songs)
                {
                    _context.Includes.Add(new Includes { ClId = client.ClId, SongId = song.SongId, PurId = purchase.PurId });
                }
                _context.SaveChanges();

                return Ok();
            }


            return null;
        }

        [HttpGet("History")]
        public IActionResult GetHistory(string login, string pass)
        {
            List<Song> _songs = new List<Song>();

            Client client = _context.Clients
                .Where(c => c.ClLogin == login && c.ClPassword == pass).FirstOrDefault();

            var orders = _context.Purchases.Where(p => p.ClId == client.ClId);

            foreach (var order in orders)
            {
                var songs = order.Includes.Select(i => i.Song).ToList();
                _songs.AddRange(songs);
            }

            return Ok(_songs);
        }

    }
}