using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ViewModels
{
    [Serializable]
    public class Log : INotifyPropertyChanged
    {
        private string _description;
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        private DateTime _time;


        public DateTime Time
        {
            get { return _time; }
            set { SetProperty(ref _time, value); }
        }

        public Log(string description, DateTime time)
        {
            Description = description;
            Time = time;
        }

        public override string ToString() => $"On {Time} : {Description} ";


        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        protected void SetProperty<T>(ref T oldValue, T newValue, [CallerMemberName]string propertyName = null)
        {
            if (!oldValue?.Equals(newValue) ?? newValue != null)
            {
                oldValue = newValue;

                OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

    }

}
