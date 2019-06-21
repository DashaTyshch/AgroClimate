using Dima.ViewModels;
using System.Windows.Controls;

namespace Dima.Views
{
    /// <summary>
    /// Логика взаимодействия для RequestView.xaml
    /// </summary>
    public partial class RequestView : UserControl
    {
        public RequestView()
        {
            InitializeComponent();

            DataContext = new RequestViewModel();
        }
    }
}
