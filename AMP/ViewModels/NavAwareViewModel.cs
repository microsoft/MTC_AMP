
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMP.ViewModels
{
    public abstract class NavAwareViewModel : ViewModelBase, INavAware
    {
        public abstract void Activate(object parameter);

        public abstract void Deactivate(object parameter);
      
    }
}
