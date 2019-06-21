using Dima.ViewModels;
using System.Windows.Controls;

namespace Dima.Views
{
    /// <summary>
    /// Логика взаимодействия для EngineerView.xaml
    /// </summary>
    public partial class EngineerView : UserControl
    {
        public EngineerView()
        {
            InitializeComponent();
            DataContext = new EngineerViewModel();
        }
    }
}
