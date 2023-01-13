using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChangeExtension
{
    /// <summary>
    /// Interaction logic for ChangeExtensionWindow.xaml
    /// </summary>
    public partial class ChangeExtensionWindow : UserControl, INotifyPropertyChanged
    {
        ChangeExtensionRule rule;

        public event PropertyChangedEventHandler PropertyChanged;

        public string _NewExt { get; set; } = "";
        public ChangeExtensionWindow(ChangeExtensionRule rule)
        {
            this.rule = rule;
            _NewExt = this.rule._NewExt;
            this.rule._arg1 = _NewExt;
            this.rule._arg2 = "";

            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            this.DataContext = this;
            if (extension_input.Text == "")
            {
                //status.Source = new BitmapImage(new Uri(@"./Icons/Cross.png", UriKind.Relative));

            }
        }

        private void ValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"[\/:*?""<>|\\]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Apply_button_click(object sender, RoutedEventArgs e)
        {
            Dictionary<string, string> DictSetup = new Dictionary<string, string>();
            List<string> ListSetup = new List<string>();

            if (extension_input.Text != "")
            {
                this.rule._NewExt = extension_input.Text;
                DictSetup.Add("NewExt", this.rule._NewExt);
                this.rule.Setup(DictSetup, null);

                //status.Source = new BitmapImage(new Uri(@"./Icons/Tick.png", UriKind.Relative));

            }
            NotifyText.Text = "Added";
        }

        private void prefixInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (extension_input.Text != "")
            {
                if (extension_input.Text != this.rule._NewExt)
                {
                    //status.Source = new BitmapImage(new Uri(@"./Icons/Cross.png", UriKind.Relative));

                }
                else
                {
                    //status.Source = new BitmapImage(new Uri(@"./Icons/Tick.png", UriKind.Relative));

                }
            }
        }
    }
}
