using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Text.Json.Serialization;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace WebAPI.Page
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MyAPI
    {
        //public string ProjectPath = Directory.GetCurrentDirectory() + "\\NovaProject";
        public string ProjectPath = "D:\\NovaProject";
        public MyAPI()
        {
            this.InitializeComponent();
            //this.NavigationCacheMode = Microsoft.UI.Xaml.Navigation.NavigationCacheMode.Required;

            NowProjectSetting.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
            if (!Directory.Exists(ProjectPath))
            {
                Directory.CreateDirectory(ProjectPath);

            }
            else
            {
                if (Directory.GetFiles(ProjectPath).Length < 1)
                {
                    ProjectList.Items.Add("Not Found Project.");
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
        string ProjectInfo_ = "";

        private T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parent = VisualTreeHelper.GetParent(child);
            if (parent == null)
                return null;

            if (parent is T typedParent)
                return typedParent;

            return FindParent<T>(parent);
        }
        private void ProjectList_SelectionChanged(object sender, Microsoft.UI.Xaml.Controls.SelectionChangedEventArgs e)
        {
            if (ProjectList.SelectedIndex != -1)
            {
                NowProjectSetting.Visibility = Microsoft.UI.Xaml.Visibility.Visible;
                ProjectInfo_ = File.ReadAllText(ProjectList.SelectedItem.ToString() + "\\Main.novaProject");
                if (Text_GetCenter(ProjectInfo_, "<HTTPS>", "</HTTPS>") == "true")
                    EnableHTTPS.IsChecked = true;
                else
                    EnableHTTPS.IsChecked = false;

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(ProjectInfo_);

                XmlNodeList items = doc.GetElementsByTagName("Item");
                foreach (XmlNode item in items)
                {
                    string name = item.Attributes["Name"].Value;
                    bool must = bool.Parse(item.Attributes["Must"].Value);
                    string state = item.Attributes["State"].Value;
                    string text = item.Attributes["Text"].Value;
                    string content = item.Attributes["Content"].Value;

                    Item newItem = new Item(name, must, text, content, state);
                    Console.WriteLine(newItem);
                    PreList.Items.Add(newItem);
                }
            }
        }

        private void EnableHTTPS_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            string result = File.ReadAllText(ProjectList.SelectedItem.ToString() + "\\Main.novaProject");
            if ((bool)EnableHTTPS.IsChecked)
                result = result.Replace("false", "true");
            else
                result = result.Replace("true", "false");

            File.WriteAllText(ProjectList.SelectedItem.ToString() + "\\Main.novaProject", result);
        }

        private void AppBarButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            var item = new { Name = "pre1", Must = true, Text = "     ", Content = "Editable Element",State = true };
            PreList.Items.Add(item);
        }

        private void PreSave_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            foreach (Item itemData in PreList.Items)
            {
                string name = itemData.Name;
                bool must = itemData.Must;
                string remark = itemData.Content;
                bool state = bool.Parse(itemData.State);
                Console.WriteLine(name + " | " + must + " | " + remark + " | " + state);
            }
        }

        private void PreDel_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            if (PreList.SelectedItem != null)
            {
                PreList.Items.Remove(PreList.SelectedItem);
            }
        }
    }
    class Item
    {
        public string Name { get; set; }
        public bool Must { get; set; }
        public string Text { get; set; }
        public string Content { get; set; }
        public string State { get; set; }
        public Item(string name, bool must, string text, string content, string state)
        {
            Name = name;
            Must = must;
            Text = text;
            Content = content;
            State = state;
        }
    }
}
