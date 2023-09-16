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
        //public string ProjectPath = Directory.GetCurrentDirectory() + "\\NovaProject";
        public string ProjectPath = "D:\\NovaProject";

        bool NewProjectName = false;
        public Project()
        {
            this.InitializeComponent();
            ProjectList.Items.Clear();
            if (!Directory.Exists(ProjectPath))
            {
                Directory.CreateDirectory(ProjectPath);

            }
            else
            {
                if (Directory.GetFiles(ProjectPath).Length <1)
                {
                    ProjectList.Items.Add("Not Found Project.");
                }
                else
                {
                    string[] folders = Directory.GetDirectories(ProjectPath, "*", SearchOption.AllDirectories);
                    foreach (string folder in folders)
                    {
                        Console.WriteLine(folder);
                        ProjectList.Items.Add(folder.Replace(ProjectPath + "\\", ""));
                    }
                }
            }
        }
        string ProjectInfo_ = "";
        private void ProjectList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (ProjectList.SelectedIndex != -1)
                {
                    ProjectInfo_ = File.ReadAllText(ProjectPath + "\\" + ProjectList.SelectedItem.ToString() + "\\Main.novaProject");
                    ProjectCreatName.Text = Text_GetCenter(ProjectInfo_, "<ProjectName>", "</ProjectName>");
                    ProjectCreatTime.Text = Text_GetCenter(ProjectInfo_, "<CreatTime>", "</CreatTime>");
                    ProjectBody.Text = Text_GetCenter(ProjectInfo_, "<ProjectBody>", "</ProjectBody>");
                }
            }
            catch{}

        }

        private void Project_del_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ProjectList.SelectedItem != null)
                {
                    if(ProjectList.SelectedValue.ToString() != "Not Found Project.") 
                    {
                    Directory.Delete(ProjectPath + "\\" + ProjectList.SelectedValue.ToString(), true);
                    Object delitem = ProjectList.SelectedItem;
                    ProjectList.SelectedItem = null;
                    ProjectList.Items.Remove(delitem);
                    }
                }
            }
            catch { }
        }

        private void Project_ref_Click(object sender, RoutedEventArgs e)
        {
            ProjectList.Items.Clear();
            //Console.WriteLine($"ProjectPath: {ProjectPath}\nPathExists: {Directory.Exists(ProjectPath)}\nFiles Num: {Directory.GetFiles(ProjectPath).Length}");
            if (!Directory.Exists(ProjectPath))
                Directory.CreateDirectory(ProjectPath);
            else
            {
                if (Directory.GetFiles(ProjectPath).Length >= 1)
                {
                    string[] folders = Directory.GetDirectories(ProjectPath, "*", SearchOption.AllDirectories);
                    foreach (string folder in folders)
                    {
                        Console.WriteLine(folder);
                        ProjectList.Items.Add(folder.Replace(ProjectPath + "\\",""));
                    }
                }
            }
        }

        private void ProjectBody_Save_Click(object sender, RoutedEventArgs e)
        {
            string newBody = ProjectBody.Text;
            string oldBody = Text_GetCenter(ProjectInfo_, "<ProjectBody>", "</ProjectBody>");
            string result = ProjectInfo_.Replace(oldBody,newBody);
            File.WriteAllText(ProjectPath + "\\" + ProjectCreatName.Text + "\\Main.novaProject", result);
        }

        private void ProjectNew_Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Directory.Exists(ProjectPath + "\\" + ProjectNew_Name.Text))
                NewProjectName = false;
            else
                NewProjectName = true;
        }

        private void ProjectNew_Creat_Click(object sender, RoutedEventArgs e)
        {
            if(NewProjectName)
            {
                Directory.CreateDirectory(ProjectPath + "\\" + ProjectNew_Name.Text);
                File.WriteAllText(ProjectPath + "\\" + ProjectNew_Name.Text + "\\Main.novaProject",$"<Project>\n\t<ProjectInfo>\n\t\t<ProjectName>{ProjectNew_Name.Text}</ProjectName>\n\t\t<HTTPS>true</HTTPS>\n\t\t<CreatTime>{DateTime.Now.ToString("yyyy-MM-dd HH:mm")}</CreatTime>\n\t</ProjectInfo>\n\t<Items>\n\t\t<Item Name=\"pre1\" Must=\"true\" Text=\"     \" Content=\"±¸×¢\" State=\"true\"></Item>\n\t</Items>\n\t<ProjectBody></ProjectBody>\n</Project>");
            }
            Project_ref_Click(new object(), new RoutedEventArgs());
        }

        public string Text_GetCenter(string source, string first, string last)
        {
            string result = "";
            int startIndex = source.IndexOf(first);
            int endIndex = source.IndexOf(last);
            if (startIndex >= 0 && endIndex >= 0)
            {
                startIndex += first.Length; // move to the end of the first text
                int length = endIndex - startIndex; // calculate the length of the text between
                result = source.Substring(startIndex, length); // result is " and "
            }
            return result;
        }


    }
}
