using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Presentation.ViewModel;

namespace ConcurrentProgramming
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // if (DataContext is ViewModel viewModel)
            // {
            //     
            // }
        }
        
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // if (DataContext is ViewModel viewModel)
            // {
            //     
            // }
        }
    }
}