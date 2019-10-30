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
using Windows.UI.Xaml.Media;
using System.Threading.Tasks;

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

            if (this.Content is Panel root)
            {
                await InitializeChildrenAsync(root, e.Parameter, this.DataContext);
            }
        }

        private async Task InitializeChildrenAsync(FrameworkElement uiElement, object state, object parentDataContext = null)
        {
           if (uiElement == null)
                return;

           if (!uiElement.DataContext.Equals(parentDataContext))
            await InitializeViewModels(uiElement, state);
           
           if (uiElement is Panel panel)
            {
                foreach (var element in panel.Children)
                {
                    await InitializeChildrenAsync(element as FrameworkElement, state, uiElement.DataContext);
                }
            }
            else if (uiElement is UserControl userControl)
            {
                await InitializeChildrenAsync(userControl.Content as FrameworkElement, state, uiElement.DataContext);
            }
            else if (uiElement is ContentControl contentControl)
            {
                var uiElementAsContentControl = (ContentControl)uiElement;
                await InitializeChildrenAsync(contentControl.Content as FrameworkElement, state, uiElement.DataContext);
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
