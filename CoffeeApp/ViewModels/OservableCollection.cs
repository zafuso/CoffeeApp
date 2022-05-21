using System;
using System.Collections.ObjectModel;

namespace CoffeeApp.ViewModels
{
    internal class OservableCollection<T>
    {
        public static implicit operator OservableCollection<T>(ObservableCollection<string> v)
        {
            throw new NotImplementedException();
        }
    }
}