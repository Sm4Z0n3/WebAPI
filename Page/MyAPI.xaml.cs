using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using Windows.UI.Notifications;
using System.Net;
using System.Threading;
using Pchp.Core;
using System.Threading.Tasks;
using System.Net.Http;
using System.Diagnostics;
using System.Collections.Specialized;

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

        public bool APIEnable = false;
        public MyAPI()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = Microsoft.UI.Xaml.Navigation.NavigationCacheMode.Required;

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
            PreList.Items.Clear();
            if (ProjectList.SelectedIndex != -1)
            {
                NowProjectSetting.Visibility = Microsoft.UI.Xaml.Visibility.Visible;
                ProjectInfo_ = File.ReadAllText(ProjectList.SelectedItem.ToString() + "\\Main.novaProject");
                if (Text_GetCenter(ProjectInfo_, "<HTTPS>", "</HTTPS>") == "true")
                    EnableHTTPS.IsChecked = true;
                else
                    EnableHTTPS.IsChecked = false;

                try
                {
                    // 读取和解析JSON数据
                    string json = Text_GetCenter(ProjectInfo_, "<Items>", "</Items>"); // 用实际的JSON文件路径替换 "yourJsonFile.json"

                    JObject jsonObject = JObject.Parse(json);
                    JArray presArray = jsonObject["pres"] as JArray;
                    if (presArray != null)
                    {
                        foreach (JObject itemObject in presArray)
                        {
                            string name = itemObject["Name"]?.ToString();
                            bool must = itemObject["Must"]?.ToObject<bool>() ?? false;
                            string remark = itemObject["remark"]?.ToString();
                            bool state = itemObject["state"]?.ToObject<bool>() ?? false;

                            int mustSelectedIndex = must ? 0 : 1; // 根据Must属性设置SelectedIndex
                            int stateSelectedIndex = state ? 0 : 1;

                            PreList.Items.Add(new { Name = name, Must = mustSelectedIndex, Text = "     ", Content = remark, State = stateSelectedIndex });
                        }
                    }
                }
                catch { }
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
            Console.WriteLine(PreList.Items.ToArray().Length);
            foreach (WebAPI.Page.Item item in PreList.Items.ToList())
            {
                // 获取选中列表项的数据
                string name = item.Name;
                bool must = item.Must;
                string remark = item.Content;
                bool state = bool.Parse(item.State);
                Console.WriteLine($"Name: {name}, Must: {must}, Remark: {remark}, State: {state}");
                // 在这里可以使用选中列表项的数据

            }
        }

        private void PreDel_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            if (PreList.SelectedItem != null)
            {
                PreList.Items.Remove(PreList.SelectedItem);
            }
        }

        public static async Task<string> RunPHPCodeAsync(string phpScript)
        {
            var phpScript1 = @"
                <?php
                function add($a, $b) {
                    return $a + $b;
                }
        
                $result = add(5, 3);
                echo $result;
                ?>
            ";

            var phpRunner = new PhpRunner();
            var result = await phpRunner.ExecutePhpScriptAsync(phpScript);

            return result;
        }

        private string ConvertItemsToJSON()
        {
            // 创建一个List<Item>对象用于存储Item的数据
            List<Item> itemList = new List<Item>();

            // 遍历items集合并将每个Item转化为Item对象并添加到List中
            foreach (Item itemData in PreList.Items)
            {
                Item item = new Item(itemData.Name, itemData.Must, itemData.Text, itemData.Content, itemData.State);
                itemList.Add(item);
            }

            // 使用JSON库将List<Item>对象序列化为JSON字符串
            string jsonString = JsonConvert.SerializeObject(itemList, Newtonsoft.Json.Formatting.Indented);

            return jsonString;
        }

        HttpServer httpServer = new HttpServer();
        private void ApiStart_Click(object sender, RoutedEventArgs e)
        {
            httpServer = new HttpServer((bool)EnableHTTPS.IsChecked);
            httpServer.CodeFile = ProjectList.SelectedValue.ToString() + "\\return.php";
            if (APIEnable)
            {
                APIEnable = false;
                httpServer.Stop();
                Console.WriteLine("项目" + ProjectList.SelectedValue.ToString().Replace(ProjectPath, "") + "已经关闭...");
                //ShowSystemMessage("NovaAPI服务", "项目" + ProjectList.SelectedValue.ToString().Replace(ProjectPath, "") + "已经关闭...");
            }
            else
            {
                APIEnable = true;
                
                httpServer.Start();
                if ((bool)EnableHTTPS.IsChecked)
                    Console.WriteLine("项目" + ProjectList.SelectedValue.ToString().Replace(ProjectPath, "") + "已经启动...\n" + "https://localhost:8080/");
                else
                    Console.WriteLine("项目" + ProjectList.SelectedValue.ToString().Replace(ProjectPath, "") + "已经启动...\n" + "http://localhost:8080/");
                //ShowSystemMessage("NovaAPI服务","项目" + ProjectList.SelectedValue.ToString().Replace(ProjectPath,"")+ "已经启动...");
            }
        }

        public void ShowSystemMessage(string title,string Content)
        {
            var toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText01);
            var textElements = toastXml.GetElementsByTagName("text");
            textElements[0].AppendChild(toastXml.CreateTextNode(title));
            textElements[1].AppendChild(toastXml.CreateTextNode(Content));

            var toast = new ToastNotification(toastXml);

            // 显示通知
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }

        private void EditCode_Click(object sender, RoutedEventArgs e)
        {
            string filePath = ProjectList.SelectedItem.ToString() + "\\return.php"; // 将文件路径替换为您想要打开的文件的路径
            Process.Start("notepad.exe", filePath);
        }

        private void RefreshList_Click(object sender, RoutedEventArgs e)
        {
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
    }

    public class HttpServer
    {
        private readonly HttpListener listener;
        private bool isRunning = false;
        private Thread serverThread;
        public string port = "8080";
        public string CodeFile = "";
        public HttpServer(bool useHttps = false)
        {
            listener = new HttpListener();

            if (useHttps)
            {
                // 如果要使用HTTPS，请配置SSL证书
                listener.Prefixes.Add($"https://localhost:{port}/");
                // listener.Prefixes.Add("https://yourdomain.com:443/");
            }
            else
            {
                listener.Prefixes.Add($"http://localhost:{port}/");
            }
        }

        public void Start()
        {
            if (!isRunning)
            {
                isRunning = true;
                serverThread = new Thread(Listen);
                serverThread.Start();
            }
        }

        public void Stop()
        {
            if (isRunning)
            {
                isRunning = false;
                listener.Stop();
                serverThread.Join(); // 等待服务器线程完成
                Console.WriteLine("HTTP服务器已关闭");
            }
        }

        private void Listen()
        {
            try
            {
                listener.Start();
                Console.WriteLine("HTTP服务器已启动");

                while (isRunning)
                {
                    // 接受传入的HTTP请求
                    HttpListenerContext context = listener.GetContext();
                    ThreadPool.QueueUserWorkItem(ProcessRequest, context);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"HTTP服务器错误: {ex.Message}");
            }
        }

        private async void ProcessRequest(object state)
        {
            HttpListenerContext context = (HttpListenerContext)state;
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;

            try
            {
                // 获取请求方法（GET, POST等）
                string httpMethod = request.HttpMethod;

                // 获取请求的URL
                string requestUrl = request.Url.ToString();

                // 获取URL中的所有参数
                NameValueCollection queryString = request.QueryString;

                // 获取请求内容（如果有的话）
                string requestBody = "";

                if (httpMethod == "POST" && request.HasEntityBody)
                {
                    using (var reader = new StreamReader(request.InputStream))
                    {
                        requestBody = reader.ReadToEnd();
                    }
                }

                // 构建响应内容
                string responseString = "";

                //responseString = $"HTTP Method: {httpMethod}\n";
                //responseString += $"Request URL: {requestUrl}\n";

                // 打印所有参数
                //responseString += "Parameters:\n";

                string phppre = "";
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"所有URL参数");
                foreach (string key in queryString.AllKeys)
                {
                    //responseString += $"{key}: {queryString[key]}\n";
                    phppre += $"${key}={queryString[key]};";
                    Console.WriteLine($"{key}: {queryString[key]}");
                }
                Console.ForegroundColor = ConsoleColor.White;
                responseString += await WebAPI.Page.MyAPI.RunPHPCodeAsync(phppre + File.ReadAllText(CodeFile));
                byte[] buffer = System.Text.Encoding.ASCII.GetBytes(responseString);
                response.ContentType = "text/html";
                response.ContentLength64 = buffer.Length;
                response.OutputStream.Write(buffer, 0, buffer.Length);
                response.OutputStream.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"处理HTTP请求时出错: {ex.Message}");
            }
            finally
            {
                response.Close();
            }
        }
    }

    public class PhpRunner
    {
        private readonly HttpClient httpClient = new HttpClient();
        private readonly string apiUrl = "http://elegy.xn--6qq986b3xl.icu/1.php/?code="; // 替换为您的API端点

        public async Task<string> ExecutePhpScriptAsync(string phpScript)
        {
            try
            {
                var response = await httpClient.GetAsync($"{apiUrl}{Uri.EscapeDataString(phpScript)}");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return result;
                }
                else
                {
                    return "HTTP请求失败";
                }
            }
            catch (Exception ex)
            {
                return $"错误：{ex.Message}";
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
