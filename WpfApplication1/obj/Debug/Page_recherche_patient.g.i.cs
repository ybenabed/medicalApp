﻿#pragma checksum "..\..\Page_recherche_patient.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "28AC3EE9775FE481A7A9F22542907F86"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Transitions;
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
    /// Page_recherche_patient
    /// </summary>
    public partial class Page_recherche_patient : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 22 "..\..\Page_recherche_patient.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox nom_box;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\Page_recherche_patient.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox prenom_box;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\Page_recherche_patient.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker date_box;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\Page_recherche_patient.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button but_rech;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\Page_recherche_patient.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lab_oui_non;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\Page_recherche_patient.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button but_oui;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\Page_recherche_patient.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button but_non;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\Page_recherche_patient.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image ic_pagepatient_close;
        
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
            System.Uri resourceLocater = new System.Uri("/WpfApplication1;component/page_recherche_patient.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Page_recherche_patient.xaml"
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
            this.nom_box = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.prenom_box = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.date_box = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 4:
            this.but_rech = ((System.Windows.Controls.Button)(target));
            
            #line 32 "..\..\Page_recherche_patient.xaml"
            this.but_rech.Click += new System.Windows.RoutedEventHandler(this.but_rech_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.lab_oui_non = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.but_oui = ((System.Windows.Controls.Button)(target));
            return;
            case 7:
            this.but_non = ((System.Windows.Controls.Button)(target));
            
            #line 36 "..\..\Page_recherche_patient.xaml"
            this.but_non.Click += new System.Windows.RoutedEventHandler(this.but_non_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.ic_pagepatient_close = ((System.Windows.Controls.Image)(target));
            
            #line 39 "..\..\Page_recherche_patient.xaml"
            this.ic_pagepatient_close.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.ic_pagepatient_close_MouseLeftButtonUp);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
