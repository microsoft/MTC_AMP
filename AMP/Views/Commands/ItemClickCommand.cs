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
//
// See https://marcominerva.wordpress.com/2013/03/07/how-to-bind-the-itemclick-event-to-a-command-and-pass-the-clicked-item-to-it/
// for original implementation.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace AMP.Views.Commands
{
    public static class ItemClickCommand
    {
        public static readonly DependencyProperty CommandProperty =
        DependencyProperty.RegisterAttached("Command", typeof(ICommand),
        typeof(ItemClickCommand), new PropertyMetadata(null, OnCommandPropertyChanged));

        public static void SetCommand(DependencyObject d, ICommand value)
        {
            d.SetValue(CommandProperty, value);
        }

        public static ICommand GetCommand(DependencyObject d)
        {
            return (ICommand)d.GetValue(CommandProperty);
        }

        private static void OnCommandPropertyChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var control = d as ListViewBase;
            if (control != null)
                control.ItemClick += OnItemClick;
        }



      
        // Using a DependencyProperty as the backing store for CommandParameter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register("CommandParameter", typeof(object), typeof(ItemClickCommand), new PropertyMetadata(null));

        public static void SetCommandParameter(DependencyObject d, object value)
        {
            d.SetValue(CommandParameterProperty, value);
        }

        public static object GetCommandParameter(DependencyObject d)
        {
            return d.GetValue(CommandParameterProperty);
        }


        // Using a DependencyProperty as the backing store for CommandParameter.  This enables animation, styling, binding, etc...

        private static void OnItemClick(object sender, ItemClickEventArgs e)
        {
            Type castType;
            var control = sender as ListViewBase;
            var command = GetCommand(control);
            var param = GetCommandParameter(control);

            var generics = command.GetType().GetGenericArguments();
            if (generics.Length > 0)
                castType = generics[0];

            if (command != null)
                if (param == null && command.CanExecute(e.ClickedItem))
                    command.Execute(e.ClickedItem);
                else if (param != null && command.CanExecute(param))
                        command.Execute(param);
        }
    }

}

