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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace AMP.Views
{
    public class AsyncUserControl : UserControl
    {
        public AsyncUserControl() : base()
        {
            this.Loaded += AsyncUserControl_Loaded;
        }

        private async void AsyncUserControl_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var vm = this.DataContext;
            if (vm is AsyncViewModel)
            {
                await (vm as AsyncViewModel).Initialization;
            }
        }
    }
}
