﻿#pragma checksum "..\..\SelectedImage.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6A440ACBA0873AA1A8C0946986F6B045987A337D"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

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
using WpfApp3_joystick;


namespace WpfApp3_joystick {
    
    
    /// <summary>
    /// SelectedImage
    /// </summary>
    public partial class SelectedImage : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 1 "..\..\SelectedImage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal WpfApp3_joystick.SelectedImage SelectedImage_Window;
        
        #line default
        #line hidden
        
        
        #line 9 "..\..\SelectedImage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid MainGrid;
        
        #line default
        #line hidden
        
        
        #line 10 "..\..\SelectedImage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid ImageGrid;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\SelectedImage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image Selected_Image;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\SelectedImage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GroupBox GroupBox_Ruler;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\SelectedImage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid GroupBoxRuler_Grid;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\SelectedImage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Label_0;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\SelectedImage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TextBox2;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\SelectedImage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TextBox1;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\SelectedImage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Result_Label;
        
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
            System.Uri resourceLocater = new System.Uri("/WpfApp3-joystick;component/selectedimage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\SelectedImage.xaml"
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
            this.SelectedImage_Window = ((WpfApp3_joystick.SelectedImage)(target));
            
            #line 8 "..\..\SelectedImage.xaml"
            this.SelectedImage_Window.Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.MainGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.ImageGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 4:
            this.Selected_Image = ((System.Windows.Controls.Image)(target));
            
            #line 11 "..\..\SelectedImage.xaml"
            this.Selected_Image.MouseMove += new System.Windows.Input.MouseEventHandler(this.Selected_Image_MouseMove);
            
            #line default
            #line hidden
            
            #line 11 "..\..\SelectedImage.xaml"
            this.Selected_Image.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Selected_Image_MouseDown);
            
            #line default
            #line hidden
            
            #line 11 "..\..\SelectedImage.xaml"
            this.Selected_Image.MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.Selected_Image_MouseUp);
            
            #line default
            #line hidden
            return;
            case 5:
            this.GroupBox_Ruler = ((System.Windows.Controls.GroupBox)(target));
            return;
            case 6:
            this.GroupBoxRuler_Grid = ((System.Windows.Controls.Grid)(target));
            return;
            case 7:
            this.Label_0 = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.TextBox2 = ((System.Windows.Controls.TextBox)(target));
            
            #line 16 "..\..\SelectedImage.xaml"
            this.TextBox2.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextBox2_TextChanged);
            
            #line default
            #line hidden
            return;
            case 9:
            this.TextBox1 = ((System.Windows.Controls.TextBox)(target));
            
            #line 17 "..\..\SelectedImage.xaml"
            this.TextBox1.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextBox1_TextChanged);
            
            #line default
            #line hidden
            return;
            case 10:
            this.Result_Label = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
