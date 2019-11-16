using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace ForceBindIPGUI.Models
{
    public class NetworkInterfaces
    {
        private Guid _ID;
        public Guid ID
        {
            get => _ID;
            set => SetField(ref _ID, value);
        }
        private IPAddress _IPv4;
        public IPAddress IPv4
        {
            get => _IPv4;
            set => SetField(ref _IPv4, value);
        }
        private IPAddress _IPV6;
        public IPAddress IPV6
        {
            get => _IPV6;
            set => SetField(ref _IPV6, value);
        }
        private string _Name;
        public string Name
        {
            get => _Name;
            set => SetField(ref _Name, value);
        }
        private string _Description;
        public string Description
        {
            get => _Description;
            set => SetField(ref _Description, value);
        }

        public static List<string> GetLocalIPv4()
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
