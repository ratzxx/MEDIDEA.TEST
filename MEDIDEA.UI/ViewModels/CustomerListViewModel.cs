using System;
using System.Threading.Tasks;
using Autofac;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using MEDIDEA.Domain;
using MEDIDEA.UI.Common;

namespace MEDIDEA.UI.ViewModels
{
    public class CustomerListViewModel : CollectionViewModel<Domain.Entities.Customer>
    {
        private ICustomerRepository _customerRepository;
        public CustomerListViewModel()
        {
            _customerRepository = Bootstrapper.Container.Resolve<ICustomerRepository>();
        }

        [AsyncCommand]
        public async Task OnLoaded()
        {
            SetLoading(true);
            try
            {
                var customers = await Task.Run(async () =>
                {
#if DEBUG
                    await Task.Delay(1000);
#endif
                    return _customerRepository.GetAll(i => !i.IsDeleted);
                });
                SetItems(customers);
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

        [Command]
        public void ItemDoubleClick()
        {
            Navigation.Navigate("CustomerCardView", SelectedItem.Id, this);
        }

        public bool CanItemDoubleClick()
        {
            return SelectedItem != null;
        }

        [Command]
        public void Create()
        {
            Navigation.Navigate("CustomerCardView", null, this);
        }
    }
}
