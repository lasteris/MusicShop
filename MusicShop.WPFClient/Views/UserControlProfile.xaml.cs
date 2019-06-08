
using System.Security;
using System.Windows.Controls;

namespace MusicShop.WPFClient.Views
{
    public partial class UserControlProfile : UserControl, IHavePassword
    {
        public UserControlProfile()
        {
            InitializeComponent();
        }

        public SecureString Password => UserPass.SecurePassword;
    }
}
