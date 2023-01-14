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

namespace AddSuffix
{
    /// <summary>
    /// Interaction logic for AddSuffixWindow.xaml
    /// </summary>
    public partial class AddSuffixWindow : UserControl, INotifyPropertyChanged
    {
        AddSuffixRule rule;

        public event PropertyChangedEventHandler PropertyChanged;

        public string _Suffix { get; set; } = "";

        public AddSuffixWindow(AddSuffixRule rule)
        {
            this.rule = rule;
            _Suffix = this.rule._Suffix;
            this.rule._arg1 = _Suffix;
            this.rule._arg2 = "";

            InitializeComponent();
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = this;

            if (Suffix_input.Text == "")
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

            if (Suffix_input.Text != "")
            {
                this.rule._Suffix = Suffix_input.Text;
                DictSetup.Add("Suffix", this.rule._Suffix);
                this.rule.Setup(DictSetup, null);
                //status.Source = new BitmapImage(new Uri(@"./Icons/Tick.png", UriKind.Relative));

            }
            NotifyText.Text = "Added";
        }

        private void input_suffix_changed(object sender, TextChangedEventArgs e)
        {
            if (Suffix_input.Text != "")
            {
                if (Suffix_input.Text != this.rule._Suffix)
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
