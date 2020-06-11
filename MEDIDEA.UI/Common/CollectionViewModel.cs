using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm.DataAnnotations;

namespace MEDIDEA.UI.Common
{
    public class CollectionViewModel<T> : BaseViewModel
    {
        public ObservableCollection<T> Items
        {
            get { return GetProperty(() => Items); }
            private set { SetProperty(() => Items, value); }
        }

        public T SelectedItem {
            get { return GetProperty(() => SelectedItem); }
            set { SetProperty(() => SelectedItem, value, OnSelectedItemChanged); }
        }

        protected virtual void OnSelectedItemChanged()
        {
            
        }

        public CollectionViewModel()
        {
            Items = new ObservableCollection<T>();
        }

        public void SetItems(IEnumerable<T> items)
        {
            Items = new ObservableCollection<T>(items);
        }
    }
}
