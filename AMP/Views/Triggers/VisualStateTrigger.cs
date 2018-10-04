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
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace AMP.Views.Triggers
{
    public class VisualStateTrigger : StateTriggerBase
    {
        string _guid;

        public VisualStateTrigger()
        {
            _guid = Guid.NewGuid().ToString();
        }
        public IVisualState ViewModel
        {
            get { return (IVisualState)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ViewModel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(IVisualState), typeof(VisualStateTrigger), new PropertyMetadata(default(object), OnViewModelChanged));

        private static void OnViewModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var model = e.NewValue as IVisualState;
            var me = d as VisualStateTrigger;
         
           
            if (model != null)
                model.VisualStateChanged += me.Model_StateChanged;
        }

      

        public string TargetState { get; set; }

        private async void Model_StateChanged(object model, VisualStateEventArgs args)
        {
            var isActive = args.NewState.ToString().Equals(TargetState);
            Debug.WriteLine($"VisualStateTrigger {_guid}: target = {TargetState} new: {args.NewState.ToString()} result={isActive}");
            await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.High, () =>
            {

                SetActive(isActive);
            });


        }
    }
}
