﻿#pragma checksum "..\..\SnipScreenForm.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "2FECA3E5201E65BAE47C5E4F4A19CE754A32F581EDFFC3DA3A3180CC22467222"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MySnipItTool;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
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


namespace MySnipItTool {
    
    
    /// <summary>
    /// SnipScreenForm
    /// </summary>
    public partial class SnipScreenForm : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\SnipScreenForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas canvas;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\SnipScreenForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle rectLeft;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\SnipScreenForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle rectRight;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\SnipScreenForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle rectTop;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\SnipScreenForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle rectBottom;
        
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
            System.Uri resourceLocater = new System.Uri("/MySnipItTool;component/snipscreenform.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\SnipScreenForm.xaml"
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
            
            #line 8 "..\..\SnipScreenForm.xaml"
            ((MySnipItTool.SnipScreenForm)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.canvas = ((System.Windows.Controls.Canvas)(target));
            
            #line 15 "..\..\SnipScreenForm.xaml"
            this.canvas.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.MouseDown_Event);
            
            #line default
            #line hidden
            
            #line 15 "..\..\SnipScreenForm.xaml"
            this.canvas.MouseMove += new System.Windows.Input.MouseEventHandler(this.MouseMove_Event);
            
            #line default
            #line hidden
            
            #line 16 "..\..\SnipScreenForm.xaml"
            this.canvas.MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.MouseUp_Event);
            
            #line default
            #line hidden
            return;
            case 3:
            this.rectLeft = ((System.Windows.Shapes.Rectangle)(target));
            return;
            case 4:
            this.rectRight = ((System.Windows.Shapes.Rectangle)(target));
            return;
            case 5:
            this.rectTop = ((System.Windows.Shapes.Rectangle)(target));
            return;
            case 6:
            this.rectBottom = ((System.Windows.Shapes.Rectangle)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

