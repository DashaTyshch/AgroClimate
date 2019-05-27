using Dima.Managers;
using Dima.Models;
using Dima.Tools;
using Dima.Windows;
using System.Windows;

namespace Dima
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ContentWindow contentWindow = new ContentWindow();
            NavigationModel navigationModel = new NavigationModel(contentWindow);
            NavigationManager.Instance.Initialize(navigationModel);
            contentWindow.Show();

            NavigationManager.Instance.Navigate(ModesEnum.Login);
        }
    }
}
