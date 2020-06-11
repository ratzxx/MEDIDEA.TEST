using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Autofac;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using MEDIDEA.Domain;
using MEDIDEA.UI.Common;

namespace MEDIDEA.UI.ViewModels
{
    public class CustomerSelectViewModel : CollectionViewModel<Domain.Entities.Customer>
    {
        private IVisitViewModel _parentViewModel;

        IDialogService DialogService { get { return GetService<IDialogService>(); } }

        private ICustomerRepository _customerRepository;

        public string Name
        {
            get { return GetProperty(() => Name); }
            set { SetProperty(() => Name, value); }
        }

        public CustomerSelectViewModel()
        {
            _customerRepository = Bootstrapper.Container.Resolve<ICustomerRepository>();
        }

        protected override void OnParentViewModelChanged(object parentViewModel)
        {
            base.OnParentViewModelChanged(parentViewModel);
            _parentViewModel = parentViewModel as IVisitViewModel;
        }

        [Command]
        public async void OnLoaded()
        {
            var customers = await Task.Run(() => _customerRepository.GetAll(Filter()));
            SetItems(customers);
        }

        private Expression<Func<Domain.Entities.Customer, bool>> Filter()
        {
            if (string.IsNullOrEmpty(Name))
            {
                return i => true;
            }

            return i => i.Name.Contains(Name);
        }

        [Command]
        public void ShowCustomerForm(DevExpress.Xpf.Editors.ProcessNewValueEventArgs e)
        {

            Domain.Entities.Customer customer = new Domain.Entities.Customer()
            {
                Name = e.DisplayText
            };
            var addCustomerCommand = new UICommand()
            {
                Caption = "Add",
                IsCancel = false,
                IsDefault = true
            };

            var cancelCustomerCommand = new UICommand()
            {
                Caption = "Cancel",
                IsDefault = false,
                IsCancel = true
            };

            var vm = Bootstrapper.Container.Resolve<ICustomerViewModel>();
            vm.SetCustomer(customer);
            vm.SetParentViewModel(this);

            var result = DialogService.ShowDialog(
                dialogCommands: new List<UICommand>() { addCustomerCommand, cancelCustomerCommand },
                title: "Add new customer",
                viewModel: vm);

            if (result == addCustomerCommand)
            {
                Items.Add(customer);
                SelectedItem = customer;
                _parentViewModel.SetCustomer(customer);
            }
        }

        public void SetSelected(long customerId)
        {
            SelectedItem = Items.FirstOrDefault(i => i.Id == customerId);
        }

        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
            if (_parentViewModel == null) return;
            if (SelectedItem != null && _parentViewModel.Item.CustomerId != SelectedItem.Id)
            {
                _parentViewModel.SetCustomerId(SelectedItem.Id);
            }
        }
    }
}
