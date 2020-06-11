using System.Threading.Tasks;
using Autofac;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using MEDIDEA.Domain;
using MEDIDEA.UI.Common;

namespace MEDIDEA.UI.ViewModels
{
    public class VisitListViewModel: CollectionViewModel<Domain.Entities.Visit>
    {
        IVisitRepository _visitRepository;

        public VisitListViewModel()
        {
            _visitRepository = Bootstrapper.Container.Resolve<IVisitRepository>();
        }

        [AsyncCommand]
        public async Task OnLoaded()
        {
            var visits = await Task.Run(() => _visitRepository.GetAll(i => !i.IsDeleted, "Customer"));
            SetItems(visits);
        }

        [Command]
        public void ItemDoubleClick()
        {
            Navigation.Navigate("VisitView", SelectedItem.Id, this);
        }

        public bool CanItemDoubleClick()
        {
            return SelectedItem != null;
        }

        [Command]
        public void Create()
        {
            Navigation.Navigate("VisitView", null, this);
        }
    }
}
