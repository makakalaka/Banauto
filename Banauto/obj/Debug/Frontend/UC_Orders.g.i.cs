﻿#pragma checksum "..\..\..\Frontend\UC_Orders.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "33E5BB813E1C7FABA95744013111C03364110219DA9383DCDF37FD9A676DC58C"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Banauto.Frontend;
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


namespace Banauto.Frontend {
    
    
    /// <summary>
    /// UC_Orders
    /// </summary>
    public partial class UC_Orders : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 8 "..\..\..\Frontend\UC_Orders.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid gridStatus;
        
        #line default
        #line hidden
        
        
        #line 9 "..\..\..\Frontend\UC_Orders.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblOrderID;
        
        #line default
        #line hidden
        
        
        #line 10 "..\..\..\Frontend\UC_Orders.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblAutoID;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\..\Frontend\UC_Orders.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblName;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\Frontend\UC_Orders.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblSurname;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\Frontend\UC_Orders.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblPhone;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\Frontend\UC_Orders.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnChange;
        
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
            System.Uri resourceLocater = new System.Uri("/Banauto;component/frontend/uc_orders.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Frontend\UC_Orders.xaml"
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
            this.gridStatus = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.lblOrderID = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.lblAutoID = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.lblName = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.lblSurname = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.lblPhone = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.btnChange = ((System.Windows.Controls.Button)(target));
            
            #line 14 "..\..\..\Frontend\UC_Orders.xaml"
            this.btnChange.Click += new System.Windows.RoutedEventHandler(this.btnChange_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

