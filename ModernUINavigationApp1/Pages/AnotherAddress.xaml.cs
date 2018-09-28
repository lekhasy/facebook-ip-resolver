using System;
using System.Windows;
using System.Windows.Controls;
using Codes;
using System.Net;

namespace ModernUINavigationApp1.Pages
{
    /// <summary>
    /// Interaction logic for BasicPage1.xaml
    /// </summary>
    public partial class BasicPage1 : UserControl
    {
        public BasicPage1()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            button.IsEnabled = false;
            textboxresult.Text = string.Empty;
            string str = textboxaddress.Text;
            new System.Threading.Thread(new System.Threading.ThreadStart(delegate ()
            {
                try {
                    another(str);
                    Dispatcher.BeginInvoke(new Action(delegate () {
                        button.IsEnabled = true;
                    }));
                }
                catch
                {
                    MessageBox.Show("an error occured");
                }
            })).Start();
        }

        public void another(string address)
        {

            string buff = string.Empty;
            HightLevelNetworkControll.setGoogleDNS();
            try
            {
                // phân giải được thì chép nó vào data
                buff = Dns.GetHostAddresses(address)[0].ToString() + " " + address;
                Dispatcher.BeginInvoke(new Action(delegate () { textboxresult.Text = buff; }));
            }
            catch
            {// không phân giải được thì báo là ko phân giải được
                Dispatcher.BeginInvoke(new Action(delegate () { textboxresult.Text = "Cant' resolve this host"; }));
            }
            HightLevelNetworkControll.RestoreDNS();
        }
    }
}
