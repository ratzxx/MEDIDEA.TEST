using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using MEDIDEA.Domain;
using MEDIDEA.Domain.Entities;
using MEDIDEA.UI.Common;

namespace MEDIDEA.UI.ViewModels
{
    public class PhoneListViewModel : CollectionViewModel<Phone>
    {
        private long _customerId;
        private IUoW _uoW;
        IDialogService DialogService { get { return GetService<IDialogService>(); } }
        private ICustomerViewModel _parentViewModel;

        public PhoneListViewModel()
        {
            _uoW = Bootstrapper.Container.Resolve<IUoW>();
        }

        protected override void OnParentViewModelChanged(object vm)
        {
            _parentViewModel = vm as ICustomerViewModel;
        }

        protected override async void OnParameterChanged(object parameter)
        {
            if (parameter is long id)
            {
                _customerId = id;
                await LoadPhones();
            }

        }

        private async Task LoadPhones()
        {
            SetLoading(true);
            try
            {
                var items = await Task.Run(() => _uoW.Phones.GetAll(i => i.CustomerId == _customerId));
                SetItems(items);
            }
            catch (Exception e)
            {
                MessageBoxService.Show(e.Message, "Error");
            }
            finally
            {
                SetLoading(false);
            }
        }

        [Command]
        public void ItemDoubleCkick()
        {
            MessageBoxService.ShowMessage("Edit/delete coming soon...");
        }

        [AsyncCommand]
        public async Task AddPhone()
        {
            Phone phone = new Phone()
            {
                Number = "",
                Type = PhoneType.Business,
                CustomerId = _customerId
            };

            var addPhoneCommand = new UICommand()
            {
                Caption = "Add",
                IsCancel = false,
                IsDefault = true
            };

            var cancelPhoneCommand = new UICommand()
            {
                Caption = "Cancel",
                IsDefault = false,
                IsCancel = true
            };

            var vm = Bootstrapper.Container.Resolve<PhoneViewModel>();
            vm.SetPhone(phone);
            vm.SetParentViewModel(this);

            var result = DialogService.ShowDialog(
                dialogCommands: new List<UICommand>() { addPhoneCommand, cancelPhoneCommand },
                title: "Add new phone number",
                viewModel: vm);

            if (result == addPhoneCommand)
            {
                
                if (ExistCustomer())
                {
                    await SavePhoneToDatabase(phone);
                    await LoadPhones();
                }
                else
                {
                    Items.Add(phone);
                    if (_parentViewModel.Item.Phones == null)
                    {
                        _parentViewModel.Item.Phones = new List<Phone>();
                    }
                    _parentViewModel.Item.Phones.Add(phone);
                }
            }
        }

        private async Task SavePhoneToDatabase(Phone phone)
        {
            SetLoading(true);
            try
            {
                await Task.Run(() =>
                {
                    _uoW.Phones.Add(phone);
                    _uoW.Commit();
                });
                ShowNotification("Phone saved!");
            }
            catch (Exception e)
            {
                MessageBoxService.Show(e.Message, "Error");
            }
            finally
            {
                SetLoading(false);
            }
            
        }

        private bool ExistCustomer()
        {
            return _customerId > 0;
        }
    }
}
