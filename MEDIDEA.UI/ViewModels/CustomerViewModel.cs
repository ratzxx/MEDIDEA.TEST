using System;
using System.Threading.Tasks;
using Autofac;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using MEDIDEA.UI.Common;
using MEDIDEA.UI.Visit;

namespace MEDIDEA.UI.ViewModels
{
    public interface ICustomerViewModel
    {
        void SetCustomer(Domain.Entities.Customer c);
        Domain.Entities.Customer Item { get; }
    }

    public class CustomerViewModel: EntityViewModel<Domain.Entities.Customer>, ICustomerViewModel
    {
        protected override async void OnParameterChanged(object parameter)
        {
            base.OnParameterChanged(parameter);
            if (parameter is long id)
            {
                SetLoading(true);
                var item = await Task.Run(() => _uoW.Customers.Get(id));
                SetItem(item);
                SetLoading(false);
            }
            else
            {
                SetItem(new Domain.Entities.Customer());
            }
        }

        [AsyncCommand]
        public async Task Save()
        {
            SetLoading(true);
            try
            {
                await Task.Run(() =>
                {
                    _uoW.Customers.Update(Item);
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

        [AsyncCommand]
        public async Task Delete()
        {
            SetLoading(true);
            try
            {
                Item.IsDeleted = true;
                await Task.Run(() =>
                {
                    _uoW.Customers.Update(Item);
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
        public async Task Create()
        {
            SetLoading(true);
            this.RaisePropertiesChanged();
            try
            {
                await Task.Run(() =>
                {
                    _uoW.Customers.Add(Item);
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

        public void SetCustomer(Domain.Entities.Customer c)
        {
            SetItem(c);
        }
    }
}
