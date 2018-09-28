using System;
using System.Windows;
using System.Windows.Controls;
using System.Threading;
using Codes;

namespace ModernUINavigationApp1.Pages
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : UserControl
    {
        
        public Home()
        {
            InitializeComponent();
            
        }

        private void HightLevelNetworkControll_WriteThing(object sender, EventArgs e)
        {
            setStatus(sender.ToString());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            HightLevelNetworkControll.WriteThing += HightLevelNetworkControll_WriteThing;
            button.IsEnabled = false;
            progressbar.Visibility = Visibility.Visible;
            Listbox.Items.Clear();

            new Thread(new ThreadStart(delegate ()
            {
                try
                {
                    OneClick();
                    setStatus("Resolve operation successfull");
                }
                catch { setStatus("Operation fail, check the log below to find the problem!");
                    HightLevelNetworkControll.RestoreDNS();
                }
                Dispatcher.BeginInvoke(new Action(delegate ()
                {
                    progressbar.Visibility = Visibility.Hidden;
                    button.IsEnabled = true;
                }));
                HightLevelNetworkControll.WriteThing -= HightLevelNetworkControll_WriteThing;
            })).Start();
            
        }

        public void OneClick()
        {
            setStatus("Set default DNS to google's DNS...");
            HightLevelNetworkControll.setGoogleDNS();

            setStatus("Getting hosts file data...");
            string WriteData = HostFile.ReadDataWithoughtanstring("facebook") + Environment.NewLine;

            setStatus("Resolving IP Address...");
            WriteData += HightLevelNetworkControll.ResolvedData();

            setStatus("Restoring default DNS...");
            HightLevelNetworkControll.RestoreDNS();

            setStatus("Writing data to hosts file...");
            HostFile.WriteHostData(WriteData);
        }

        void setStatus(string status)
        {
            this.Dispatcher.BeginInvoke(new Action(delegate ()
            {
                Listbox.Items.Add(status);
            }));
        }
    }
}
