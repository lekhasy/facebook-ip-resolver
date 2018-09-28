using FirstFloor.ModernUI.Windows.Controls;
using System.Windows;

namespace ModernUINavigationApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ModernWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ModernWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //new ResourceDictionary { Source = new Uri("/SearchIPfacebook;component/Pages/Settings/Snowflakes.xaml", UriKind.Relative) };
        }
    }
}
