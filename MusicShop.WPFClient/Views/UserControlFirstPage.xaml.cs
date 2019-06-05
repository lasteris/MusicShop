
using MusicShop.WPFClient.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace MusicShop.WPFClient.Views
{
    public partial class UserControlFirstPage : UserControl
    {
      //  ObservableCollection<GenreResponse> _genres { get; set; }
      //  Extension _extension { get; set; }  = new Extension();
        public UserControlFirstPage()
        {
            InitializeComponent();
            Loaded += UserControlFirstPage_Loaded;
        }

        private void UserControlFirstPage_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            //_genres = new ObservableCollection<GenreResponse>( await _extension.GetAllGenres());

            //ListViewGenres.ItemsSource = _genres;
        }
    }
}
