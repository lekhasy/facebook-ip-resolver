using System.Windows.Controls;
using System.Windows.Input;

namespace ModernUINavigationApp1.Pages.Settings
{
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About : UserControl
    {
        public About()
        {
            InitializeComponent();
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.facebook.com/le.khasy");
        }
    }
}
