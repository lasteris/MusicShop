
using System.Security;
using System.Windows;

namespace MusicShop.WPFClient
{
    public partial class LoginView : Window, IHavePassword, IWindow
    {
        public LoginView()
        {
            InitializeComponent();
        }

        public SecureString Password => UserPass.SecurePassword;

        public void CloseApp()
        {
            throw new System.NotImplementedException();
        }

        public void CloseWindow()
        {
            Close();
        }

        public void MoveWindow()
        {
            DragMove();
        }

        public void ShowWindow()
        {
            ShowDialog();
        }
    }
}
