﻿#pragma checksum "..\..\Interface_Authentification.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "582CDD22FD09C8AC6E987F383D46916E"
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


namespace WpfApplication1 {
    
    
    /// <summary>
    /// Interface_Authentification
    /// </summary>
    public partial class Interface_Authentification : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\Interface_Authentification.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Username;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\Interface_Authentification.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox Password;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\Interface_Authentification.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle auth_rectangle;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\Interface_Authentification.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Connexion_Lab;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\Interface_Authentification.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label close_lab_auth;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\Interface_Authentification.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Reduir_lab_auth;
        
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
            System.Uri resourceLocater = new System.Uri("/WpfApplication1;component/interface_authentification.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Interface_Authentification.xaml"
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
            this.Username = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.Password = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 3:
            this.auth_rectangle = ((System.Windows.Shapes.Rectangle)(target));
            
            #line 14 "..\..\Interface_Authentification.xaml"
            this.auth_rectangle.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.auth_rectangle_MouseDown);
            
            #line default
            #line hidden
            return;
            case 4:
            this.Connexion_Lab = ((System.Windows.Controls.Label)(target));
            
            #line 15 "..\..\Interface_Authentification.xaml"
            this.Connexion_Lab.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Connexion_Lab_MouseLeftButtonDown);
            
            #line default
            #line hidden
            
            #line 15 "..\..\Interface_Authentification.xaml"
            this.Connexion_Lab.MouseEnter += new System.Windows.Input.MouseEventHandler(this.Connexion_Lab_MouseEnter);
            
            #line default
            #line hidden
            
            #line 15 "..\..\Interface_Authentification.xaml"
            this.Connexion_Lab.MouseLeave += new System.Windows.Input.MouseEventHandler(this.Connexion_Lab_MouseLeave);
            
            #line default
            #line hidden
            return;
            case 5:
            this.close_lab_auth = ((System.Windows.Controls.Label)(target));
            
            #line 17 "..\..\Interface_Authentification.xaml"
            this.close_lab_auth.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.close_lab_auth_MouseLeftButtonUp);
            
            #line default
            #line hidden
            
            #line 17 "..\..\Interface_Authentification.xaml"
            this.close_lab_auth.MouseEnter += new System.Windows.Input.MouseEventHandler(this.close_lab_auth_MouseEnter);
            
            #line default
            #line hidden
            
            #line 17 "..\..\Interface_Authentification.xaml"
            this.close_lab_auth.MouseLeave += new System.Windows.Input.MouseEventHandler(this.close_lab_auth_MouseLeave);
            
            #line default
            #line hidden
            return;
            case 6:
            this.Reduir_lab_auth = ((System.Windows.Controls.Label)(target));
            
            #line 18 "..\..\Interface_Authentification.xaml"
            this.Reduir_lab_auth.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.Reduir_lab_auth_MouseLeftButtonUp);
            
            #line default
            #line hidden
            
            #line 18 "..\..\Interface_Authentification.xaml"
            this.Reduir_lab_auth.MouseEnter += new System.Windows.Input.MouseEventHandler(this.Reduir_lab_auth_MouseEnter);
            
            #line default
            #line hidden
            
            #line 18 "..\..\Interface_Authentification.xaml"
            this.Reduir_lab_auth.MouseLeave += new System.Windows.Input.MouseEventHandler(this.Reduir_lab_auth_MouseLeave);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
