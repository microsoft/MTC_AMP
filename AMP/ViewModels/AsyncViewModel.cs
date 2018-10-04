//*********************************************************
//
// Copyright (c) Microsoft. All rights reserved.
// This code is licensed under the MIT License (MIT).
// THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
// IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
// PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//*********************************************************
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
