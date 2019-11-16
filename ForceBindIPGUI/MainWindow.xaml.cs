using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace ForceBindIPGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Models.Profiles currentProfile;
        private List<Models.Profiles> ProfileList;

        public MainWindow()
        {
            InitializeComponent();

            currentProfile = new Models.Profiles();
            this.DataContext = currentProfile;

            IPListRefresh();

            ProfileList = new List<Models.Profiles>();
            DummyProfileList();
            dgProfileList.ItemsSource = ProfileList;

        }

        private void DummyProfileList()
        {
            ProfileList.Add(new Models.Profiles(Guid.NewGuid(),"Proximus","192.168.1.100", @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe", false));
        }

        private void IPListRefresh()
        {
            cbIPs.ItemsSource = Models.NetworkInterfaces.GetLocalIPv4();
        }

        private void FileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true) currentProfile.Path = openFileDialog.FileName;
        }

        #region Events

        private void btIPListRefresh_Click(object sender, RoutedEventArgs e)
        {
            IPListRefresh();
        }

        private void btLaunch_Click(object sender, RoutedEventArgs e)
        {
            Models.ForceBindIPConnector.Launch(currentProfile);
        }

        private void btFiledialog_Click(object sender, RoutedEventArgs e)
        {
            FileDialog();
        }

        #endregion

        
    }
}
