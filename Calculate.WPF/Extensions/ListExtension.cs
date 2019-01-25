using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Calculate.WPF.Extensions
{
    public static class ListExtension
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> coll)
        {
            return new ObservableCollection<T>(coll);
        }
    }
}