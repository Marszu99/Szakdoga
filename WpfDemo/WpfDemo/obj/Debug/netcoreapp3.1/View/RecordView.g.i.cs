// Updated by XamlIntelliSenseFileGenerator 2022. 03. 15. 14:33:02
#pragma checksum "..\..\..\..\View\RecordView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9BFC046DB9228EC1C71D9825EDCC2BCEFBD5B594"
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


namespace WpfDemo.View
{


    /// <summary>
    /// RecordView
    /// </summary>
    public partial class RecordView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector
    {

#line default
#line hidden


#line 35 "..\..\..\..\View\RecordView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CreateRecordTask;

#line default
#line hidden


#line 66 "..\..\..\..\View\RecordView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker CreateRecordDate;

#line default
#line hidden


#line 145 "..\..\..\..\View\RecordView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox CreateRecordDuration;

#line default
#line hidden


#line 157 "..\..\..\..\View\RecordView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button DurationAddButtton;

#line default
#line hidden


#line 162 "..\..\..\..\View\RecordView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button DurationReduceButtton;

#line default
#line hidden


#line 174 "..\..\..\..\View\RecordView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox CreateRecordComment;

#line default
#line hidden


#line 187 "..\..\..\..\View\RecordView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CreateRecordButton;

#line default
#line hidden


#line 197 "..\..\..\..\View\RecordView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CancelCreateRecordButton;

#line default
#line hidden

        private bool _contentLoaded;

        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.14.0")]
        public void InitializeComponent()
        {
            if (_contentLoaded)
            {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/WpfDemo;component/view/recordview.xaml", System.UriKind.Relative);

#line 1 "..\..\..\..\View\RecordView.xaml"
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
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.RecordViewUserControl = ((WpfDemo.View.RecordView)(target));
                    return;
                case 2:
                    this.CreateRecordTask = ((System.Windows.Controls.ComboBox)(target));
                    return;
                case 3:
                    this.CreateRecordDate = ((System.Windows.Controls.DatePicker)(target));
                    return;
                case 4:
                    this.CreateRecordDuration = ((System.Windows.Controls.TextBox)(target));
                    return;
                case 5:
                    this.DurationAddButtton = ((System.Windows.Controls.Button)(target));
                    return;
                case 6:
                    this.DurationReduceButtton = ((System.Windows.Controls.Button)(target));
                    return;
                case 7:
                    this.CreateRecordComment = ((System.Windows.Controls.TextBox)(target));
                    return;
                case 8:
                    this.CreateRecordButton = ((System.Windows.Controls.Button)(target));
                    return;
                case 9:
                    this.CancelCreateRecordButton = ((System.Windows.Controls.Button)(target));
                    return;
            }
            this._contentLoaded = true;
        }

        internal System.Windows.Controls.UserControl RecordViewUserControl;
    }
}

