﻿#pragma checksum "..\..\..\..\View\UserProfileTaskView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3A0AE78A0D508266B8A867AFCAA72B8A4A54495E"
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
    /// UserProfileTaskView
    /// </summary>
    public partial class UserProfileTaskView : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 178 "..\..\..\..\View\UserProfileTaskView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox AddTaskUser;
        
        #line default
        #line hidden
        
        
        #line 189 "..\..\..\..\View\UserProfileTaskView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox AddTaskTitle;
        
        #line default
        #line hidden
        
        
        #line 199 "..\..\..\..\View\UserProfileTaskView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox AddTaskDescription;
        
        #line default
        #line hidden
        
        
        #line 210 "..\..\..\..\View\UserProfileTaskView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker AddTaskDeadline;
        
        #line default
        #line hidden
        
        
        #line 250 "..\..\..\..\View\UserProfileTaskView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CreateTaskStatus;
        
        #line default
        #line hidden
        
        
        #line 273 "..\..\..\..\View\UserProfileTaskView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SaveTaskToUserButton;
        
        #line default
        #line hidden
        
        
        #line 282 "..\..\..\..\View\UserProfileTaskView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BackToListButton;
        
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
            System.Uri resourceLocater = new System.Uri("/WpfDemo;component/view/userprofiletaskview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\View\UserProfileTaskView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
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
            this.AddTaskUser = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.AddTaskTitle = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.AddTaskDescription = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.AddTaskDeadline = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 5:
            this.CreateTaskStatus = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.SaveTaskToUserButton = ((System.Windows.Controls.Button)(target));
            return;
            case 7:
            this.BackToListButton = ((System.Windows.Controls.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

