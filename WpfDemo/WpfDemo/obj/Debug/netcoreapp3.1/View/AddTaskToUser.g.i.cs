#pragma checksum "..\..\..\..\View\AddTaskToUser.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "A84A43D9CC5BB36E92FA1DBEAF67C4E4FCFC7C6D"
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
using WpfDemo.View;


namespace WpfDemo.View {
    
    
    /// <summary>
    /// AddTaskToUser
    /// </summary>
    public partial class AddTaskToUser : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 39 "..\..\..\..\View\AddTaskToUser.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox AddTaskUser;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\..\View\AddTaskToUser.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox AddTaskTitle;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\..\..\View\AddTaskToUser.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox AddTaskDescription;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\..\..\View\AddTaskToUser.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker AddTaskDeadline;
        
        #line default
        #line hidden
        
        
        #line 73 "..\..\..\..\View\AddTaskToUser.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CreateTaskStatus;
        
        #line default
        #line hidden
        
        
        #line 78 "..\..\..\..\View\AddTaskToUser.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddTaskToUserButton;
        
        #line default
        #line hidden
        
        
        #line 87 "..\..\..\..\View\AddTaskToUser.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BackToListButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.13.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/WpfDemo;V1.0.0.0;component/view/addtasktouser.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\View\AddTaskToUser.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.13.0")]
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
            this.AddTaskToUserButton = ((System.Windows.Controls.Button)(target));
            return;
            case 7:
            this.BackToListButton = ((System.Windows.Controls.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

