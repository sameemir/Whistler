﻿

#pragma checksum "D:\Windows\12-4-15\Whistler\Whistler\Whistler\View\MyProfile.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "AC26E4714D1EA65B54304C7555C9D1DA"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Whistler.View
{
    partial class MyProfile : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 46 "..\..\View\MyProfile.xaml"
                ((global::Windows.UI.Xaml.FrameworkElement)(target)).Loaded += this.buttonAddImage_Loaded;
                 #line default
                 #line hidden
                #line 46 "..\..\View\MyProfile.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.buttonAddImage_Tapped;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 63 "..\..\View\MyProfile.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.doneButton_Tapped;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 64 "..\..\View\MyProfile.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.resetButton_Tapped;
                 #line default
                 #line hidden
                break;
            case 4:
                #line 53 "..\..\View\MyProfile.xaml"
                ((global::Windows.UI.Xaml.FrameworkElement)(target)).Loaded += this.textboxPhone_Loaded;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


