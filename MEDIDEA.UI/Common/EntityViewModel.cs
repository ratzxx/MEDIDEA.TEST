using Autofac;
using MEDIDEA.Domain;

namespace MEDIDEA.UI.Common
{
    public class EntityViewModel<T> : BaseViewModel
    {
        protected IUoW _uoW;
        public T Item
        {
            get { return GetProperty(() => Item); }
            private set { SetProperty(() => Item, value); }
        }
        public EntityViewModel()
        {
            _uoW = Bootstrapper.Container.Resolve<IUoW>();
        }

        protected virtual void SetItem(T item)
        {
            Item = item;
        }
    }
}
