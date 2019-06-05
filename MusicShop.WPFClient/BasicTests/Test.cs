
using System;
using System.Collections.ObjectModel;
using MusicShop.WPFClient.Models;


namespace MusicShop.WPFClient
{
    public static class Test
    {
        #region тестовый набор музыки
        public static ObservableCollection<SongResponse> GetMusics()
        {
            return new ObservableCollection<SongResponse>()
            {
                new SongResponse
                {
                    Image = @"..\..\Assets\Venom.jpg",
                    Author = "BFMV",
                    Album = "Venom",
                    Name = "Do you want a battle ?",
                    DateRelease = DateTime.Now
                },
                new SongResponse
                {
                    Image = @"..\..\Assets\Venom.jpg",
                    Author = "BFMV",
                    Album = "Venom",
                    Name = "Do you want a battle ?",
                    DateRelease = DateTime.Now
                },
                new SongResponse
                {
                    Image = @"..\..\Assets\Venom.jpg",
                    Author = "BFMV",
                    Album = "Venom",
                    Name = "Do you want a battle ?",
                    DateRelease = DateTime.Now
                },
                new SongResponse
                {
                    Image = @"..\..\Assets\Venom.jpg",
                    Author = "BFMV",
                    Album = "Venom",
                    Name = "Do you want a battle ?",
                    DateRelease = DateTime.Now
                },
                new SongResponse
                {
                    Image = @"..\..\Assets\Venom.jpg",
                    Author = "BFMV",
                    Album = "Venom",
                    Name = "Do you want a battle ?",
                    DateRelease = DateTime.Now
                },
                new SongResponse
                {
                    Image = @"..\..\Assets\Venom.jpg",
                    Author = "BFMV",
                    Album = "Venom",
                    Name = "Do you want a battle ?",
                    DateRelease = DateTime.Now
                },
                new SongResponse
                {
                    Image = @"..\..\Assets\Venom.jpg",
                    Author = "BFMV",
                    Album = "Venom",
                    Name = "Do you want a battle ?",
                    DateRelease = DateTime.Now
                }
            };
        }
        #endregion

        #region тестовый набор исполнителей
        public static ObservableCollection<AuthorResponse> GetPerformers()
        {
            return new ObservableCollection<AuthorResponse>
            {
                new AuthorResponse
                {
                    Image = @"..\..\Assets\Venom.jpg",
                    Name = "BFMV",
                    MusicCount = 200
                },
                new AuthorResponse
                {
                    Image = @"..\..\Assets\Venom.jpg",
                    Name = "BFMV",
                    MusicCount = 200
                },
                new AuthorResponse
                {
                    Image = @"..\..\Assets\Venom.jpg",
                    Name = "BFMV",
                    MusicCount = 200
                },
                new AuthorResponse
                {
                    Image = @"..\..\Assets\Venom.jpg",
                    Name = "BFMV",
                    MusicCount = 200
                },
                new AuthorResponse
                {
                    Image = @"..\..\Assets\Venom.jpg",
                    Name = "BFMV",
                    MusicCount = 200
                },
                new AuthorResponse
                {
                    Image = @"..\..\Assets\Venom.jpg",
                    Name = "BFMV",
                    MusicCount = 200
                },
                new AuthorResponse
                {
                    Image = @"..\..\Assets\Venom.jpg",
                    Name = "BFMV",
                    MusicCount = 200
                },

            };
        }
        #endregion

        #region Тестовый набор лейблов
        public static ObservableCollection<PublisherResponse> GetProducers()
        {
            return new ObservableCollection<PublisherResponse>
            {
                new PublisherResponse
                {
                    Image = @"..\..\Assets\Epitaph.jpg",
                    Name = "Epitaph Records",
                    Description = "Американский лейбл звукозаписи, основанный в 1980 году гитаристом группы Bad Religion Бретом Гуревичем (англ. Brett Gurewitz)",
                    PerformerCount = 30
                },
                new PublisherResponse
                {
                    Image = @"..\..\Assets\Epitaph.jpg",
                    Name = "Epitaph Records",
                    Description = "Американский лейбл звукозаписи, основанный в 1980 году гитаристом группы Bad Religion Бретом Гуревичем (англ. Brett Gurewitz)",
                    PerformerCount = 30
                },
                new PublisherResponse
                {
                    Image = @"..\..\Assets\Epitaph.jpg",
                    Name = "Epitaph Records",
                    Description = "Американский лейбл звукозаписи, основанный в 1980 году гитаристом группы Bad Religion Бретом Гуревичем (англ. Brett Gurewitz)",
                    PerformerCount = 30
                },
                new PublisherResponse
                {
                    Image = @"..\..\Assets\Epitaph.jpg",
                    Name = "Epitaph Records",
                    Description = "Американский лейбл звукозаписи, основанный в 1980 году гитаристом группы Bad Religion Бретом Гуревичем (англ. Brett Gurewitz)",
                    PerformerCount = 30
                },
                new PublisherResponse
                {
                    Image = @"..\..\Assets\Epitaph.jpg",
                    Name = "Epitaph Records",
                    Description = "Американский лейбл звукозаписи, основанный в 1980 году гитаристом группы Bad Religion Бретом Гуревичем (англ. Brett Gurewitz)",
                    PerformerCount = 30
                },
                new PublisherResponse
                {
                    Image = @"..\..\Assets\Epitaph.jpg",
                    Name = "Epitaph Records",
                    Description = "Американский лейбл звукозаписи, основанный в 1980 году гитаристом группы Bad Religion Бретом Гуревичем (англ. Brett Gurewitz)",
                    PerformerCount = 30
                },
                new PublisherResponse
                {
                    Image = @"..\..\Assets\Epitaph.jpg",
                    Name = "Epitaph Records",
                    Description = "Американский лейбл звукозаписи, основанный в 1980 году гитаристом группы Bad Religion Бретом Гуревичем (англ. Brett Gurewitz)",
                    PerformerCount = 30
                }

            };
        }
        #endregion

        #region Тестовый набор жанров
        public static ObservableCollection<GenreResponse> GetGenreResponses()
        {
            return new ObservableCollection<GenreResponse>
            {
                new GenreResponse
                {
                    Name = "METAL"
                },
                new GenreResponse
                {
                    Name = "POP"
                },
                new GenreResponse
                {
                    Name = "RapCore"
                },
                new GenreResponse
                {
                    Name = "PostHardCore"
                },
                new GenreResponse
                {
                    Name = "TRAP"
                },
                new GenreResponse
                {
                    Name = "J-POP"
                }
            };
        }
        #endregion
    }
}
