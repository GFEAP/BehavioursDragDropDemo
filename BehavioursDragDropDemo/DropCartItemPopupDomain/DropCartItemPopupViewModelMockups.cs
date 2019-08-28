using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BehavioursDragDropDemo.DropCartItemPopupDomain
{
    public static class DropCartItemPopupViewModelMockups
    {
        public static DropCartItemPopupViewModel<int> InputPopupViewModelMockup
        {
            get
            {
                return new DropCartItemPopupViewModel<int>()
                {
                    TextBox1Text = 10,
                    Label1Content = "Items:",
                    Button1Content = "OK",
                    Button2Content = "Cancel",
                    IsVisible = true
                };
            }
        }
    }
}