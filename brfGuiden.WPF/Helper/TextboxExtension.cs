using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Wpf.Ui.Controls;

namespace brfGuiden.WPF.Helper
{
    public class TextboxExtension : Wpf.Ui.Controls.TextBox
    {


        public TextboxExtension()
        {
            PreviewTextInput += MyTextBox_PreviewTextInput;
            
        }


        public static readonly DependencyProperty MaskProperty =
            DependencyProperty.Register("Mask", typeof(string), typeof(TextboxExtension), new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty RedBorderProperty =
    DependencyProperty.Register("SetRedBorder", typeof(string), typeof(TextboxExtension), new PropertyMetadata(string.Empty));


        void MyTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            var tb = sender as TextboxExtension;
            if (tb == null) return;

            if (IsMasking(tb) && !Regex.IsMatch(e.Text, Mask))
            {
                e.Handled = true;
            }
        }

        private static bool IsMasking(TextboxExtension tb)
        {
            if (string.IsNullOrWhiteSpace(tb.Mask)) return false;
            try
            {
                new Regex(tb.Mask);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public string Mask
        {
            get { return (string)GetValue(MaskProperty); }
            set { SetValue(MaskProperty, value); }
        }

        private bool _redBorder;

        public bool SetRedBorder
        {
            get { return (bool)GetValue(RedBorderProperty); }
            set { SetValue(RedBorderProperty, value); }
        }





    }
}
