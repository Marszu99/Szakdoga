﻿#pragma checksum "..\..\..\..\View\UserProfileView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3E6D954B38F2E206991BF71DC87BBC60F8D51DDC"
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


namespace WpfDemo.View {
    
    
    /// <summary>
    /// UserProfileView
    /// </summary>
    public partial class UserProfileView : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 30 "..\..\..\..\View\UserProfileView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label userNameLabel;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\..\View\UserProfileView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label firstNameLabel;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\..\View\UserProfileView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lastNameLabel;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\..\View\UserProfileView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label emailLabel;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\..\..\View\UserProfileView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label telephoneLabel;
        
        #line default
        #line hidden
        
        
        #line 78 "..\..\..\..\View\UserProfileView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TaskSearchingText;
        
        #line default
        #line hidden
        
        
        #line 95 "..\..\..\..\View\UserProfileView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox RecordSearchingText;
        
        #line default
        #line hidden
        
        
        #line 119 "..\..\..\..\View\UserProfileView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddTaskToUserTaskButton;
        
        #line default
        #line hidden
        
        /// <summary>
        /// UserTasksDataGrid Name Field
        /// </summary>
        
        #line 131 "..\..\..\..\View\UserProfileView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        public System.Windows.Controls.DataGrid UserTasksDataGrid;
        
        #line default
        #line hidden
        
        /// <summary>
        /// UserRecordsDataGrid Name Field
        /// </summary>
        
        #line 215 "..\..\..\..\View\UserProfileView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        public System.Windows.Controls.DataGrid UserRecordsDataGrid;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.15.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/WpfDemo;component/view/userprofileview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\View\UserProfileView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.15.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.userNameLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.firstNameLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.lastNameLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.emailLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.telephoneLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.TaskSearchingText = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.RecordSearchingText = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.AddTaskToUserTaskButton = ((System.Windows.Controls.Button)(target));
            return;
            case 9:
            this.UserTasksDataGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 10:
            this.UserRecordsDataGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

