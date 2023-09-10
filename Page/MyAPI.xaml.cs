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
        public MyAPI()
        {
            this.InitializeComponent();
            if (!Directory.Exists("Project"))
            {
                ProjectList.Items.Add("Not Found Project Directory.");
                //Directory.CreateDirectory("Project");
            }
        }
    }
}
