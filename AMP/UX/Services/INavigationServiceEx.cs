using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace AMP.UX.Services
{
    public interface INavigationServiceEx : INavigationService
    {
        Frame SubFrame { get; set; }

        bool CanGoBack { get; }
        bool RemoveBackEntry();
    }
}
