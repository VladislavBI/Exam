﻿#pragma checksum "..\..\..\DB_Folder\StatisticWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "9684D291F387745F4A7CD54BE55AFDDF"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
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
using System.Windows.Forms;
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


namespace Exam_VSTBuh.DB_Folder {
    
    
    /// <summary>
    /// StatisticWindow
    /// </summary>
    public partial class StatisticWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 6 "..\..\..\DB_Folder\StatisticWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Exam_VSTBuh.DB_Folder.StatisticWindow statWindow;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\DB_Folder\StatisticWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button DelBut;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\DB_Folder\StatisticWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ChangeBut;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\DB_Folder\StatisticWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddBut;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\DB_Folder\StatisticWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid dataGridsGrid;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\DB_Folder\StatisticWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RowDefinition RowDEfResultRow;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\DB_Folder\StatisticWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dataGridView;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\DB_Folder\StatisticWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dataGridSelect;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\DB_Folder\StatisticWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button PrevBut;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\DB_Folder\StatisticWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButOK;
        
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
            System.Uri resourceLocater = new System.Uri("/Exam_VSTBuh;component/db_folder/statisticwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\DB_Folder\StatisticWindow.xaml"
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
            this.statWindow = ((Exam_VSTBuh.DB_Folder.StatisticWindow)(target));
            return;
            case 2:
            this.DelBut = ((System.Windows.Controls.Button)(target));
            
            #line 13 "..\..\..\DB_Folder\StatisticWindow.xaml"
            this.DelBut.Click += new System.Windows.RoutedEventHandler(this.DelBut_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ChangeBut = ((System.Windows.Controls.Button)(target));
            
            #line 14 "..\..\..\DB_Folder\StatisticWindow.xaml"
            this.ChangeBut.Click += new System.Windows.RoutedEventHandler(this.ChangeBut_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.AddBut = ((System.Windows.Controls.Button)(target));
            
            #line 15 "..\..\..\DB_Folder\StatisticWindow.xaml"
            this.AddBut.Click += new System.Windows.RoutedEventHandler(this.AddBut_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.dataGridsGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 6:
            this.RowDEfResultRow = ((System.Windows.Controls.RowDefinition)(target));
            return;
            case 7:
            this.dataGridView = ((System.Windows.Controls.DataGrid)(target));
            
            #line 22 "..\..\..\DB_Folder\StatisticWindow.xaml"
            this.dataGridView.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.dataGridView_MouseDoubleClick_1);
            
            #line default
            #line hidden
            return;
            case 8:
            this.dataGridSelect = ((System.Windows.Controls.DataGrid)(target));
            
            #line 23 "..\..\..\DB_Folder\StatisticWindow.xaml"
            this.dataGridSelect.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.dataGridView_MouseDoubleClick_1);
            
            #line default
            #line hidden
            return;
            case 9:
            this.PrevBut = ((System.Windows.Controls.Button)(target));
            
            #line 25 "..\..\..\DB_Folder\StatisticWindow.xaml"
            this.PrevBut.Click += new System.Windows.RoutedEventHandler(this.PrevBut_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.ButOK = ((System.Windows.Controls.Button)(target));
            
            #line 26 "..\..\..\DB_Folder\StatisticWindow.xaml"
            this.ButOK.Click += new System.Windows.RoutedEventHandler(this.ButOK_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

