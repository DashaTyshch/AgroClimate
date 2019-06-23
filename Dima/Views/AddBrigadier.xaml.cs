using System.Windows.Controls;
using Dima.ViewModels;

namespace Dima.Views
{
    /// <summary>
    /// Interaction logic for AddBrigadier.xaml
    /// </summary>
    public partial class AddBrigadier : UserControl
    {

        public AddBrigadier()
        {
            InitializeComponent();

            DataContext = new AddBrigadierViewModel();
        }

      
    }
}
