////*********************************************************
//
// Copyright (c) Microsoft. All rights reserved.
// This code is licensed under the MIT License (MIT).
// THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
// IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
// PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//*********************************************************


using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace AMP.UX.Services
{
    /// <summary>
    /// Windows 8 and Windows Phone Application 8.1 implementation of <see cref="INavigationService"/>.
    /// </summary>

    public class FrameNavigationService : INavigationService, INavigationServiceEx
    {
        ViewTarget _lastTarget;
        Frame _subFrame;
        /// <summary>
        /// The key that is returned by the <see cref="CurrentPageKey"/> property
        /// when the current Page is the root page.
        /// </summary>
        public const string RootPageKey = "-- ROOT --";

        /// <summary>
        /// The key that is returned by the <see cref="CurrentPageKey"/> property
        /// when the current Page is not found.
        /// This can be the case when the navigation wasn't managed by this NavigationService,
        /// for example when it is directly triggered in the code behind, and the
        /// NavigationService was not configured for this page type.
        /// </summary>
        public const string UnknownPageKey = "-- UNKNOWN --";

        private readonly Dictionary<string, ViewInfo> _pagesByKey = new Dictionary<string, ViewInfo>();

        /// <summary>
        /// The key corresponding to the currently displayed page.
        /// </summary>
        public string CurrentPageKey
        {
            get
            {
                lock (_pagesByKey)
                {
                    var frame = GetCurrentFrame();

                    if (frame.BackStackDepth == 0)
                    {
                        return RootPageKey;
                    }

                    if (frame.Content == null)
                    {
                        return UnknownPageKey;
                    }

                    var currentType = frame.Content.GetType();

                    if (_pagesByKey.All(p => p.Value.View != currentType))
                    {
                        return UnknownPageKey;
                    }

                    var item = _pagesByKey.FirstOrDefault(
                        i => i.Value.View == currentType);

                    return item.Key;
                }
            }
        }


        public Frame SubFrame
        {
            get { return _subFrame; }
            set { _subFrame = value; }
        }




        public bool CanGoBack
        {
            get
            {

                var frame = this.GetCurrentFrame();
                if (frame != null)
                    return frame.CanGoBack;

                return false;

            }
        }

        public bool RemoveBackEntry()
        {
            var frame = this.GetCurrentFrame();
            if (frame.CanGoBack)
            {
                frame.BackStack.RemoveAt(frame.BackStackDepth - 1);
                return true;
            }

            return false;

        }

        private Frame GetCurrentFrame()
        {
            if (_lastTarget == ViewTarget.MainFrame)
                return (Frame)Window.Current.Content;

            return _subFrame;
        }

        private Frame GetTargetFrame(ViewTarget target)
        {
            _lastTarget = target;
            if (target == ViewTarget.MainFrame)
                return (Frame)Window.Current.Content;

            return _subFrame;
        }

        /// <summary>
        /// Displays a new page corresponding to the given key. 
        /// Make sure to call the <see cref="Configure"/>
        /// method first.
        /// </summary>
        /// <param name="pageKey">The key corresponding to the page
        /// that should be displayed.</param>
        /// <exception cref="ArgumentException">When this method is called for 
        /// a key that has not been configured earlier.</exception>
        public void NavigateTo(string pageKey)
        {
            NavigateTo(pageKey, null);
        }

        /// <summary>
        /// Displays a new page corresponding to the given key,
        /// and passes a parameter to the new page.
        /// Make sure to call the <see cref="Configure"/>
        /// method first.
        /// </summary>
        /// <param name="pageKey">The key corresponding to the page
        /// that should be displayed.</param>
        /// <param name="parameter">The parameter that should be passed
        /// to the new page.</param>
        /// <exception cref="ArgumentException">When this method is called for 
        /// a key that has not been configured earlier.</exception>
        public virtual void NavigateTo(string pageKey, object parameter)
        {
            lock (_pagesByKey)
            {
                if (!_pagesByKey.ContainsKey(pageKey))
                {
                    throw new ArgumentException(
                        string.Format(
                            "No such page: {0}. Did you forget to call NavigationService.Configure?",
                            pageKey),
                        "pageKey");
                }
                var frame = GetTargetFrame(_pagesByKey[pageKey].Target);
                
                frame.Navigate(_pagesByKey[pageKey].View, parameter);
            }
        }

       

        /// <summary>
        /// Adds a key/page pair to the navigation service.
        /// </summary>
        /// <param name="key">The key that will be used later
        /// in the <see cref="NavigateTo(string)"/> or <see cref="NavigateTo(string, object)"/> methods.</param>
        /// <param name="viewInfo">The type of the page corresponding to the key.</param>
        public void Configure(string key, ViewInfo viewInfo)
        {
            lock (_pagesByKey)
            {
                if (_pagesByKey.ContainsKey(key))
                {
                    throw new ArgumentException("This key is already used: " + key);
                }

                if (_pagesByKey.Any(p => p.Value.View == viewInfo.View))
                {
                    throw new ArgumentException(
                        "This type is already configured with key " + _pagesByKey.First(p => p.Value.View == viewInfo.View).Key);
                }

                _pagesByKey.Add(
                    key,
                    viewInfo);
            }
        }

        public void GoBack()
        {
            var frame = GetCurrentFrame();

            if (frame.CanGoBack)
            {
                frame.GoBack();
            }

        }
    }
}
