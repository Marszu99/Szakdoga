﻿#pragma checksum "..\..\..\..\View\MyProfileView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "87B32AE97BD93AC2FA008A916F2DAD78CAEC56A8"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using TimeSheet.Resource;
using WpfDemo;
using WpfDemo.Components;


namespace WpfDemo.View {
    
    
    /// <summary>
    /// MyProfileView
    /// </summary>
    public partial class MyProfileView : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 52 "..\..\..\..\View\MyProfileView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal WpfDemo.Components.BindablePasswordBox MyProfilePassword;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\..\..\View\MyProfileView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox MyProfileFirstName;
        
        #line default
        #line hidden
        
        
        #line 87 "..\..\..\..\View\MyProfileView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox MyProfileLastName;
        
        #line default
        #line hidden
        
        
        #line 104 "..\..\..\..\View\MyProfileView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox MyProfileEmail;
        
        #line default
        #line hidden
        
        
        #line 121 "..\..\..\..\View\MyProfileView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox MyProfileTelephone;
        
        #line default
        #line hidden
        
        
        #line 138 "..\..\..\..\View\MyProfileView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ChangeUserValuesButton;
        
        #line default
        #line hidden
        
        
        #line 148 "..\..\..\..\View\MyProfileView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SaveChangedUserValuesButton;
        
        #line default
        #line hidden
        
        
        #line 158 "..\..\..\..\View\MyProfileView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CancelChangeUserValuesButton;
        
        #line default
        #line hidden
        
        /// <summary>
        /// UserToDoTasksDataGrid Name Field
        /// </summary>
        
        #line 195 "..\..\..\..\View\MyProfileView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        public System.Windows.Controls.DataGrid UserToDoTasksDataGrid;
        
        #line default
        #line hidden
        
        /// <summary>
        /// UserDoneTasksDataGrid Name Field
        /// </summary>
        
        #line 254 "..\..\..\..\View\MyProfileView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        public System.Windows.Controls.DataGrid UserDoneTasksDataGrid;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.14.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/WpfDemo;component/view/myprofileview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\View\MyProfileView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.14.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.14.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.MyProfilePassword = ((WpfDemo.Components.BindablePasswordBox)(target));
            return;
            case 2:
            this.MyProfileFirstName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.MyProfileLastName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.MyProfileEmail = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.MyProfileTelephone = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.ChangeUserValuesButton = ((System.Windows.Controls.Button)(target));
            return;
            case 7:
            this.SaveChangedUserValuesButton = ((System.Windows.Controls.Button)(target));
            return;
            case 8:
            this.CancelChangeUserValuesButton = ((System.Windows.Controls.Button)(target));
            return;
            case 9:
            this.UserToDoTasksDataGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 10:
            this.UserDoneTasksDataGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

