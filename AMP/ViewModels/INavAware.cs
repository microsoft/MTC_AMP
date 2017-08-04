using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMP.ViewModels
{
    public interface INavAware
    {
        void Activate(object parameter);
        void Deactivate(object parameter);
    }
}
