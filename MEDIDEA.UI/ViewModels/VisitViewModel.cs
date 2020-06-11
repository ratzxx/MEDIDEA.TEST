using System;
using System.Threading.Tasks;
using Autofac;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using MEDIDEA.UI.Common;

namespace MEDIDEA.UI.ViewModels
{
    public interface IVisitViewModel
    {
        void SetCustomerId(long customerId);
        void SetCustomer(Domain.Entities.Customer customer);
        Domain.Entities.Visit Item { get; }
    }

    public class VisitViewModel : EntityViewModel<Domain.Entities.Visit>, IVisitViewModel
    {
        private long _customerId;

        public CustomerSelectViewModel CustomerSelectViewModel { get; }

        public VisitViewModel()
        {
            CustomerSelectViewModel = Bootstrapper.Container.Resolve<CustomerSelectViewModel>();
        }

        protected override async void OnParameterChanged(object parameter)
        {
            base.OnParameterChanged(parameter);
            if (parameter is long id)
            {
                SetLoading(true);
                var item = await Task.Run(() => _uoW.Visits.Get(id));
                SetItem(item);
                SetLoading(false);
            }
            else
            {
                SetItem(new Domain.Entities.Visit());
                if(_customerId > 0)
                {
                    Item.CustomerId = _customerId;
                    CustomerSelectViewModel.SetSelected(_customerId);
                }
            }
        }

        protected override void SetItem(Domain.Entities.Visit item)
        {
            base.SetItem(item);
            CustomerSelectViewModel.SetSelected(item.CustomerId);
        }

        [AsyncCommand]
        public async Task Create()
        {
            SetLoading(true);
            try
            {
                await Task.Run(() =>
                {
                    _uoW.Visits.Add(Item);
                    _uoW.Commit();
                });
                ShowNotification("Created!");
                Navigation.GoBack();
            }
            catch (Exception e)
            {
                MessageBoxService.ShowMessage(e.Message, "Error", MessageButton.OK);
            }
            finally
            {
                SetLoading(false);
            }
        }

        public bool CanCreate()
        {
            return Item?.Id == 0;
        }

        [AsyncCommand]
        public async Task Delete()
        {
            SetLoading(true);
            try
            {
                Item.IsDeleted = true;
                await Task.Run(() =>
                {
                    _uoW.Visits.Update(Item);
                    _uoW.Commit();
                });
                ShowNotification("Deleted!");
                Navigation.GoBack();
            }
            catch (Exception e)
            {
                MessageBoxService.ShowMessage(e.Message, "Error", MessageButton.OK);
            }
            finally
            {
                SetLoading(false);
            }
        }

        public bool CanDelete()
        {
            return Item?.Id > 0;
        }

        [AsyncCommand]
        public async Task Save()
        {
            SetLoading(true);
            try
            {
                await Task.Run(() =>
                {
                    _uoW.Visits.Update(Item);
                    _uoW.Commit();
                });
                ShowNotification("Saved!");
            }
            catch (Exception e)
            {
                MessageBoxService.ShowMessage(e.Message, "Error", MessageButton.OK);
            }
            finally
            {
                SetLoading(false);
            }
        }

        public bool CanSave()
        {
            return Item?.Id > 0;
        }

        public void SetCustomerId(long customerId)
        {
            _customerId = customerId;
            if (Item != null)
            {
                Item.CustomerId = customerId;
            }
        }
        public void SetCustomer(Domain.Entities.Customer customer)
        {
            SetCustomerId(customer.Id);
            if (Item != null)
            {
                Item.Customer = customer;
            }
        }
    }

}
