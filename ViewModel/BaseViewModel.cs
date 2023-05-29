using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ServerDistances.ViewModel
{
    internal class BaseViewModel : INotifyPropertyChanged
    {
        protected virtual void SetProperty<T> (ref T member,  T value,
            [CallerMemberName] string propertyName = null)
        {
            member = value;
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler? PropertyChanged = delegate { };

        protected static readonly log4net.ILog log = log4net.LogManager
            .GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    }
}
