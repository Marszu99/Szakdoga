﻿#pragma checksum "..\..\..\..\View\UserProfileView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "DDAB35D7D58CF51E37848E41DBEA20EF417AF20D"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Syncfusion.UI.Xaml.Charts;
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
        
        
        #line 281 "..\..\..\..\View\UserProfileView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddTaskToUserTaskButton;
        
        #line default
        #line hidden
        
        
        #line 293 "..\..\..\..\View\UserProfileView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.FrameworkElement frameworkElement;
        
        #line default
        #line hidden
        
        /// <summary>
        /// UserTasksDataGrid Name Field
        /// </summary>
        
        #line 296 "..\..\..\..\View\UserProfileView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        public System.Windows.Controls.DataGrid UserTasksDataGrid;
        
        #line default
        #line hidden
        
        /// <summary>
        /// UserRecordsDataGrid Name Field
        /// </summary>
        
        #line 442 "..\..\..\..\View\UserProfileView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        public System.Windows.Controls.DataGrid UserRecordsDataGrid;
        
        #line default
        #line hidden
        
        
        #line 510 "..\..\..\..\View\UserProfileView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Syncfusion.UI.Xaml.Charts.SfChart MyChart;
        
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
            this.AddTaskToUserTaskButton = ((System.Windows.Controls.Button)(target));
            return;
            case 2:
            this.frameworkElement = ((System.Windows.FrameworkElement)(target));
            return;
            case 3:
            this.UserTasksDataGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 4:
            this.UserRecordsDataGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 5:
            this.MyChart = ((Syncfusion.UI.Xaml.Charts.SfChart)(target));
            return;
            case 6:
            
            #line 520 "..\..\..\..\View\UserProfileView.xaml"
            ((Syncfusion.UI.Xaml.Charts.TimeSpanAxis)(target)).LabelCreated += new System.EventHandler<Syncfusion.UI.Xaml.Charts.LabelCreatedEventArgs>(this.NumericalAxis_LabelCreated);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

