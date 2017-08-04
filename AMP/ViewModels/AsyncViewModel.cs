using GalaSoft.MvvmLight;
using AMP.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;

namespace AMP.ViewModels
{

    public abstract class AsyncViewModel : ViewModelBase, IAsyncInitialization, INavAware
    {
        private static CoreDispatcher _dispatcher;

        public Task Initialization { get { return InitializeAsync(); } }

        protected AsyncViewModel() :base()
        {
            Debug.WriteLine("AsyncViewModel ctor");
            
            if (_dispatcher == null)
                _dispatcher = Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher;
        }

        protected Task RunAsync(Action a) { 
            return _dispatcher.RunAsync(CoreDispatcherPriority.Normal, ()=> { a.Invoke(); }).AsTask();
        }
        protected virtual async Task InitializeAsync() { }

        public virtual void Activate(object parameter) { }

        public virtual void Deactivate(object parameter) { }
    }
}
