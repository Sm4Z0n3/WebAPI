// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
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
    public sealed partial class Project
    {
        public string ProjectPath = Directory.GetCurrentDirectory() + "\\Project";
        public Project()
        {
            this.InitializeComponent();
            if (!Directory.Exists(ProjectPath))
            {
                if(Directory.GetFiles(ProjectPath).Length == 0)
                {
                    ProjectList.Items.Add("Not Found Project.");
                }
                Directory.CreateDirectory(ProjectPath);
            }
            else
            {
                string[] folders = Directory.GetDirectories(ProjectPath, "*", SearchOption.AllDirectories);
                foreach (string folder in folders)
                {
                    Console.WriteLine(folder);
                    ProjectList.Items.Add(folder);
                }
            }
        }

        private void ProjectList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProjectList.SelectedValue.ToString() != "Not Found Project Directory.")
            {
                Console.WriteLine(ProjectList.SelectedValue.ToString());
            }
        }

        private void Project_del_Click(object sender, RoutedEventArgs e)
        {
            if (ProjectList.SelectedValue.ToString() != "Not Found Project Directory.")
            {
                Console.WriteLine(ProjectList.SelectedValue.ToString());
            }
        }
    }
}
