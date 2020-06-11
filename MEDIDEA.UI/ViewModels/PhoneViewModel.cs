using MEDIDEA.Domain.Entities;
using MEDIDEA.UI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEDIDEA.UI.ViewModels
{
    public class PhoneViewModel: EntityViewModel<Phone>
    {
        public void SetPhone(Phone p)
        {
            SetItem(p);
        }
    }
}
