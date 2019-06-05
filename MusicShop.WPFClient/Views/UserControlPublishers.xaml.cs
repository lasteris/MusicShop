using MusicShop.WPFClient.Models;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace MusicShop.WPFClient.Views
{

    public partial class UserControlPublishers : UserControl
    {
        //public Extension _extension { get; set; } = new Extension();
        //public ObservableCollection<PublisherResponse> PublisherResponses { get; set; } = new ObservableCollection<PublisherResponse>();
        public UserControlPublishers()
        {
            InitializeComponent();
            //ListViewPublishers.ItemsSource = Test.GetProducers();

            Loaded += UserControlPublishers_Loaded;
        }

        private void UserControlPublishers_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            //PublisherResponses = new ObservableCollection<PublisherResponse>(await _extension.GetAllPublishers());

            //ListViewPublishers.ItemsSource = PublisherResponses;
        }
    }
}
