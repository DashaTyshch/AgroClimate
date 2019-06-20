using Dima.ViewModels;
using System.Windows.Controls;

namespace Dima.Views
{
    /// <summary>
    /// Логика взаимодействия для AddEngineerView.xaml
    /// </summary>
    public partial class AddEngineerView : UserControl
    {

        public AddEngineerView()
        {
            InitializeComponent();

            DataContext = new AddEngineerViewModel();
        }
    }
}
