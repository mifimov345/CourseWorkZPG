using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Text.RegularExpressions;
using System.Windows;

namespace CourseWorkZPG.ViewModels
{
    public class HintInputter : TextBox
    {
        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            base.OnPreviewTextInput(e);

            Regex r = new Regex("[^а-яёА-яё]+");
            if (Text.Length == 0)
            {
                Text = "";
            }
            if (Text.Length >= 15)
            {
                e.Handled = true;
                return;
            }

            e.Handled = r.IsMatch(e.Text);
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            base.OnPreviewKeyDown(e);
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);
            DataObject.AddPastingHandler(this, PasteHandler);
        }

        private void PasteHandler(object sender, DataObjectPastingEventArgs e)
        {
            TextBox tb = sender as TextBox;
            bool textOK = false;
            Regex r = new Regex("[^а-яёА-яё]+");
            string pasteText = "";

            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string temp = "";
                pasteText = e.DataObject.GetData(typeof(string)) as string;
                string[] result = Regex.Split(pasteText, "[^а-яёА-яё]+");
                for (int i = 0; i < result.Length; i++)
                {
                    temp += result[i];
                }
                Text = temp;
            }
            e.CancelCommand();
        }
    }
}
