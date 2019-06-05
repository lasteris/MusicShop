
using System.Windows;
using System.Windows.Controls;
using MusicShop.WPFClient.Views;

namespace MusicShop.WPFClient
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //как-то не забыть
           // ListViewMenu.SelectionChanged += ListViewMenu_SelectionChanged;
            WindowClose.Click += WindowClose_Click;
            AuthButton.Click += AuthButton_Click;
        }

        private void AuthButton_Click(object sender, RoutedEventArgs e)
        {
            new Auth().ShowDialog();
        }

        #region Закрытие приложения + движение окна

        private void WindowClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Grid_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        #endregion


        #region перемещение по списку => Обновление содержимого главного Грида (ContentArea)
        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           // int index = ListViewMenu.SelectedIndex;
          //  MoveCursorMenu(index);

            #region 1
            //switch (index)
            //{
            //    case 0:
            //        ContentArea.Children.Clear();
            //        ContentArea.Children.Add(new UserControlFirstPage());
            //        break;
            //    case 1:
            //        ContentArea.Children.Clear();
            //        ContentArea.Children.Add(new UserControlPublishers());
            //        break;
            //    case 2:
            //        ContentArea.Children.Clear();
            //        ContentArea.Children.Add(new UserControlPerformers());
            //        break;
            //    case 3:
            //        ContentArea.Children.Clear();
            //        ContentArea.Children.Add(new UserControlAllMusic());
            //        break;
            //    case 4:
            //        ContentArea.Children.Clear();
            //        ContentArea.Children.Add(new UserControlCart());
            //        break;
            //    case 5:
            //        ContentArea.Children.Clear();
            //        ContentArea.Children.Add(new UserControlProfile());
            //        break;
            //    default:
            //        break;
            //}
            #endregion
        }

        private void MoveCursorMenu(int index)
        {
            TrainsitionigContentSlide.OnApplyTemplate();
            GridCursor.Margin = new Thickness(0, (60 * index), 0, 0);
        }

        #endregion
    }
}
