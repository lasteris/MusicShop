
using MusicShop.WPFClient.Models;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;


namespace MusicShop.WPFClient.Views
{

    public partial class UserControlPerformers : UserControl
    {
       // public Extension _extension { get; set; } = new Extension();
       // public ObservableCollection<AuthorResponse> AuthorResponses { get; set; } = new ObservableCollection<AuthorResponse>();
        public UserControlPerformers()
        {
            InitializeComponent();
            Loaded += UserControlPerformers_Loaded;
        }

        private void UserControlPerformers_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            //AuthorResponses = new ObservableCollection<AuthorResponse>(await _extension.GetAllPerformers());
            //ListViewPerformers.ItemsSource = AuthorResponses;
        }
    }

   
}
