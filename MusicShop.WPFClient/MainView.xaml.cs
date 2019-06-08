
using System.Windows;
using System.Windows.Controls;

namespace MusicShop.WPFClient
{

    public partial class MainView : Window, IWindow
    {
        public MainView()
        {
            InitializeComponent();
            ListViewMenu.SelectionChanged += ListViewMenu_SelectionChanged;
        }

#region IWindow реализация
        public void CloseApp()
        {
            Application.Current.Shutdown();
        }

        public void CloseWindow()
        {
            Application.Current.Shutdown();

        }

        public void MoveWindow()
        {
            DragMove();
        }

        public void ShowWindow()
        {
            throw new System.NotImplementedException();
        }
#endregion

        #region перемещение полоски по списку
        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ListViewMenu.SelectedIndex;
            MoveCursorMenu(index);           
        }

        private void MoveCursorMenu(int index)
        {
            TrainsitionigContentSlide.OnApplyTemplate();
            GridCursor.Margin = new Thickness(0, (60 * index), 0, 0);
        }

        #endregion
    }
}
