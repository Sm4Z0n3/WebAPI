using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
        public string ProjectPath = Directory.GetCurrentDirectory() + "\\Project";
        public MyAPI()
        {
            this.InitializeComponent();
            if (!Directory.Exists(ProjectPath))
            {
                if (Directory.GetFiles(ProjectPath).Length == 0)
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
    }
}
