﻿#pragma checksum "..\..\AddCounterWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "1365EBDB25E5CF548C06A398D52C9C90CB5E0AFECA2BF774A663CFDE6452759D"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using AddCounter;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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


namespace AddCounter {
    
    
    /// <summary>
    /// AddCounterWindow
    /// </summary>
    public partial class AddCounterWindow : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 19 "..\..\AddCounterWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Start_input;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\AddCounterWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Step_input;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\AddCounterWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image status;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/AddCounter;component/addcounterwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\AddCounterWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 12 "..\..\AddCounterWindow.xaml"
            ((AddCounter.AddCounterWindow)(target)).Loaded += new System.Windows.RoutedEventHandler(this.UserControl_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Start_input = ((System.Windows.Controls.TextBox)(target));
            
            #line 21 "..\..\AddCounterWindow.xaml"
            this.Start_input.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.ValidationTextBox);
            
            #line default
            #line hidden
            
            #line 22 "..\..\AddCounterWindow.xaml"
            this.Start_input.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.start_text_change);
            
            #line default
            #line hidden
            return;
            case 3:
            this.Step_input = ((System.Windows.Controls.TextBox)(target));
            
            #line 28 "..\..\AddCounterWindow.xaml"
            this.Step_input.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.ValidationTextBox);
            
            #line default
            #line hidden
            
            #line 29 "..\..\AddCounterWindow.xaml"
            this.Step_input.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.step_text_change);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 52 "..\..\AddCounterWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Apply_button_click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.status = ((System.Windows.Controls.Image)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

