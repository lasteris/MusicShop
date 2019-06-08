using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net;
using System.Security;
using System.Runtime.InteropServices;

namespace MusicShop.WPFClient.Models
{
    public sealed class Options
    {
        private static readonly Lazy<Options> Lazy = new Lazy<Options>(() => new Options());

        public static Options MusicOptions => Lazy.Value;
        private Options()
        {
            IsStraight = false;
            IsNew = false;
            IsTop = false;
            Count = 10;
            Author = "all";
            Publisher = "all";
            Genre = "all";
            User = new ClientResponse
            {
                Name = "Guest",
                Password = "Guest"
            };
        }
        public void Clear()
        {
            IsStraight = false;
            IsNew = false;
            IsTop = false;
            Count = 10;
            Author = "all";
            Publisher = "all";
            Genre = "all";
        }
        public bool IsStraight { get; set; }
        public bool IsNew { get; set; }
        public bool IsTop { get; set; }
        public int Count { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public string Genre { get; set; }

        public ClientResponse User { get; set; }
        public ViewModels.MainVM Main {get; set;}
    };
    public class MusicShopAPIHelper
    {
        private static HttpClient Сlient { get; set; }
        public Action<string> Log { get; set; }

        public MusicShopAPIHelper()
        {
            Сlient = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5000/")
            };
            Сlient.DefaultRequestHeaders.Accept.Clear();
            Сlient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public ICollection<SongResponse> GetAllMusicAsync(Options options)
        {
            var data = new List<SongResponse>();
            Task.Run(async () =>
            {
                var response = await Сlient.GetAsync($"api/v1/music?IsNew={options.IsNew}&IsTop={options.IsTop}&Count={options.Count}&Author={options.Author}&Publisher={options.Publisher}&Genre={options.Genre}");
                 data = await response.Content.ReadAsAsync<List<SongResponse>>();
            }).Wait();
            return data;
        }
        public ICollection<AuthorResponse> GetAllPerformersAsync(Options options)
        {
            var data = new List<AuthorResponse>();
            Task.Run(async () =>
            {
                var response = await Сlient.GetAsync($"api/v1/music/authors?publisher={options.Publisher}");
                data = await response.Content.ReadAsAsync<List<AuthorResponse>>();
            }).Wait();
            return data;
        }

        public ICollection<PublisherResponse> GetAllPublishersAsync()
        {
            var data = new List<PublisherResponse>();
            Task.Run(async () =>
            {
                var response = await Сlient.GetAsync("api/v1/music/publishers");
                data = await response.Content.ReadAsAsync<List<PublisherResponse>>();
            }).Wait();
            return data;
        }
        public  ICollection<GenreResponse> GetAllGenresAsync()
        {
            var data = new List<GenreResponse>();
            Task.Run(async () =>
            {
                var response = await Сlient.GetAsync("api/v1/music/genres");
                data = await response.Content.ReadAsAsync<List<GenreResponse>>();
            }).Wait();
            
            return data;
        }

        public bool TryLogin(string login, SecureString password)
        {
            var pass = ConvertToUnsecureString(password);
            var data = new ClientResponse();
            try
            {
                Task.Run(async () =>
                {
                    var response = await Сlient.GetAsync($"api/v1/client/{login}?key={pass}");

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        data = await response.Content.ReadAsAsync<ClientResponse>();
                        Options.MusicOptions.User = data;
                    }
                    else if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        throw new Exception("Такого пользователя не существует");
                    }
                    else if (response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        throw new Exception("Пароль недействителен");
                    }
                }).Wait();
            }
           catch(Exception ex)
            {
                Log(ex.Message);
            }

            return true;
        }

        private string ConvertToUnsecureString(SecureString securePassword)
        {
            if (securePassword == null)
            {
                return string.Empty;
            }

            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(securePassword);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }
    }
}
