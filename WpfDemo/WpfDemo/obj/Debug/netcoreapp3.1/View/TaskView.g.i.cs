﻿#pragma checksum "..\..\..\..\View\TaskView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "26F4F477DE279B76EE9F06A5B7E41E42282DE850"
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
using WpfDemo.ViewModel;


namespace WpfDemo.View {
    
    
    /// <summary>
    /// TaskView
    /// </summary>
    public partial class TaskView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\..\..\View\TaskView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal WpfDemo.View.TaskView Task;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\..\View\TaskView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CreateTaskUser;
        
        #line default
        #line hidden
        
        
        #line 72 "..\..\..\..\View\TaskView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox CreateTaskTitle;
        
        #line default
        #line hidden
        
        
        #line 91 "..\..\..\..\View\TaskView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox CreateTaskDescription;
        
        #line default
        #line hidden
        
        
        #line 105 "..\..\..\..\View\TaskView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker CreateTaskDeadline;
        
        #line default
        #line hidden
        
        
        #line 151 "..\..\..\..\View\TaskView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CreateTaskStatus;
        
        #line default
        #line hidden
        
        
        #line 182 "..\..\..\..\View\TaskView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CreateTaskForUserButton;
        
        #line default
        #line hidden
        
        
        #line 191 "..\..\..\..\View\TaskView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CancelCreateTaskForUserButton;
        
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
            System.Uri resourceLocater = new System.Uri("/WpfDemo;component/view/taskview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\View\TaskView.xaml"
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
            this.Task = ((WpfDemo.View.TaskView)(target));
            return;
            case 2:
            this.CreateTaskUser = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 3:
            this.CreateTaskTitle = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.CreateTaskDescription = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.CreateTaskDeadline = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 6:
            this.CreateTaskStatus = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 7:
            this.CreateTaskForUserButton = ((System.Windows.Controls.Button)(target));
            return;
            case 8:
            this.CancelCreateTaskForUserButton = ((System.Windows.Controls.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

