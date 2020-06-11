using DevExpress.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DevExpress.Mvvm.DataAnnotations;
using MEDIDEA.UI.Common;
using MEDIDEA.UI.Customer;
using MEDIDEA.UI.Visit;

namespace MEDIDEA.UI
{
    public class MainViewModel : BaseViewModel
    {
        public ReadOnlyCollection<IHamburgerMenuItemViewModel> HamburgerMenuItems { get; }
        public ReadOnlyCollection<IHamburgerMenuBottomBarItemViewModel> HamburgerMenuBottomBarItems { get; }

        public object HamburgerMenuSelectedItem
        {
            get { return GetProperty(() => HamburgerMenuSelectedItem); }
            set { SetProperty(() => HamburgerMenuSelectedItem, value); }
        }

        public MainViewModel()
        {
            HamburgerMenuItems = new ReadOnlyCollection<IHamburgerMenuItemViewModel>(InitializeMenuItems());
        }

        protected virtual IList<IHamburgerMenuItemViewModel> InitializeMenuItems()
        {
            var result = new List<IHamburgerMenuItemViewModel>();
            result.Add(new NavigationItemModel("Customers") { NavigationTarget = typeof(CustomersListView), Glyph = "Icons/Home.png" });
            result.Add(new NavigationItemModel("Visits") { NavigationTarget = typeof(VisitListView), Glyph = "Icons/Check.png" });
            //result.Add(new SeparatorItem());
            
            result.Add(new NavigationItemModel("About") { NavigationTarget = typeof(About), IsAlternatePlacementItem = true, Glyph = "Icons/About.png" });
            return result;
        }

        [Command]
        public void OnLoaded()
        {
            Navigation.Navigate("CustomersListView", null, this);
        }
    }
}
