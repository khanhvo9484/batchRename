#pragma checksum "..\..\ReplaceCharacterWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "2BA425D0FDF3A71C2E24ADF9CC3E63F42E479F8F7E7100974EF2773B616C33D2"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using ReplaceCharacter;
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


namespace ReplaceCharacter {
    
    
    /// <summary>
    /// ReplaceCharacterWindow
    /// </summary>
    public partial class ReplaceCharacterWindow : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 18 "..\..\ReplaceCharacterWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox source_input;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\ReplaceCharacterWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox target_input;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\ReplaceCharacterWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock NotifyText;
        
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
            System.Uri resourceLocater = new System.Uri("/ReplaceCharacter;component/replacecharacterwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\ReplaceCharacterWindow.xaml"
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
            
            #line 12 "..\..\ReplaceCharacterWindow.xaml"
            ((ReplaceCharacter.ReplaceCharacterWindow)(target)).Loaded += new System.Windows.RoutedEventHandler(this.UserControl_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.source_input = ((System.Windows.Controls.TextBox)(target));
            
            #line 22 "..\..\ReplaceCharacterWindow.xaml"
            this.source_input.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.ValidationTextBox);
            
            #line default
            #line hidden
            
            #line 24 "..\..\ReplaceCharacterWindow.xaml"
            this.source_input.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.source_input_changed);
            
            #line default
            #line hidden
            return;
            case 3:
            this.target_input = ((System.Windows.Controls.TextBox)(target));
            
            #line 32 "..\..\ReplaceCharacterWindow.xaml"
            this.target_input.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.ValidationTextBox);
            
            #line default
            #line hidden
            
            #line 34 "..\..\ReplaceCharacterWindow.xaml"
            this.target_input.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.target_input_changed);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 54 "..\..\ReplaceCharacterWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Apply_button_click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.NotifyText = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

