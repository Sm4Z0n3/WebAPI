// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Xaml;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WebAPI.Page
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Home
    {
        public int AllApi = 0;
        public int OnlineApi = 0;
        public int OfflineApi = 0;
        public int ErrorApi = 0;

        public Home()
        {
            this.InitializeComponent();
            AllAPI_.Text = "All API : " + AllApi.ToString();
            OnlineAPI_.Text = "Online API : " + OnlineApi.ToString();
            OfflineAPI_.Text = "Offline API : " + OfflineApi.ToString();
            ErrorAPI_.Text = "Error API : " + ErrorApi.ToString();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AllAPI_.Text = "All API : " + AllApi.ToString();
            OnlineAPI_.Text = "Online API : " + OnlineApi.ToString();
            OfflineAPI_.Text = "Offline API : " + OfflineApi.ToString();
            ErrorAPI_.Text = "Error API : " + ErrorApi.ToString();
        }

    }
}
