using System.Collections.ObjectModel;

namespace Services.Extensions
{
    public static class ObservableCollectionExt
    {
        public static void AddRange<T>(this ObservableCollection<T> items, IList<T> newItems)
        {
            for (int i = 0; i < newItems.Count; i++)
            {
                items.Add(newItems[i]);
            }
        }
    }
}
