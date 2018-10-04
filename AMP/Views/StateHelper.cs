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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace AMP.Views
{
    
    public class StateHelper : DependencyObject
    {
        public StateHelper()
        {

        }

        public static readonly DependencyProperty StateProperty = DependencyProperty.RegisterAttached(
            "State", typeof(String), typeof(StateHelper), new PropertyMetadata("Normal", StateChanged));


        internal static void StateChanged(DependencyObject target, DependencyPropertyChangedEventArgs args)
        {
            try
            {
                string newState = (string)args.NewValue;
                if (args.NewValue != null)
                {
                    bool res = VisualStateManager.GoToState((Control)target, newState, true);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("StateHelper: " + ex.Message);
            }
        }

        public static void SetState(DependencyObject obj, string value)
        {
            obj.SetValue(StateProperty, value);
        }

        public static string GetState(DependencyObject obj)
        {
            return (string)obj.GetValue(StateProperty);
        }
    }
}
