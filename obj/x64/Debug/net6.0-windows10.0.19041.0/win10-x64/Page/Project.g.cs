﻿#pragma checksum "E:\CSProject\WebAPI\Page\Project.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "03E1034CAC9895B3C8A02E41E93A6719597C203173D8A74827FD15A3D2727036"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebAPI.Page
{
    partial class Project : 
        global::Microsoft.UI.Xaml.Controls.Page, 
        global::Microsoft.UI.Xaml.Markup.IComponentConnector
    {

        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 1.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // Page\Project.xaml line 17
                {
                    this.ProjectList = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.ListBox>(target);
                    ((global::Microsoft.UI.Xaml.Controls.ListBox)this.ProjectList).SelectionChanged += this.ProjectList_SelectionChanged;
                }
                break;
            case 3: // Page\Project.xaml line 19
                {
                    this.ProjectMan = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Border>(target);
                }
                break;
            case 4: // Page\Project.xaml line 25
                {
                    this.ProjectNew = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Border>(target);
                }
                break;
            case 5: // Page\Project.xaml line 32
                {
                    this.ProjectInfo = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Border>(target);
                }
                break;
            case 6: // Page\Project.xaml line 39
                {
                    this.ProjectBody = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBox>(target);
                }
                break;
            case 7: // Page\Project.xaml line 40
                {
                    this.ProjectSaveBody = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Button>(target);
                    ((global::Microsoft.UI.Xaml.Controls.Button)this.ProjectSaveBody).Click += this.ProjectSaveBody_Click;
                }
                break;
            case 8: // Page\Project.xaml line 35
                {
                    this.ProjectCreatName = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBlock>(target);
                }
                break;
            case 9: // Page\Project.xaml line 36
                {
                    this.ProjectCreatTime = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBlock>(target);
                }
                break;
            case 10: // Page\Project.xaml line 28
                {
                    this.ProjectNew_Name = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBox>(target);
                    ((global::Microsoft.UI.Xaml.Controls.TextBox)this.ProjectNew_Name).TextChanged += this.ProjectNew_Name_TextChanged;
                }
                break;
            case 11: // Page\Project.xaml line 29
                {
                    this.ProjectNew_Creat = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Button>(target);
                    ((global::Microsoft.UI.Xaml.Controls.Button)this.ProjectNew_Creat).Click += this.ProjectNew_Creat_Click;
                }
                break;
            case 12: // Page\Project.xaml line 21
                {
                    this.Project_del = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Button>(target);
                    ((global::Microsoft.UI.Xaml.Controls.Button)this.Project_del).Click += this.Project_del_Click;
                }
                break;
            case 13: // Page\Project.xaml line 22
                {
                    this.Project_ref = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Button>(target);
                    ((global::Microsoft.UI.Xaml.Controls.Button)this.Project_ref).Click += this.Project_ref_Click;
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 1.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Microsoft.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Microsoft.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

