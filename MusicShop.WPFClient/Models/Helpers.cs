using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace MusicShop.WPFClient.Models
{
    public class MusicShopAPIHelper
    {
        private static HttpClient client { get; set; }

        public MusicShopAPIHelper()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5000/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<ICollection<SongResponse>> GetAllMusicAsync()
        {
            var response = await client.GetAsync("api/v1/music");
            var data = await response.Content.ReadAsAsync<List<SongResponse>>();

            return data;
        }
        public async Task<ICollection<AuthorResponse>> GetAllPerformers()
        {
            var response = await client.GetAsync("api/v1/music/authors");
            var data = await response.Content.ReadAsAsync<List<AuthorResponse>>();

            return data;
        }
        public async Task<ICollection<PublisherResponse>> GetAllPublishers()
        {
            var response = await client.GetAsync("api/v1/music/publishers");
            var data = await response.Content.ReadAsAsync<List<PublisherResponse>>();

            return data;
        }
        public async Task<ICollection<GenreResponse>> GetAllGenres()
        {
            var response = await client.GetAsync("api/v1/music/genres");
            var data = await response.Content.ReadAsAsync<List<GenreResponse>>();

            return data;
        }

    }
}
