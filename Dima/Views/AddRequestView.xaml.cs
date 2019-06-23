using Dima.ViewModels;
using System.Windows.Controls;

namespace Dima.Views
{
    /// <summary>
    /// Логика взаимодействия для AddRequestView.xaml
    /// </summary>
    public partial class AddRequestView : UserControl
    {
        public AddRequestView()
        {
            InitializeComponent();

            DataContext = new AddRequestViewModel();
        }
    }
}
