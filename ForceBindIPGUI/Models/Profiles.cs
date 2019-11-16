using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace ForceBindIPGUI.Models
{
    public class Profiles : INotifyPropertyChanged
    {
        private Guid _ID;
        public Guid ID
        {
            get => _ID;
            set => SetField(ref _ID, value);
        }
        private string _Name;
        public string Name
        {
            get => _Name;
            set => SetField(ref _Name, value);
        }
        private string _IP;
        public string IP
        {
            get => _IP;
            set => SetField(ref _IP, value);
        }
        private string _Path;
        public string Path
        {
            get => _Path;
            set => SetField(ref _Path, value);
        }
        private bool _InjectionDelay;
        public bool InjectionDelay
        {
            get => _InjectionDelay;
            set => SetField(ref _InjectionDelay, value);
        }

        public Profiles()
        {

        }

        public Profiles(Guid ID, string Name, string IP, string Path, bool InjectionDelay)
        {
            this.ID = ID;
            this.Name = Name;
            this.IP = IP;
            this.Path = Path;
            this.InjectionDelay = InjectionDelay;
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
