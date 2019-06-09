using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net;
using System.Security;
using System.Runtime.InteropServices;
using NAudio.Wave;
using System.Threading;

namespace MusicShop.WPFClient.Models
{
    public class APIHelper
    {
        private HttpClient Client { get; set; }
        public Action<string> Log { get; set; }

        public WaveOutEvent WO { get; set; } = new WaveOutEvent();

        public APIHelper()
        {
            Client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5000/")
            };
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public ICollection<SongResponse> GetAllMusicAsync(Options options)
        {
            var data = new List<SongResponse>();
            try
            {
                Task.Run(async () =>
                {
                    var response = await Client.GetAsync($"api/v1/music?IsNew={options.IsNew}&IsTop={options.IsTop}&Count={options.Count}&Author={options.Author}&Publisher={options.Publisher}&Genre={options.Genre}");
                    data = await response.Content.ReadAsAsync<List<SongResponse>>();
                }).Wait();
            }
           catch(Exception ex)
            {
                Log(ex.Message);
            }
            return data;
        }
        public ICollection<AuthorResponse> GetAllPerformersAsync(Options options)
        {
            var data = new List<AuthorResponse>();
            try
            {
                Task.Run(async () =>
                {
                    var response = await Client.GetAsync($"api/v1/music/authors?publisher={options.Publisher}");
                    data = await response.Content.ReadAsAsync<List<AuthorResponse>>();
                }).Wait();
            }
            catch(Exception ex)
            {
                Log(ex.Message);
            }
            
            return data;
        }

        public ICollection<PublisherResponse> GetAllPublishersAsync()
        {
            var data = new List<PublisherResponse>();
            try
            {
                Task.Run(async () =>
                {
                    var response = await Client.GetAsync("api/v1/music/publishers");
                    data = await response.Content.ReadAsAsync<List<PublisherResponse>>();
                }).Wait();
            }
            catch(Exception ex)
            {
                Log(ex.Message);
            }
           
            return data;
        }
        public  ICollection<GenreResponse> GetAllGenresAsync()
        {
            var data = new List<GenreResponse>();
            try
            {
                Task.Run(async () =>
                {
                    var response = await Client.GetAsync("api/v1/music/genres");
                    data = await response.Content.ReadAsAsync<List<GenreResponse>>();
                }).Wait();
            }
            catch(Exception ex)
            {
                Log(ex.Message);
            }                       
            return data;
        }

        public bool TryLogin(string login, SecureString password)
        {
            var pass = ConvertToUnsecureString(password);
            var data = new ClientResponse();
            var response = new HttpResponseMessage();
            try
            {
                Task.Run(async () =>
                {
                    response = await Client.GetAsync($"api/v1/client/{login}?key={pass}");

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

            return response.IsSuccessStatusCode;
        }
        public bool TryRegister(string login, SecureString key, string name, string phone, string email = "")
        {
            var response = new HttpResponseMessage();

            var data = new ClientResponse()
            {
                Login = login,
                Password = ConvertToUnsecureString(key),
                Name = name,
                Phone = phone,
                Email = email
            };

            try
            {
                Task.Run(async () =>
                {
                    response = await Client.PostAsJsonAsync($"api/v1/client/new", data);

                }).Wait();
            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }

            return response.IsSuccessStatusCode;
        }

        public bool TryChangeProfile(string login, SecureString key, string name, string phone, string email)
        {
            var response = new HttpResponseMessage();
            var data = new ClientResponse()
            {
                Login = login,
                Password = ConvertToUnsecureString(key),
                Name = name,
                Phone = phone,
                Email = email,
                Identity = Options.MusicOptions.User.Identity
            };
            try
            {
                Task.Run(async () =>
                {
                    response = await Client.PutAsJsonAsync($"api/v1/client/{name}/Profile", data);
                    response.EnsureSuccessStatusCode();

                    Options.MusicOptions.User = await response.Content.ReadAsAsync<ClientResponse>();

                }).Wait();
                
            }
            catch(Exception ex)
            {
                Log(ex.Message);
            }
            return response.IsSuccessStatusCode;
        }

        public ICollection<SongResponse> GetHistoryAsync()
        {
            var data = new List<SongResponse>();
            try
            {
                Task.Run(async () =>
                {
                    var response = await Client.GetAsync($"api/v1/client/{Options.MusicOptions.User.Identity}/History");
                    data = await response.Content.ReadAsAsync<List<SongResponse>>();
                }).Wait();
            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }
            return data;
        }
        public void Play(SongResponse song)
        {
            Task.Run(() =>
            {
                    var url = $"http://localhost:5000/api/v1/music/stream?author={song.Author}&name={song.Name}";

                    using (var mf = new MediaFoundationReader(url))
                    {
                        WO.Init(mf);
                        WO.Play();

                        while (WO.PlaybackState == PlaybackState.Playing)
                        {
                            Thread.Sleep(1000);
                        }
                    }                            
            });
            
        }
        public void Stop()
        {
            if (WO.PlaybackState == PlaybackState.Playing)
                WO.Stop();
            WO.Dispose();
        }

        public bool TryCreateOrder()
        {
            var response = new HttpResponseMessage();
            try
            {
                Task.Run(async () =>
                {
                    response = await Client.PostAsJsonAsync($"api/v1/client/{Options.MusicOptions.User.Identity}/Order", 
                        Options.MusicOptions.Cart);
                });
            }
            catch(Exception ex)
            {
                Log(ex.Message);
            }

            return response.IsSuccessStatusCode;
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
