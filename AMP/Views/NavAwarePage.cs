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
using AMP.ViewModels;
using AMP.Services;
using System.Diagnostics;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml;

namespace AMP.Views
{
    public class NavAwarePage : Page
    {

        public NavAwarePage() : base()
        {
            this.Loaded += NavAwarePage_Loaded;
        }

        private void NavAwarePage_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Debug.WriteLine("loaded");
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            await InitializeViewModels(this, e.Parameter);

            if (this.Content is Panel)
            {
                var panel = this.Content as Panel;
                foreach (var child in panel.Children)
                {
                    if (child is FrameworkElement)
                        await InitializeViewModels(child as FrameworkElement, e.Parameter);
                }
            }
        }

        private async System.Threading.Tasks.Task InitializeViewModels(FrameworkElement element, object parameter)
        {
            var navigableViewModel = element.DataContext as INavAware;
            if (navigableViewModel != null)
                navigableViewModel.Activate(parameter);

            var asyncViewModel = element.DataContext as IAsyncInitialization;
            if (asyncViewModel != null)
                await asyncViewModel.Initialization;
        }


        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            var navigableViewModel = this.DataContext as INavAware;
            if (navigableViewModel != null)
                navigableViewModel.Deactivate(e.Parameter);
        }

    }
}
