﻿#pragma checksum "..\..\ListadoContratos.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "AB94296DD65F4EF4D8B21D583DEC6FF9B8DDCB79A2EAE472ECA929C186E9B21E"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using MahApps.Metro.Behaviours;
using MahApps.Metro.Controls;
using OnBreakApp;
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
using System.Windows.Interactivity;
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


namespace OnBreakApp {
    
    
    /// <summary>
    /// ListadoContratos
    /// </summary>
    public partial class ListadoContratos : MahApps.Metro.Controls.MetroWindow, System.Windows.Markup.IComponentConnector {
        
        
        #line 29 "..\..\ListadoContratos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtContratoFiltrar;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\ListadoContratos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtRutFiltrar;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\ListadoContratos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtTipoFiltrar;
        
        #line default
        #line hidden
        
        
        #line 74 "..\..\ListadoContratos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid GridContratos;
        
        #line default
        #line hidden
        
        
        #line 77 "..\..\ListadoContratos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSelecionarContrato;
        
        #line default
        #line hidden
        
        
        #line 78 "..\..\ListadoContratos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCargarCliente;
        
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
            System.Uri resourceLocater = new System.Uri("/Presentacion;component/listadocontratos.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\ListadoContratos.xaml"
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
            
            #line 22 "..\..\ListadoContratos.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_Menu);
            
            #line default
            #line hidden
            return;
            case 2:
            this.txtContratoFiltrar = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.txtRutFiltrar = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.txtTipoFiltrar = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            
            #line 70 "..\..\ListadoContratos.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_Filtrar);
            
            #line default
            #line hidden
            return;
            case 6:
            this.GridContratos = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 7:
            this.btnSelecionarContrato = ((System.Windows.Controls.Button)(target));
            
            #line 77 "..\..\ListadoContratos.xaml"
            this.btnSelecionarContrato.Click += new System.Windows.RoutedEventHandler(this.Button_Click_Seleccionar);
            
            #line default
            #line hidden
            return;
            case 8:
            this.btnCargarCliente = ((System.Windows.Controls.Button)(target));
            
            #line 78 "..\..\ListadoContratos.xaml"
            this.btnCargarCliente.Click += new System.Windows.RoutedEventHandler(this.Button_Click_Cliente);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

