

namespace MusicShop.WPFClient.Services
{
   public class WindowFactory
    {
        public WindowFactory()  {   }
        public IWindow CreateWindow(string NameofTypeWindow)
        {
            //тип моя простая фабрика окон..
            //контекст наверное не буду прописывать, ибо каждое окно знает о своем через xaml
            if (NameofTypeWindow == "LoginView")
            {
                return  new LoginView();
            }
            return null;
        }
    }
}
