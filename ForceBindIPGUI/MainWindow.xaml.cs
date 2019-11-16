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
using System.Diagnostics;
using System.IO;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Win32;

namespace ForceBindIPGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private string _softwarePath;
        public string softwarePath
        {
            get => _softwarePath;
            set => SetField(ref _softwarePath, value);
        }

        private string _ip;
        public string ip
        {
            get => _ip;
            set => SetField(ref _ip, value);
        }


        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = this;

            IPListRefresh();
            //softwarePath = @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe";
        }

        public void launchSoftware()
        {
            PeHeaderReader phr = new PeHeaderReader(softwarePath);

            ProcessStartInfo startInfo = new ProcessStartInfo((phr.Is32BitHeader)?"ForceBindIP.exe": "ForceBindIP64.exe");
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.ArgumentList.Add(ip);
            startInfo.ArgumentList.Add(convertProgFilePath(softwarePath));

            Process process = new Process();
            process.StartInfo = startInfo;
            process.Start();
        }

        private string convertProgFilePath(string path)
        {
            if (path.Contains("Program Files (x86)")) return path.Replace("Program Files (x86)", "PROGRA~2");
            if (path.Contains("Program Files")) return path.Replace("Program Files", "PROGRA~1");
            return path;
        }

        public List<string> GetLocalIPv4()
        {
            List<string> IPList = new List<string>();
            foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (item.OperationalStatus == OperationalStatus.Up)
                {
                    foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            IPList.Add(ip.Address.ToString());
                        }
                    }
                }
            }
            return IPList;
        }

        private void IPListRefresh()
        {
            cbIPs.ItemsSource = GetLocalIPv4();
        }

        private void FileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true) softwarePath = openFileDialog.FileName;
        }

        #region Events

        private void btIPListRefresh_Click(object sender, RoutedEventArgs e)
        {
            IPListRefresh();
        }

        private void btLaunch_Click(object sender, RoutedEventArgs e)
        {
            launchSoftware();
        }

        private void btFiledialog_Click(object sender, RoutedEventArgs e)
        {
            FileDialog();
        }

        #endregion


        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }


        #endregion

        
    }
}
